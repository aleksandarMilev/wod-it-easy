import { useContext } from "react";
import { useNavigate } from "react-router-dom";
import { useForm } from "react-hook-form";
import * as Yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";

import { routes } from "../../../common/constants";
import { create, update } from "../../../api/profileApi";
import { useMessage } from "../../../contexts/Message";
import { UserContext } from "../../../contexts/User";

import "./ProfileForm.css";

export default function ProfileForm({ isEditMode = false, profile = {} }) {
  const navigate = useNavigate();
  const { showMessage } = useMessage();
  const { token, setHasProfile } = useContext(UserContext);

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm({
    defaultValues: {
      avatarUrl: profile.avatarUrl || "",
      bio: profile.bio || "",
    },
    resolver: yupResolver(validationSchema),
  });

  const onSubmit = async (data) => {
    const profileData = {
      avatarUrl: data.avatarUrl,
      bio: data.bio,
    };

    try {
      if (isEditMode) {
        await update(profileData, token);

        showMessage(`Your profile was successfully updated!`, true);
        navigate(routes.profile.default);
      } else {
        await create(profileData, token);

        setHasProfile(true);
        showMessage("You have successfully configured your profile!", true);
        navigate(routes.profile.default);
      }
    } catch (error) {
      showMessage(error.message, false);
    }
  };

  return (
    <form className="athlete-form" onSubmit={handleSubmit(onSubmit)}>
      <h2 className="form-title">
        {isEditMode ? "Update Profile" : "Create Profile"}
      </h2>

      <div className="form-group">
        <label htmlFor="name">Avatar URL</label>
        <input id="name" type="text" {...register("avatarUrl")} />
        {errors.name && (
          <p className="error-message">{errors.avatarUrl.message}</p>
        )}
      </div>

      <div className="form-group">
        <label htmlFor="name">Bio</label>
        <input id="name" type="text" {...register("bio")} />
        {errors.name && <p className="error-message">{errors.bio.message}</p>}
      </div>

      <button type="submit" className="submit-button">
        {isEditMode ? "Update" : "Create"}
      </button>
    </form>
  );
}

const validationSchema = Yup.object({
  avatarUrl: Yup.string()
    .min(10, `Avatar URL must be at least 10 characters.`)
    .max(2_048, `Avatar URL must be at most 2 048 characters.`)
    .required("Avatar URL is required."),
  bio: Yup.string()
    .min(10, `Bio must be at least 10 characters.`)
    .max(5_000, `Bio must be at most 5 000 characters.`)
    .required("Bio is required."),
});
