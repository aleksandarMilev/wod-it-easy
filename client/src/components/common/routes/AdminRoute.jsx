import { useContext } from "react";
import { Navigate } from "react-router-dom";

import { UserContext } from "../../../contexts/User";
import { routes } from "../../../common/constants";

export default function AdminRoute({ element }) {
  const { isAuthenticated, isAdmin } = useContext(UserContext);

  if (!isAuthenticated) {
    return <Navigate to={routes.login} replace />;
  }

  if (!isAdmin) {
    return <Navigate to={routes.error.accessDenied} replace />;
  }

  return element;
}
