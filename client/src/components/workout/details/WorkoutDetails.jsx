import { useParams } from 'react-router-dom'
import { 
    FaCalendarAlt, 
    FaUsers, 
    FaRegFileAlt, 
    FaSwimmer, 
    FaRegClock 
} from 'react-icons/fa'

import { useDetails } from '../../../hooks/useWorkout'
import DefaultSpinner from '../../common/default-spinner/DefaultSpinner'

import './WorkoutDetails.css'

export default function WorkoutDetails() {
    const { id } = useParams()
    const { workout, isFetching } = useDetails(id)

    if (isFetching || !workout) {
        return (
            <DefaultSpinner />
        )
    }

    return (
        <div className="workout-details card shadow-sm">
            <div className="card-body">
                <div className="workout-details__image-container">
                    <img 
                        src="/src/assets/workout.jpg" 
                        alt="Workout" 
                        className="workout-details__image" 
                    />
                </div>
                <h2 className="workout-details__title">{workout.name}</h2> 
    
                <div className="workout-details__section">
                    <FaRegFileAlt className="workout-details__icon" />
                    <div>
                        <strong className="workout-details__label">Description:</strong>
                        <p className="workout-details__value">{workout.description}</p>
                    </div>
                </div>
    
                <div className="workout-details__section">
                    <FaUsers className="workout-details__icon" />
                    <div>
                        <strong className="workout-details__label">Max Participants:</strong>
                        <p className="workout-details__value">{workout.maxParticipantsCount}</p>
                    </div>
                </div>
    
                <div className="workout-details__section">
                    <FaUsers className="workout-details__icon" />
                    <div>
                        <strong className="workout-details__label">Current Participants:</strong>
                        <p className="workout-details__value">{workout.currentParticipantsCount}</p>
                    </div>
                </div>
    
                <div className="workout-details__section">
                    <FaCalendarAlt className="workout-details__icon" />
                    <div>
                        <strong className="workout-details__label">Start Date:</strong>
                        <p className="workout-details__value">{new Date(workout.startsAtDate).toLocaleDateString('en-GB', {
                            day: '2-digit',
                            month: 'long',
                            year: 'numeric'
                        })}</p>
                    </div>
                </div>
    
                <div className="workout-details__section">
                    <FaRegClock className="workout-details__icon" />
                    <div>
                        <strong className="workout-details__label">Start Time:</strong>
                        <p className="workout-details__value">{workout.startsAtTime.slice(0, 5)}</p>
                    </div>
                </div>
    
                <div className="workout-details__section">
                    <FaSwimmer className="workout-details__icon" />
                    <div>
                        <strong className="workout-details__label">Type:</strong>
                        <p className="workout-details__value">{workout.type}</p>
                    </div>
                </div>
            </div>
        </div>
    )
    
}
