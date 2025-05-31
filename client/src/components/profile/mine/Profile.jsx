import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { FaUser } from "react-icons/fa";
import { Link } from "react-router-dom";

import { routes } from "../../../common/constants";
import { useMine } from "../../../hooks/useProfile";
import { remove as deleteProfle } from "../../../api/profileApi";
import { useMessage } from "../../../contexts/Message";
import { UserContext } from "../../../contexts/User";

import DeleteConfirmModal from "../../common/delete-modal/DeleteConfirmModal";
import DefaultSpinner from "../../common/default-spinner/DefaultSpinner";

import "./Profile.css";

export default function Profile() {
  const navigate = useNavigate();
  const { showMessage } = useMessage();
  const { token, setHasProfile, username, athleteName, email } =
    useContext(UserContext);

  const { isFetching, profile } = useMine(token);

  const [showModal, setShowModal] = useState(false);
  const toggleModal = () => setShowModal((prev) => !prev);

  const deleteHandler = async () => {
    if (showModal) {
      try {
        await deleteProfle(token);

        setHasProfile(false);
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

  if (isFetching || !profile) {
    return <DefaultSpinner />;
  } else {
    console.log("Profile Data:", profile);
  }

  const displayName = athleteName || "Not an Athlete";
  const displayBio = profile.bio || "Hard Work Pays Off";
  const avatarUrl =
    profile.avatarUrl ||
    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSVHZh9LmC_ScCXjK7rEmUHgcc3tHQJyBMa8Q&s";

  return (
    <>
      <div className="athlete-profile-container">
        <div className="athlete-profile-card">
          <div className="athlete-icon-container">
            {profile.avatarUrl ? (
              <img src={avatarUrl} alt="Profile" className="athlete-avatar" />
            ) : (
              <FaUser className="athlete-icon" />
            )}
          </div>
          <h2 className="athlete-name">{displayName}</h2>
          <div className="profile-details">
            <p className="profile-username">Username: {username}</p>
            <p className="profile-email">Email: {email}</p>
            <p className="profile-bio">Bio: {displayBio}</p>
          </div>
          <div className="profile-actions">
            <Link to={routes.profile.update} className="update-button">
              Update
            </Link>
            <button className="delete-button" onClick={toggleModal}>
              Delete
            </button>
          </div>
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
