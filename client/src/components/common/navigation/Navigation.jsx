import { useContext } from "react";
import { Link } from "react-router-dom";

import { routes } from "../../../common/constants";
import { UserContext } from "../../../contexts/User";

import "./Navigation.css";

export default function Navigation() {
  const { username, isAuthenticated, isAthlete, isAdmin, logout } =
    useContext(UserContext);

  return (
    <header className="header">
      <Link to={routes.home}>
        <div className="logo">
          <h1>Wod It Easy</h1>
          <p>Stronger, Faster, Better</p>
        </div>
      </Link>
      <nav className="nav-bar">
        <ul className="nav-left">
          {!isAthlete && !isAdmin && isAuthenticated && (
            <li>
              <Link to={routes.athlete.create}>Become an Athlete</Link>
            </li>
          )}
          {isAthlete && !isAdmin && isAuthenticated && (
            <>
              <li>
                <Link to={routes.athlete.mine}>Profile</Link>
              </li>
              <li>
                <Link to={routes.participation.default}>Participations</Link>
              </li>
            </>
          )}
          {isAdmin && (
            <li>
              <Link to={routes.workout.create}>Create Workout</Link>
            </li>
          )}
          {isAuthenticated && (
            <li>
              <Link to={routes.workout.search}>Workouts</Link>
            </li>
          )}
        </ul>
        <ul className="nav-right">
          {!isAuthenticated ? (
            <>
              <li>
                <Link to={routes.login}>Login</Link>
              </li>
              <li>
                <Link to={routes.register}>Register</Link>
              </li>
            </>
          ) : (
            <>
              <li>
                <span>Hello, {username}!</span>
              </li>
              <li>
                <button onClick={logout}>Logout</button>
              </li>
            </>
          )}
        </ul>
      </nav>
    </header>
  );
}
