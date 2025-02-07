import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";

import { routes } from "../../../../common/constants";
import { create, update } from "../../../../api/athleteApi";
import { useMessage } from "../../../../contexts/Message";
import { UserContext } from "../../../../contexts/User";

import "./AthleteForm.css";

export default function AthleteForm({ isEditMode = false, athlete = {} }) {
  const navigate = useNavigate();
  const { showMessage } = useMessage();
  const { token, updateAthleteId } = useContext(UserContext);

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

        showMessage(`Your profile was successfully updated!`, true);
        navigate(routes.athlete.mine);
      } else {
        const id = await create(athleteData, token);
        updateAthleteId(id);

        showMessage(
          `You are now an Athlete! Go to 'View Workouts and Join!'`,
          true
        );
        navigate(routes.athlete.mine);
      }
    } catch (error) {
      showMessage(error.message, false);
    }
  };

  return (
    <form className="athlete-form" onSubmit={handleSubmit(onSubmit)}>
      <h2 className="form-title">
        {isEditMode ? "My Profile" : "Athlete Registration"}
      </h2>

      <div className="form-group">
        <label htmlFor="name">Name</label>
        <input id="name" type="text" {...register("name")} />
        {errors.name && <p className="error-message">{errors.name.message}</p>}
      </div>
      <button type="submit" className="submit-button">
        {isEditMode ? "Update" : "Register"}
      </button>
    </form>
  );
}

const validationSchema = Yup.object({
  name: Yup.string()
    .min(2, "Name must be at least 2 characters long")
    .max(50, "Name must not exceed 50 characters")
    .required("Name is required"),
});
