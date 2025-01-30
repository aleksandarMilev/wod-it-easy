import { useState, useEffect, useContext } from 'react'
import { useParams, useNavigate, Link } from 'react-router-dom'
import { 
    FaCalendarAlt,
    FaUsers,
    FaRegFileAlt,
    FaSwimmer,
    FaRegClock
} from 'react-icons/fa'

import { formatDate } from '../../../common/functions'
import { remove as deleteWorkout } from '../../../api/workoutApi'
import { join, leave } from '../../../api/participationApi'
import { routes } from '../../../common/constants'
import { useDetails } from '../../../hooks/useWorkout'
import { UserContext } from '../../../contexts/User'
import { useMessage } from '../../../contexts/Message'
import { useIsParticipant } from '../../../hooks/useParticipation.js'

import DefaultSpinner from '../../common/default-spinner/DefaultSpinner'
import DeleteConfirmModal from '../../common/delete-modal/DeleteConfirmModal'

import './WorkoutDetails.css'

export default function WorkoutDetails() {
    const { id } = useParams()
    const navigate = useNavigate()
    const { showMessage } = useMessage()
    const {
        isAdmin,
        isAthlete,
        athleteId, 
        token } = useContext(UserContext)

    const [showModal, setShowModal] = useState(false)
    const toggleModal = () => setShowModal(prev => !prev)

    const { workout, isFetching } = useDetails(id)
    const { isParticipant, setIsParticipant } = useIsParticipant(athleteId, id, token)

    const [participantsCount, setParticipantsCount] = useState(0)

    useEffect(() => {
        if (workout) {
            setParticipantsCount(workout.currentParticipantsCount)
        }
    }, [workout])

    const deleteHandler = async () => {
        if(showModal){
            try {
                await deleteWorkout(id, token)

                navigate(routes.workout.search)
                showMessage(`${workout.name || 'This workout'} was successfully deleted!`, true)
            } catch(error) {
                showMessage(error.message, false)
            }
            
            toggleModal()
        } else {
            toggleModal()
        }
    }

    const joinHandler = async () => {
        const participation = {
            workoutId: id,
            athleteId: athleteId
        }

        const success = await join(participation, token)

        if(success) {
            setIsParticipant(true)
            setParticipantsCount(prev => prev + 1)

            showMessage('You have successfully joined this workout! Go to \'Participations\' for more details.', true)
        } else {
            showMessage('Something went wrong while joining this workout, please, try again!', false)
        }
    }

    const leaveHandler = async () => {
        const success = await leave(athleteId, id, token)

        if(success) {
            setIsParticipant(false)
            setParticipantsCount(prev => prev - 1)

            showMessage('You have successfully left this workout! Go to \'Participations\' for more details.', true)
        } else {
            showMessage('Something went wrong while removing you from this workout, please, try again!', false)
        }
    }

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
                        <p className="workout-details__value">{participantsCount}</p>
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
                            onClick={toggleModal}
                        >
                            Delete
                        </button>
                    </div>
                )}

                {isAthlete && !isAdmin && !isParticipant && (
                    <div className="workout-details__athlete-actions">
                        <button 
                            className="btn btn-success"
                            onClick={joinHandler}
                        >
                            Join
                        </button>
                    </div>
                )}

                {isAthlete && !isAdmin && isParticipant && (
                    <div className="workout-details__athlete-actions">
                        <button 
                            className="btn btn-success"
                            onClick={leaveHandler}
                        >
                            Leave
                        </button>
                    </div>
                )}


                <DeleteConfirmModal 
                    showModal={showModal}
                    toggleModal={toggleModal}
                    deleteHandler={deleteHandler}
                />
            </div>
        </div>
    )
}
