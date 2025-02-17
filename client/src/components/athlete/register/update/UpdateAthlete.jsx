import { useContext } from "react";

import { useMine } from "../../../../hooks/useAthlete";
import { UserContext } from "../../../../contexts/User";

import AthleteForm from "../form/AthleteForm";
import DefaultSpinner from "../../../common/default-spinner/DefaultSpinner";

export default function UpdateAthlete() {
  const { token } = useContext(UserContext);
  const { athlete, isFetching } = useMine(token);

  if (isFetching || !athlete) {
    return <DefaultSpinner />;
  }

  return <AthleteForm isEditMode={true} athlete={athlete} />;
}
