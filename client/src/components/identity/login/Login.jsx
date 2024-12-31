import { Link } from 'react-router-dom'

import { routes } from '../../../common/routes'

import './Login.css'

export default function Login() {
    return (
        <div className="login-container">
            <div className="login-image">
            </div>
            <div className="login-form-container">
                <h2 className="login-heading">Welcome Back</h2>
                <form className="login-form">
                    <div className="form-group">
                        <label htmlFor="email" className="form-label">Email</label>
                        <input type="email" id="email" className="form-input" placeholder="Enter your email" required />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password" className="form-label">Password</label>
                        <input type="password" id="password" className="form-input" placeholder="Enter your password" required />
                    </div>
                    <button type="submit" className="login-button">Login</button>
                </form>
                <p className="register-link">
                    Don't have an account? <Link to={routes.register}>Register here</Link>
                </p>
            </div>
        </div>
    )
}
