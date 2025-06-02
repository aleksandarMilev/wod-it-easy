import { useContext } from "react";

import { useMine } from "../../../hooks/useProfile";
import { UserContext } from "../../../contexts/User";

import ProfileForm from "../form/ProfileForm";
import DefaultSpinner from "../../common/default-spinner/DefaultSpinner";

export default function UpdateProfile() {
  const { token } = useContext(UserContext);
  const { profile, isFetching } = useMine(token);

  if (isFetching || !profile) {
    return <DefaultSpinner />;
  }

  return <ProfileForm isEditMode={true} profile={profile} />;
}
