import { useContext, useState, useEffect } from 'react'
import { Link } from 'react-router-dom'

import { cancel } from '../../../../api/participationApi'
import { UserContext } from '../../../../contexts/User'
import { useMessage } from '../../../../contexts/Message'
import { routes, participationStatuses } from '../../../../common/constants'
import { formatDate, formatDateAndTime } from '../../../../common/functions'

import './ParticipationListItem.css'

export default function ParticipationListItem({
    id,
    workoutId,
    workoutName, 
    workoutStartsAtDate, 
    workoutStartsAtTime, 
    joinedAt,
    modifiedOn,
    status: initialStatus
}) {
    const { showMessage } = useMessage()
    const { token } = useContext(UserContext)

    const [status, setStatus] = useState(initialStatus)

    useEffect(() => {
        setStatus(initialStatus)
    }, [initialStatus])

    const isJoined = status.toLowerCase() === participationStatuses.joined.toLowerCase()
    const isLeft = status.toLowerCase() === participationStatuses.left.toLowerCase()

    const cancelHandler = async () => {
        const success = await cancel(id, token)

        if (success) {
            setStatus(participationStatuses.left)
            showMessage('You have successfully canceled this workout!', true)
        } else {
            showMessage('Something went wrong while canceling this workout, please try again!', false)
        }
    }

    return (
        <div className="participation-list-item-card card mb-3 shadow-sm">
            <div className="card-body">
                <h5 className="card-title">{workoutName}</h5>
                <p className="card-text" data-icon="date">
                    <strong>Start Date:</strong> {formatDate(workoutStartsAtDate)}
                </p>
                <p className="card-text" data-icon="time">
                    <strong>Start Time:</strong> {workoutStartsAtTime.slice(0, 5)}
                </p>
                <p className="card-text" data-icon="joined">
                    <strong>{isJoined ? 'Joined At:' : 'Left At:'}</strong> 
                    {formatDateAndTime(modifiedOn || joinedAt)}
                </p>
                {status && (
                    <p 
                        data-icon="status"
                        className={`card-text ${isJoined ? 'text-success' : 'text-warning'}`} 
                    >
                        <strong>Status:</strong> {status}
                    </p>
                )}
                <Link to={routes.workout.default + `/${workoutId}`}>
                    View
                </Link>
                {isJoined && <button onClick={cancelHandler}>Cancel</button>}
                {isLeft && <button onClick={() => setStatus(participationStatuses.joined)}>Join Again</button>}
            </div>
        </div>
    )
}
