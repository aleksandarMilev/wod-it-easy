import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";

import { routes } from "../../../../common/constants";
import { create, update } from "../../../../api/athleteApi";
import { useMessage } from "../../../../contexts/Message";
import { UserContext } from "../../../../contexts/User";

import DeleteConfirmModal from "../../../common/delete-modal/DeleteConfirmModal";

import "./AthleteForm.css";

export default function AthleteForm({
  isEditMode = false,
  athlete = {},
  showModal = false,
  toggleModal = () => {},
  deleteHandler = () => {},
}) {
  const navigate = useNavigate();
  const { showMessage } = useMessage();
  const { token, updateAthleteIdAndName, athleteId } = useContext(UserContext);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    defaultValues: {
      name: athlete.name || "",
    },
    resolver: yupResolver(validationSchema),
  });

  const onSubmit = async (data) => {
    const athleteData = {
      name: data.name,
    };

    try {
      if (isEditMode) {
        await update(athleteData, token);

        updateAthleteIdAndName(athleteId, athleteData.name);
        showMessage(`Your profile was successfully updated!`, true);
        navigate(routes.profile.default);
      } else {
        const id = await create(athleteData, token);
        updateAthleteIdAndName(id, athleteData.name);

        showMessage(
          `You are now an Athlete! Go to 'View Workouts and Join!'`,
          true
        );
        navigate(routes.profile.default);
      }
    } catch (error) {
      showMessage(error.message, false);
    }
  };

  return (
    <>
      <form className="athlete-form" onSubmit={handleSubmit(onSubmit)}>
        <h2 className="form-title">
          {isEditMode ? "My Profile" : "Athlete Registration"}
        </h2>

        <div className="form-group">
          <label htmlFor="name">Name</label>
          <input id="name" type="text" {...register("name")} />
          {errors.name && (
            <p className="error-message">{errors.name.message}</p>
          )}
        </div>
        <button type="submit" className="submit-button">
          {isEditMode ? "Update" : "Register"}
        </button>
      </form>
      {isEditMode && (
        <>
          <button className="delete-button" onClick={toggleModal}>
            Become Regular User
          </button>
          <DeleteConfirmModal
            showModal={showModal}
            toggleModal={toggleModal}
            deleteHandler={deleteHandler}
            message={
              athlete?.upcomingParticipationsCount == 0
                ? null
                : athlete?.upcomingParticipationsCount == 1
                ? `You are registered for one upcoming workout. Deleting your account won't cancel it. To cancel, go to 'Participations' and delete it.`
                : `You are registered for ${athlete?.upcomingParticipationsCount} upcoming workouts. Deleting your account won't cancel them. To cancel, go to 'Participations' and delete them.`
            }
          />
        </>
      )}
    </>
  );
}

const validationSchema = Yup.object({
  name: Yup.string()
    .min(2, "Name must be at least 2 characters long")
    .max(50, "Name must not exceed 50 characters")
    .required("Name is required"),
});
