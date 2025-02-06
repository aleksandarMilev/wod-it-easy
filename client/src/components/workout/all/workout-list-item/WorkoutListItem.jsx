import { Link } from "react-router-dom";
import { FaUsers, FaClock } from "react-icons/fa";

import { formatDate } from "../../../../common/functions";
import { routes } from "../../../../common/constants";

import "./WorkoutListItem.css";

export default function WorkoutListItem({
  id,
  name,
  imageUrl,
  type,
  startsAtDate,
  startsAtTime,
  currentParticipantsCount,
  maxParticipantsCount,
}) {
  return (
    <div className="row p-3 bg-light border rounded mb-3 shadow-sm workout-list-item">
      <div className="col-md-3 col-4 mt-1 d-flex justify-content-center align-items-center">
        <img src={imageUrl} alt={name} className="workout-list-item-image" />
      </div>
      <div className="col-md-6 col-8 mt-1 workout-list-item-content">
        <h5 className="mb-2 workout-list-item-title">{name}</h5>
        <h6 className="text-muted mb-2 workout-list-item-type">{type}</h6>
        <div className="d-flex flex-row mb-2 workout-list-item-participants">
          <FaUsers className="me-2" /> {currentParticipantsCount} /{" "}
          {maxParticipantsCount} Participants
        </div>
        <div className="mt-1 mb-2 workout-list-item-time">
          <FaClock className="me-2" /> {formatDate(startsAtDate)} at{" "}
          {startsAtTime.slice(0, 5)}
        </div>
      </div>

      <div className="col-md-3 d-flex align-items-center justify-content-center mt-1">
        <div className="d-flex flex-column align-items-center">
          <Link
            to={routes.workout.default + `/${id}`}
            className="btn btn-sm btn-primary workout-list-item-btn"
          >
            View Details
          </Link>
        </div>
      </div>
    </div>
  );
}
