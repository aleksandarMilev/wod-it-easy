import { useNavigate } from "react-router-dom";
import { useContext } from "react";
import { jwtDecode } from "jwt-decode";

import * as api from "../api/identityApi";
import { getId } from "../api/athleteApi";
import { UserContext } from "../contexts/User";
import { useMessage } from "../contexts/Message";
import { routes } from "../common/constants";

function useAuthentication() {
  const navigate = useNavigate();
  const { showMessage } = useMessage();

  const { changeAuthenticationState } = useContext(UserContext);

  const onAuthenticate = async (apiCall, data) => {
    try {
      const token = await apiCall(data);
      const tokenEncoded = jwtDecode(token);
      const username = tokenEncoded["unique_name"];

      const user = {
        token: token,
        username: username,
        email: tokenEncoded.email,
        userId: tokenEncoded.nameid,
        isAdmin: !!tokenEncoded.role,
      };

      if (apiCall === api.login && !user.isAdmin) {
        const athleteId = await getId(token);

        user.athleteId = athleteId;
        user.isAthlete = !!athleteId;
      }

      changeAuthenticationState(user);

      showMessage(`Welcome, ${username}!`, true);
      navigate(routes.home);
    } catch (error) {
      showMessage(error.message, false);
    }
  };

  return onAuthenticate;
}

export function useLogin() {
  const onAuthenticate = useAuthentication();
  const onLogin = (data) => onAuthenticate(api.login, data);

  return onLogin;
}

export function useRegister() {
  const onAuthenticate = useAuthentication();
  const onRegister = (data) => onAuthenticate(api.register, data);

  return onRegister;
}
