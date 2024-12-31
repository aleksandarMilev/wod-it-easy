import './Register.css'

export default function Register() {
    return(
        <div className="register-container">
            <div className="register-image">
            </div>
            <div className="register-form-container">
                <h2 className="register-heading">Create Account</h2>
                <form className="register-form">
                    <div className="form-group">
                        <label htmlFor="username" className="form-label">Username</label>
                        <input type="text" id="username" className="form-input" placeholder="Enter your username" required />
                    </div>
                    <div className="form-group">
                        <label htmlFor="email" className="form-label">Email</label>
                        <input type="email" id="email" className="form-input" placeholder="Enter your email" required />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password" className="form-label">Password</label>
                        <input type="password" id="password" className="form-input" placeholder="Enter your password" required />
                    </div>
                    <div className="form-group">
                        <label htmlFor="confirm-password" className="form-label">Confirm Password</label>
                        <input type="password" id="confirm-password" className="form-input" placeholder="Confirm your password" required />
                    </div>
                    <button type="submit" className="register-button">Register</button>
                </form>
            </div>
        </div>
    )
}
