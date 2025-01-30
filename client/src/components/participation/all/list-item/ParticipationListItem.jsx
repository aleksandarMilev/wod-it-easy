import { useContext, useState, useEffect } from 'react'
import { Link } from 'react-router-dom'

import { cancel, reJoin } from '../../../../api/participationApi'
import { UserContext } from '../../../../contexts/User'
import { useMessage } from '../../../../contexts/Message'
import { routes, participationStatuses } from '../../../../common/constants'
import { formatDate, formatDateAndTime, jsNow } from '../../../../common/functions'

import './ParticipationListItem.css'

export default function ParticipationListItem({
    id,
    workoutId,
    workoutName, 
    workoutStartsAtDate, 
    workoutStartsAtTime, 
    joinedAt: initialJoinedAt,
    modifiedOn: initialModifiedOn,
    status: initialStatus
}) {
    const { showMessage } = useMessage()
    const { token } = useContext(UserContext)

    const [status, setStatus] = useState(initialStatus)
    const [joinedAt, setJoinedAt] = useState(initialJoinedAt)
    const [modifiedOn, setModifiedOn] = useState(initialModifiedOn)

    useEffect(() => {
        setStatus(initialStatus)
        setJoinedAt(initialJoinedAt)
        setModifiedOn(initialModifiedOn)
    }, [initialStatus, initialJoinedAt, initialModifiedOn])

    const isJoined = status.toLowerCase() === participationStatuses.joined.toLowerCase()
    const isLeft = status.toLowerCase() === participationStatuses.left.toLowerCase()

    const cancelHandler = async () => {
        const success = await cancel(id, token)

        if (success) {
            setStatus(participationStatuses.left)
            setModifiedOn(jsNow())

            showMessage('You have successfully canceled this workout!', true)
        } else {
            showMessage('Something went wrong while canceling this workout, please try again!', false)
        }
    }

    const reJoinHandler = async () => {
        const success = await reJoin(id, token)

        if (success) {
            setStatus(participationStatuses.joined)
            setModifiedOn(jsNow())

            showMessage('You are again a participant in this workout!', true)
        } else {
            showMessage('Something went wrong while adding you to this workout, please try again!', false)
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
                {isLeft && <button onClick={reJoinHandler}>Join Again</button>}
            </div>
        </div>
    )
}
