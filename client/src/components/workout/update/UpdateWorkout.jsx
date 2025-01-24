import { useParams } from 'react-router-dom'

import { useDetails } from '../../../hooks/useWorkout'

import WorkoutForm from '../form/WorkoutForm'
import DefaultSpinner from '../../common/default-spinner/DefaultSpinner'

export default function UpdateWorkout() {
    const { id } = useParams()
    const { workout, isFetching } = useDetails(id)

    if(isFetching || !workout){
        return(
            <DefaultSpinner />
        )
    }

    return(
        <WorkoutForm isEditMode={true} workout={workout} />
    )
}
