import { Link } from 'react-router-dom'

import { routes } from '../../../../common/constants'
import { formatDate, formatDateAndTime } from '../../../../common/functions'

import './ParticipationListItem.css'

export default function ParticipationListItem({ 
    workoutId,
    workoutName, 
    workoutStartsAtDate, 
    workoutStartsAtTime, 
    joinedAt, 
    status
}) {
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
                    <strong>Joined At:</strong> {formatDateAndTime(joinedAt)}
                </p>
                <p className={`card-text ${status.value === 1 ? 'text-success' : 'text-warning'}`} data-icon="status">
                    <strong>Status:</strong> {status.name}
                </p>
                <Link 
                    to={routes.workout.default + `/${workoutId}`}
                >
                    View
                </Link>
            </div>
        </div>
    )
}