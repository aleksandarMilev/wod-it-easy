import { useContext } from 'react'
import { useNavigate } from 'react-router-dom'
import { useForm } from 'react-hook-form'
import * as Yup from 'yup'
import { yupResolver } from '@hookform/resolvers/yup'

import { useMessage } from '../../../contexts/Message'
import { workoutTypes } from '../../../common/constants'
import { create } from '../../../api/workout'
import { UserContext } from '../../../contexts/User'
import { routes } from '../../../common/constants'

import './CreateWorkout.css'

export default function CreateWorkout() {
    const navigate = useNavigate()
    const { showMessage } = useMessage()
    const { token } = useContext(UserContext)

    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm({
        resolver: yupResolver(validationSchema)
    })

    const onSubmit = async (data) => {
        const workout = {
            name: data.name,
            description: data.description,
            maxParticipantsCount: parseInt(data.maxParticipantsCount, 10),
            startsAtDate: data.startsAtDate,
            startsAtTime: data.startsAtTime,
            type: parseInt(data.type)
        }

        try {
            await create(workout, token)

            showMessage(`Workout created!`, true)
            navigate(routes.home)
        } catch (error) {
            showMessage(error.message, false)
        }
    }

    return (
        <form className="create-workout-form" onSubmit={handleSubmit(onSubmit)}>
            <h2 className="form-title">Create Workout</h2>

            <div className="form-group">
                <label htmlFor="name">Name</label>
                <input
                    id="name"
                    type="text"
                    {...register('name')}
                />
                {errors.name && <p className="error-message">{errors.name.message}</p>}
            </div>

            <div className="form-group">
                <label htmlFor="description">Description</label>
                <textarea
                    id="description"
                    {...register('description')}
                ></textarea>
                {errors.description && (
                    <p className="error-message">{errors.description.message}</p>
                )}
            </div>

            <div className="form-group">
                <label htmlFor="maxParticipantsCount">Max Participants</label>
                <input
                    id="maxParticipantsCount"
                    type="number"
                    {...register('maxParticipantsCount')}
                />
                {errors.maxParticipantsCount && (
                    <p className="error-message">{errors.maxParticipantsCount.message}</p>
                )}
            </div>

            <div className="form-group">
                <label htmlFor="startsAtDate">Start Date</label>
                <input
                    id="startsAtDate"
                    type="date"
                    {...register('startsAtDate')}
                />
                {errors.startsAtDate && (
                    <p className="error-message">{errors.startsAtDate.message}</p>
                )}
            </div>

            <div className="form-group">
                <label htmlFor="startsAtTime">Start Time</label>
                <input
                    id="startsAtTime"
                    type="time"
                    {...register('startsAtTime')}
                />
                {errors.startsAtTime && (
                    <p className="error-message">{errors.startsAtTime.message}</p>
                )}
            </div>

            <div className="form-group">
                <label htmlFor="type">Type</label>
                <select
                    id="type"
                    {...register('type')}
                >
                    {workoutTypes.map(type => (
                        <option key={type.value} value={type.value}>
                            {type.label}
                        </option>
                    ))}
                </select>
                {errors.type && <p className="error-message">{errors.type.message}</p>}
            </div>

            <button type="submit" className="submit-button">
                Create Workout
            </button>
        </form>
    )
}

const validationSchema = Yup.object({
    name: Yup
        .string()
        .min(2)
        .max(100)
        .required(),
    description: Yup
        .string()
        .min(2)
        .max(500)
        .required(),
    maxParticipantsCount: Yup
        .number()
        .min(1)
        .max(15),
    startsAtDate: Yup
        .date()
        .min(new Date(Date.now() - 60000)) //one min ago
        .max(new Date(Date.now() + 7 * 24 * 60 * 60 * 1000)), //7 days ahead
    startsAtTime: Yup
        .string()
        .required(),
    type: Yup
        .number()
        .required()
})
