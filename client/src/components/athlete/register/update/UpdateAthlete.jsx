import { useMine } from "../../../../hooks/useAthlete";

import AthleteForm from "../form/AthleteForm";
import DefaultSpinner from "../../../common/default-spinner/DefaultSpinner";

export default function UpdateAthlete() {
  const { athlete, isFetching } = useMine();

  if (isFetching || !athlete) {
    return <DefaultSpinner />;
  }

  return <AthleteForm isEditMode={true} athlete={athlete} />;
}
