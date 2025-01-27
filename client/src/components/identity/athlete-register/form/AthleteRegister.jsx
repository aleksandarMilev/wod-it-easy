import { useContext } from 'react'
import { useNavigate } from 'react-router-dom'
import { useForm } from 'react-hook-form'
import * as Yup from 'yup'
import { yupResolver } from '@hookform/resolvers/yup'

import { routes } from '../../../../common/constants'
import { create, update } from '../../../../api/athleteApi'
import { useMessage } from '../../../../contexts/Message'
import { UserContext } from '../../../../contexts/User'

import './AthleteForm.css'

export default function AthleteForm({ isEditMode = false, athlete = {} }) {
    const navigate = useNavigate()
    const { showMessage } = useMessage()
    const { token } = useContext(UserContext)

    const {
        register,
        handleSubmit,
        formState: { errors }
    } = useForm({
        defaultValues: {
            name: athlete.name || ''
        },
        resolver: yupResolver(validationSchema)
    })

    const onSubmit = async (data) => {
        const athleteData = {
            name: data.name
        }

        try {
            if (isEditMode) {
                await update(athlete.id, athleteData, token)

                showMessage(`Your profile was successfully updated!`, true)
                navigate(routes.home)
            } else {
                const id = await create(athleteData, token)

                showMessage(`Your profile was successfully created!`, true)
                navigate(routes.home)
            }
        } catch (error) {
            showMessage(error.message, false)
        }
    }

    return (
        <form className="athlete-form" onSubmit={handleSubmit(onSubmit)}>
            <h2 className="form-title">Athlete Registration</h2>

            <div className="form-group">
                <label htmlFor="name">Name</label>
                <input
                    id="name"
                    type="text"
                    {...register('name')}
                />
                {errors.name && (
                    <p className="error-message">{errors.name.message}</p>
                )}
            </div>
            <button type="submit" className="submit-button">
                Register as Athlete
            </button>
        </form>
    )
}

const validationSchema = Yup.object({
    name: Yup
        .string()
        .min(2, 'Name must be at least 2 characters long')
        .max(50, 'Name must not exceed 100 characters')
        .required('Name is required')
})
