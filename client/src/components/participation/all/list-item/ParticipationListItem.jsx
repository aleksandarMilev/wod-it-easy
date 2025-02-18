import { Link } from "react-router-dom";
import { useContext, useState, useEffect } from "react";

import * as api from "../../../../api/participationApi";
import { UserContext } from "../../../../contexts/User";
import { useMessage } from "../../../../contexts/Message";
import { routes, participationStatuses } from "../../../../common/constants";
import {
  formatUtcDateAndTime,
  getDateTimeNow,
} from "../../../../common/functions";

import DeleteConfirmModal from "../../../common/delete-modal/DeleteConfirmModal";

import "./ParticipationListItem.css";

export default function ParticipationListItem({
  id,
  workoutId,
  workoutName,
  workoutStartsAt,
  workoutIsFull,
  joinedAt: initialJoinedAt,
  modifiedOn: initialModifiedOn,
  status: initialStatus,
  onDelete,
}) {
  const { showMessage } = useMessage();
  const { token } = useContext(UserContext);

  const [status, setStatus] = useState(initialStatus);
  const [joinedAt, setJoinedAt] = useState(initialJoinedAt);
  const [modifiedOn, setModifiedOn] = useState(initialModifiedOn);

  useEffect(() => {
    setStatus(initialStatus);
    setJoinedAt(initialJoinedAt);
    setModifiedOn(initialModifiedOn);
  }, [initialStatus, initialJoinedAt, initialModifiedOn]);

  const isJoined =
    status.toLowerCase() === participationStatuses.joined.toLowerCase();
  const isLeft =
    status.toLowerCase() === participationStatuses.left.toLowerCase();

  const isClosed = (() => {
    const now = new Date();
    const workoutStartLocal = new Date(workoutStartsAt + "z");

    const isNotToday = now.getDate() !== workoutStartLocal.getDate();

    if (isNotToday) {
      return false;
    }

    const timeDifference = (workoutStartLocal - now) / (1000 * 60 * 60);

    const thereIsLessThanTwoHoursToTheWorkout = timeDifference <= 2;
    return thereIsLessThanTwoHoursToTheWorkout;
  })();

  const [isFull, setIsFull] = useState(workoutIsFull);

  const cancelHandler = async () => {
    const success = await api.leave(id, token);

    if (success) {
      setStatus(participationStatuses.left);
      setModifiedOn(getDateTimeNow());
      setIsFull(false);

      showMessage("You have successfully canceled this workout!", true);
    } else {
      showMessage(
        "Something went wrong while canceling this workout, please try again!",
        false
      );
    }
  };

  const reJoinHandler = async () => {
    const success = await api.reJoin(id, token);

    if (success) {
      setStatus(participationStatuses.joined);
      setModifiedOn(getDateTimeNow());

      showMessage("You are again a participant in this workout!", true);
    } else {
      showMessage(
        "Something went wrong while adding you to this workout, please try again!",
        false
      );
    }
  };

  const [showModal, setShowModal] = useState(false);
  const toggleModal = () => setShowModal((prev) => !prev);

  const deleteHandler = async () => {
    if (showModal) {
      const success = await api.remove(id, token);

      if (success) {
        showMessage(`The participation was successfully deleted!`, true);
        onDelete(id);
      } else {
        showMessage(
          "Something went wrong while deleting your participation, please, try again.",
          false
        );
      }
      toggleModal();
    } else {
      toggleModal();
    }
  };

  return (
    <div className="participation-list-item-card card mb-3 shadow-sm">
      <div className="card-body">
        {(!isClosed || isLeft) && (
          <button
            className="delete-button"
            onClick={toggleModal}
            aria-label="Delete"
          >
            Delete 🗑️
          </button>
        )}

        <h5 className="card-title">{workoutName}</h5>
        <p className="card-text" data-icon="date">
          <strong>Start At:</strong> {formatUtcDateAndTime(workoutStartsAt)}
        </p>
        <p className="card-text" data-icon="joined">
          <strong>{isJoined ? "Joined At:" : "Left At:"}</strong>
          {formatUtcDateAndTime(modifiedOn || joinedAt)}
        </p>
        {status && (
          <p
            data-icon="status"
            className={`card-text ${
              isJoined ? "text-success" : "text-warning"
            }`}
          >
            <strong>Status:</strong> {status.toUpperCase()}
          </p>
        )}
        <Link to={routes.workout.default + `/${workoutId}`}>View</Link>

        {isJoined && !isClosed && (
          <button onClick={cancelHandler}>Cancel</button>
        )}
        {isLeft && !isClosed && !isFull && (
          <button onClick={reJoinHandler}>Join Again</button>
        )}

        {isClosed && isLeft && (
          <p className="text-danger mt-3">
            This workout is already closed, so you can not join it again. You
            can delete the participation from the delete icon.
          </p>
        )}
        {isFull && isLeft && (
          <p className="text-danger mt-3">
            This workout has reached its full capacity so you can not join it
            again. You can delete the participation from the delete icon.
          </p>
        )}
        {!isClosed && !isFull && isLeft && (
          <p className="text-danger mt-3">
            You are currently not a participant in this workout. You can delete
            the participation from the delete icon.
          </p>
        )}
      </div>
      <DeleteConfirmModal
        showModal={showModal}
        toggleModal={toggleModal}
        deleteHandler={deleteHandler}
      />
    </div>
  );
}
