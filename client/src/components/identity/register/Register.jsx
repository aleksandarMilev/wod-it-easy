import { useNavigate } from 'react-router-dom'
import { useFormik } from 'formik'
import * as Yup from 'yup'

import { routes } from '../../../common/constants'
import { useRegister } from '../../../hooks/useIdentity'

import './Register.css'

export default function Register() {
    const navigate = useNavigate()
    const onRegister = useRegister()

    const formik = useFormik({
        initialValues: {
            username: '',
            email: '',
            password: '',
            confirmPassword: ''
        },
        validationSchema: Yup.object({
            username: Yup
                .string()
                .required('Username is required'),
            email: Yup
                .string()
                .email('Invalid email address')
                .required('Email is required'),
            password: Yup
                .string()
                .min(6, 'Password must be at least 6 characters')
                .required('Password is required'),
            confirmPassword: Yup
                .string()
                .oneOf([Yup.ref('password'), null], 'Passwords must match')
                .required('Confirm Password is required')
        }),
        onSubmit: async (values) => {
            await onRegister(values)
            navigate(routes.registrationChoice)
        }
    })

    return (
        <div className="register-container">
            <div className="register-image"></div>
            <div className="register-form-container">
                <h2 className="register-heading">Create Account</h2>
                <form className="register-form" onSubmit={formik.handleSubmit}>
                    <div className="register-form-group">
                        <label htmlFor="username" className="register-form-label">Username</label>
                        <input
                            type="text"
                            id="username"
                            className="register-form-input"
                            placeholder="Enter your username"
                            {...formik.getFieldProps('username')}
                        />
                        {formik.touched.username && formik.errors.username ? (
                            <div className="register-form-error">{formik.errors.username}</div>
                        ) : null}
                    </div>
                    <div className="register-form-group">
                        <label htmlFor="email" className="register-form-label">Email</label>
                        <input
                            type="email"
                            id="email"
                            className="register-form-input"
                            placeholder="Enter your email"
                            {...formik.getFieldProps('email')}
                        />
                        {formik.touched.email && formik.errors.email ? (
                            <div className="register-form-error">{formik.errors.email}</div>
                        ) : null}
                    </div>
                    <div className="register-form-group">
                        <label htmlFor="password" className="register-form-label">Password</label>
                        <input
                            type="password"
                            id="password"
                            className="register-form-input"
                            placeholder="Enter your password"
                            {...formik.getFieldProps('password')}
                        />
                        {formik.touched.password && formik.errors.password ? (
                            <div className="register-form-error">{formik.errors.password}</div>
                        ) : null}
                    </div>
                    <div className="register-form-group">
                        <label htmlFor="confirmPassword" className="register-form-label">Confirm Password</label>
                        <input
                            type="password"
                            id="confirmPassword"
                            className="register-form-input"
                            placeholder="Confirm your password"
                            {...formik.getFieldProps('confirmPassword')}
                        />
                        {formik.touched.confirmPassword && formik.errors.confirmPassword ? (
                            <div className="register-form-error">{formik.errors.confirmPassword}</div>
                        ) : null}
                    </div>
                    <button type="submit" className="register-button">Register</button>
                </form>
            </div>
        </div>
    )
}
