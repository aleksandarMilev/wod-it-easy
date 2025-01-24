import { useContext } from 'react'
import { useNavigate } from 'react-router-dom'
import { useForm } from 'react-hook-form'
import * as Yup from 'yup'
import { yupResolver } from '@hookform/resolvers/yup'

import { formatDate } from '../../../common/functions'
import { useMessage } from '../../../contexts/Message'
import { workoutTypes } from '../../../common/constants'
import { create, update } from '../../../api/workout'
import { UserContext } from '../../../contexts/User'
import { routes } from '../../../common/constants'

import './WorkoutForm.css'

export default function WorkoutForm({ isEditMode = false, workout = {} }) {
    const navigate = useNavigate()
    const { showMessage } = useMessage()
    const { token } = useContext(UserContext)

    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm({
        defaultValues: {
            name: workout.name || '',
            imageUrl: workout.imageUrl || '',
            description: workout.description || '',
            maxParticipantsCount: workout.maxParticipantsCount || 1,
            startsAtDate: isEditMode 
                ? workout.startsAtDate.split('T')[0] 
                : '',
            startsAtTime: workout.startsAtTime || '',
            type: isEditMode && workout.type 
                ? mapTypeToNumber(workout.type) 
                : workoutTypes[0].value
        },
        resolver: yupResolver(validationSchema)
    })

    const onSubmit = async (data) => {
        const workoutData = {
            name: data.name,
            imageUrl: data.imageUrl || defaultImageUrl,
            description: data.description,
            maxParticipantsCount: parseInt(data.maxParticipantsCount),
            startsAtDate: data.startsAtDate,
            startsAtTime: data.startsAtTime,
            type: parseInt(data.type) 
        }

        try {
            if (isEditMode) {
                await update(workout.id, workoutData, token)
                showMessage(`Workout updated!`, true)
            } else {
                await create(workoutData, token)
                showMessage(`Workout created!`, true)
            }

            navigate(routes.home)
        } catch (error) {
            showMessage(error.message, false)
        }
    }

    return (
        <form className="workout-form" onSubmit={handleSubmit(onSubmit)}>
            <h2 className="form-title">
                {isEditMode ? 'Edit Workout' : 'Create Workout'}
            </h2>

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
                <label htmlFor="imageUrl">Image URL (Optional)</label>
                <input
                    id="imageUrl"
                    type="url"
                    {...register('imageUrl')}
                />
                {errors.imageUrl && <p className="error-message">{errors.imageUrl.message}</p>}
            </div>

            <div className="form-group">
                <label htmlFor="description">Description</label>
                <textarea
                    id="description"
                    {...register('description')}
                ></textarea>
                {errors.description && <p className="error-message">{errors.description.message}</p>}
            </div>

            <div className="form-group">
                <label htmlFor="maxParticipantsCount">Max Participants</label>
                <input
                    id="maxParticipantsCount"
                    type="number"
                    {...register('maxParticipantsCount')}
                />
                {errors.maxParticipantsCount && <p className="error-message">{errors.maxParticipantsCount.message}</p>}
            </div>

            <div className="form-group">
                <label htmlFor="startsAtDate">Start Date</label>
                <input
                    id="startsAtDate"
                    type="date"
                    {...register('startsAtDate')}
                />
                {errors.startsAtDate && <p className="error-message">{errors.startsAtDate.message}</p>}
            </div>

            <div className="form-group">
                <label htmlFor="startsAtTime">Start Time</label>
                <input
                    id="startsAtTime"
                    type="time"
                    {...register('startsAtTime')}
                />
                {errors.startsAtTime && <p className="error-message">{errors.startsAtTime.message}</p>}
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
                {isEditMode ? 'Update Workout' : 'Create Workout'}
            </button>
        </form>
    )
}

const minStartDate = new Date(Date.now() - 60000)
const maxStartDate = new Date(Date.now() + 7 * 24 * 60 * 60 * 1000)

const validationSchema = Yup.object({
    name: Yup
        .string()
        .min(
            2,
            "Name must be at least 2 characters long"
        )
        .max(
            100,
            "Name must not exceed 100 characters"
        )
        .required("Name is required"),
    imageUrl: Yup
        .string()
        .url("Image URL must be a valid URL")
        .nullable(),
    description: Yup
        .string()
        .min(
            2,
            "Description must be at least 2 characters long"
        )
        .max(
            500,
            "Description must not exceed 500 characters"
        )
        .required("Description is required"),
    maxParticipantsCount: Yup
        .number()
        .min(
            1,
            "Participants count must be at least 1"
        )
        .max(
            15,
            "Participants count must not exceed 15"
        )
        .required("Participants count is required"),
    startsAtDate: Yup
        .date()
        .min(
            minStartDate,
            `Start Date must be no earlier than ${formatDate(minStartDate)}`
        )
        .max(
            maxStartDate,
            `Start Date must be no later than ${formatDate(maxStartDate)}`
        )
        .required(`Start Date is required and must be between ${formatDate(minStartDate)} and ${formatDate(maxStartDate)}`)
        .typeError(`Start Date is required and must be between ${formatDate(minStartDate)} and ${formatDate(maxStartDate)}`),
    startsAtTime: Yup
        .string()
        .required("Start Time is required"),
    type: Yup
        .number()
        .required("Type is required")
})

const mapTypeToNumber = type => {
    const foundType = workoutTypes.find(t => t.label.toString() === type)

    return foundType 
        ? foundType.value 
        : workoutTypes[0].value
}

const defaultImageUrl = 'https://media.licdn.com/dms/image/D4E12AQFGe4i-pReXSQ/article-cover_image-shrink_720_1280/0/1707604455346?e=2147483647&v=beta&t=HosnJc4I3061iujawviv6rc4R6aP4-vmq9Kq7KHviIg';
