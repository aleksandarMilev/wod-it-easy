import { createContext } from "react";
import { useNavigate } from "react-router-dom";

import { routes } from "../common/constants";
import { useMessage } from "./Message";
import usePersistedState from "../hooks/usePersistedState";

export const UserContext = createContext({
  userId: "",
  athleteId: "",
  athleteName: "",
  username: "",
  email: "",
  token: "",
  isAdmin: false,
  isAthlete: false,
  isAuthenticated: false,
  hasProfile: false,
  changeAuthenticationState: (state) => {},
  updateAthleteIdAndName: (id, name) => {},
  setHasProfile: (hasProfile) => {},
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

  const updateAthleteIdAndName = (id, name) =>
    setUser({
      userId: user.userId,
      athleteId: id,
      athleteName: name,
      username: user.username,
      email: user.email,
      isAdmin: user.isAdmin,
      isAthlete: id && id !== 0,
      token: user.token,
      isAuthenticated: !!user.username,
      hasProfile: user.hasProfile,
      changeAuthenticationState,
      updateAthleteIdAndName,
      setHasProfile,
      logout,
    });

  const setHasProfile = (hasProfile) =>
    setUser({
      userId: user.userId,
      athleteId: user.athleteId,
      athleteName: user.athleteName,
      username: user.username,
      email: user.email,
      isAdmin: user.isAdmin,
      isAthlete: user.athleteId && user.athleteId !== 0,
      token: user.token,
      isAuthenticated: !!user.username,
      hasProfile: hasProfile,
      changeAuthenticationState,
      updateAthleteIdAndName,
      setHasProfile,
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
    athleteName: user.athleteName,
    username: user.username,
    email: user.email,
    isAdmin: user.isAdmin,
    token: user.token,
    isAthlete: user.isAthlete,
    isAuthenticated: !!user.username,
    hasProfile: user.hasProfile,
    changeAuthenticationState,
    updateAthleteIdAndName,
    setHasProfile,
    logout,
  };

  return (
    <UserContext.Provider value={userData}>
      {props.children}
    </UserContext.Provider>
  );
}
