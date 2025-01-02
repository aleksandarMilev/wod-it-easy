import { Link } from 'react-router-dom'
import { useFormik } from 'formik'
import * as Yup from 'yup'

import { routes } from '../../../common/constants'
import { useLogin } from '../../../hooks/useIdentity'

import './Login.css'

export default function Login() {
    const onLogin = useLogin()

    const formik = useFormik({
        initialValues: {
            credentials: '',
            password: '',
            rememberMe: false 
        },
        validationSchema: Yup.object({
            credentials: Yup
                .string()
                .required('Username or Email is required'),
            password: Yup
                .string()
                .required('Password is required')
        }),
        onSubmit: (values) => onLogin(values)
    })

    return (
        <div className="login-container">
            <div className="login-image"></div>
            <div className="login-form-container">
                <h2 className="login-heading">Welcome Back</h2>
                <form className="login-form" onSubmit={formik.handleSubmit}>
                    <div className="login-form-group">
                        {formik.touched.credentials && formik.errors.credentials ? (
                            <div className="login-form-error">{formik.errors.credentials}</div>
                        ) : null}
                        <label htmlFor="credentials" className="login-form-label">Username or Email</label>
                        <input
                            type="text"
                            id="credentials"
                            className="login-form-input"
                            placeholder="Enter your credentials"
                            {...formik.getFieldProps('credentials')}
                        />
                    </div>
                    <div className="login-form-group">
                        {formik.touched.password && formik.errors.password ? (
                            <div className="login-form-error">{formik.errors.password}</div>
                        ) : null}
                        <label htmlFor="password" className="login-form-label">Password</label>
                        <input
                            type="password"
                            id="password"
                            className="login-form-input"
                            placeholder="Enter your password"
                            {...formik.getFieldProps('password')}
                        />
                    </div>
                    <div className="login-form-group login-remember-me">
                        <label htmlFor="rememberMe" className="login-form-label">
                            <input
                                type="checkbox"
                                id="rememberMe"
                                {...formik.getFieldProps('rememberMe')}
                                className="login-form-checkbox"
                            />
                            Remember Me
                        </label>
                    </div>
                    <button type="submit" className="login-button">Login</button>
                </form>
                <p className="login-register-link">
                    Don't have an account? <Link to={routes.register}>Register here</Link>
                </p>
            </div>
        </div>
    )
}
