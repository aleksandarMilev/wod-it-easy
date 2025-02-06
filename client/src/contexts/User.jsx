import { createContext } from "react";
import { useNavigate } from "react-router-dom";

import { routes } from "../common/constants";
import { useMessage } from "./Message";
import usePersistedState from "../hooks/usePersistedState";

export const UserContext = createContext({
  userId: "",
  athleteId: "",
  username: "",
  email: "",
  token: "",
  isAdmin: false,
  isAthlete: false,
  isAuthenticated: false,
  changeAuthenticationState: (state) => {},
  updateAthleteId: (id) => {},
  logout: () => {},
});

export function UserContextProvider(props) {
  const { showMessage } = useMessage();
  const navigate = useNavigate();

  const getInitUser = () => {
    const storedUser = localStorage.getItem("user");
    return storedUser ? JSON.parse(storedUser) : {};
  };

  const [user, setUser] = usePersistedState("user", getInitUser());

  const changeAuthenticationState = (state) => setUser(state);

  const updateAthleteId = (athleteId) =>
    setUser({
      userId: user.userId,
      athleteId: athleteId,
      username: user.username,
      email: user.email,
      isAdmin: user.isAdmin,
      isAthlete: athleteId && athleteId !== 0,
      token: user.token,
      isAuthenticated: !!user.username,
      changeAuthenticationState,
      logout,
    });

  const logout = () => {
    const username = user.username;

    setUser({});
    localStorage.removeItem("user");

    showMessage(`Goodbuy, ${username}!`, true);
    navigate(routes.home);
  };

  const userData = {
    userId: user.userId,
    athleteId: user.athleteId,
    username: user.username,
    email: user.email,
    isAdmin: user.isAdmin,
    token: user.token,
    isAthlete: user.isAthlete,
    isAuthenticated: !!user.username,
    changeAuthenticationState,
    updateAthleteId,
    logout,
  };

  return (
    <UserContext.Provider value={userData}>
      {props.children}
    </UserContext.Provider>
  );
}
