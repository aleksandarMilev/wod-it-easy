import { Link } from "react-router-dom";
import { FaUserAlt, FaRunning } from "react-icons/fa";

import { routes } from "../../../common/constants";

import "./RegistrationChoice.css";

export default function RegistrationChoice() {
  return (
    <div className="registration-choice">
      <h2>Welcome! Please, choose your profile:</h2>
      <div className="options-container">
        <Link to={routes.home}>
          <div className="option-card">
            <FaUserAlt className="icon" />
            <h3>I am a regular user</h3>
          </div>
        </Link>
        <Link to={routes.athlete.create}>
          <div className="option-card">
            <FaRunning className="icon" />
            <h3>I am an athlete</h3>
          </div>
        </Link>
      </div>
    </div>
  );
}
