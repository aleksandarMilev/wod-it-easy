import { useContext } from 'react'
import { useParams, Link } from 'react-router-dom'
import { 
    FaCalendarAlt, 
    FaUsers, 
    FaRegFileAlt, 
    FaSwimmer, 
    FaRegClock 
} from 'react-icons/fa'

import { routes } from '../../../common/constants'
import { UserContext } from '../../../contexts/User'
import { formatDate } from '../../../common/functions'
import { useDetails } from '../../../hooks/useWorkout'

import DefaultSpinner from '../../common/default-spinner/DefaultSpinner'

import './WorkoutDetails.css'

export default function WorkoutDetails() {
    const { id } = useParams()
    const { workout, isFetching } = useDetails(id)
    const { isAdmin } = useContext(UserContext)

    if (isFetching || !workout) {
        return <DefaultSpinner />
    }

    return (
        <div className="workout-details card shadow-sm">
            <div className="card-body">
                <div className="workout-details__image-container">
                    <img 
                        src={workout.imageUrl}
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
                        <p className="workout-details__value">{formatDate(workout.startsAtDate)}</p>
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

                {isAdmin && (
                    <div className="workout-details__admin-actions">
                        <Link 
                            to={routes.workout.update + `/${id}`}  
                            className="btn btn-primary me-2"
                        >
                            Update
                        </Link>
                        <button 
                            className="btn btn-danger"
                        >
                            Delete
                        </button>
                    </div>
                )}
            </div>
        </div>
    )
}
