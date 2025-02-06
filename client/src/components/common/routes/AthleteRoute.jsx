import { useContext } from "react";

import { Navigate } from "react-router-dom";
import { UserContext } from "../../../contexts/User";
import { routes } from "../../../common/constants";

export default function NonAthleteRoute({ element }) {
  const { isAuthenticated, isAthlete } = useContext(UserContext);

  if (!isAuthenticated) {
    return <Navigate to={routes.login} replace />;
  }

  if (!isAthlete) {
    return <Navigate to={routes.home} replace />;
  }

  return element;
}
