import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { FaUser } from "react-icons/fa";
import { Link } from "react-router-dom";

import { routes } from "../../../common/constants";
import { useMine } from "../../../hooks/useAthlete";
import { remove as deleteAthlete } from "../../../api/athleteApi";
import { useMessage } from "../../../contexts/Message";
import { UserContext } from "../../../contexts/User";

import DeleteConfirmModal from "../../common/delete-modal/DeleteConfirmModal";
import DefaultSpinner from "../../common/default-spinner/DefaultSpinner";

import "./AthleteProfile.css";

export default function AthleteProfile() {
  const navigate = useNavigate();
  const { showMessage } = useMessage();
  const { token, updateAthleteId } = useContext(UserContext);

  const { isFetching, athlete } = useMine();

  const [showModal, setShowModal] = useState(false);
  const toggleModal = () => setShowModal((prev) => !prev);

  const deleteHandler = async () => {
    if (showModal) {
      try {
        await deleteAthlete(token);
        updateAthleteId(null);

        showMessage("Your profile was successfully deleted!", true);
        navigate(routes.home);
      } catch (error) {
        showMessage(error.message, false);
      }

      toggleModal();
    } else {
      toggleModal();
    }
  };

  if (isFetching || !athlete) {
    return <DefaultSpinner />;
  }

  return (
    <>
      <div className="athlete-profile-container">
        <div className="athlete-profile-card">
          <div className="athlete-icon-container">
            <FaUser className="athlete-icon" />
          </div>
          <h1 className="athlete-name">{athlete.name}</h1>
          <Link to={routes.athlete.update} className="update-button">
            Update
          </Link>
          <button className="delete-button" onClick={toggleModal}>
            Delete
          </button>
        </div>
      </div>
      <DeleteConfirmModal
        showModal={showModal}
        toggleModal={toggleModal}
        deleteHandler={deleteHandler}
      />
    </>
  );
}
