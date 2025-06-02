import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";

import { routes } from "../../../../common/constants";
import { useMine } from "../../../../hooks/useAthlete";
import { UserContext } from "../../../../contexts/User";
import { remove as deleteAthlete } from "../../../../api/athleteApi";
import { useMessage } from "../../../../contexts/Message";

import AthleteForm from "../form/AthleteForm";
import DefaultSpinner from "../../../common/default-spinner/DefaultSpinner";

export default function UpdateAthlete() {
  const navigate = useNavigate();
  const { showMessage } = useMessage();
  const { token, updateAthleteIdAndName } = useContext(UserContext);
  const { athlete, isFetching } = useMine(token);

  const [showModal, setShowModal] = useState(false);
  const toggleModal = () => setShowModal((prev) => !prev);

  const deleteHandler = async () => {
    if (showModal) {
      try {
        await deleteAthlete(token);
        updateAthleteIdAndName(0, "");

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
    <AthleteForm
      isEditMode={true}
      athlete={athlete}
      showModal={showModal}
      toggleModal={toggleModal}
      deleteHandler={deleteHandler}
    />
  );
}
