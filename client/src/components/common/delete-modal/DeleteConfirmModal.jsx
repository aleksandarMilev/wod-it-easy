import { FaExclamationTriangle, FaTrashAlt } from "react-icons/fa";

import "./DeleteConfirmModal.css";

export default function DeleteConfirmModal({
  showModal,
  toggleModal,
  deleteHandler,
  message = null,
}) {
  return (
    <div
      className={`modal ${showModal ? "fade show d-block" : ""}`}
      tabIndex="-1"
      role="dialog"
      aria-labelledby="deleteModalLabel"
      aria-hidden={!showModal}
    >
      <div className="modal-dialog modal-dialog-centered" role="document">
        <div className="modal-content">
          <div className="modal-header bg-warning text-white">
            <h5 className="modal-title" id="deleteModalLabel">
              <FaExclamationTriangle className="me-2" /> Confirm Deletion
            </h5>
            <button
              type="button"
              className="btn-close"
              aria-label="Close"
              onClick={toggleModal}
            ></button>
          </div>
          <div className="modal-body">
            <p>
              {message ||
                "Are you sure you want to delete this? This action cannot be undone."}
            </p>
          </div>
          <div className="modal-footer">
            <button
              type="button"
              className="btn btn-secondary"
              onClick={toggleModal}
            >
              Cancel
            </button>
            <button
              type="button"
              className="btn btn-danger"
              onClick={deleteHandler}
            >
              <FaTrashAlt className="me-2" /> Delete
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
