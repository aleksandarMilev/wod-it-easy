import { useContext } from "react";
import { Link } from "react-router-dom";

import { routes } from "../../../common/constants";
import { UserContext } from "../../../contexts/User";

import "./Navigation.css";

export default function Navigation() {
  const { username, isAuthenticated, isAthlete, isAdmin, hasProfile, logout } =
    useContext(UserContext);

  console.log("Navigation rendered");
  console.log("isAuthenticated:", isAuthenticated);
  console.log("isAthlete:", isAthlete);
  console.log("isAdmin:", isAdmin);
  console.log("hasProfile:", hasProfile);
  console.log("username:", username);

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
          {isAuthenticated && !hasProfile && !isAdmin && (
            <>
              <li>
                <Link to={routes.profile.create}>Configure Profile</Link>
              </li>
            </>
          )}
          {isAuthenticated && hasProfile && !isAdmin && (
            <>
              <li>
                <Link to={routes.profile.default}>Profile</Link>
              </li>
            </>
          )}
          {isAthlete && !isAdmin && isAuthenticated && (
            <>
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
