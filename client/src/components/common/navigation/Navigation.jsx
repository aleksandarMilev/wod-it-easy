import { Link } from 'react-router-dom'

import { routes } from '../../../common/constants'

import './Navigation.css'

export default function Navigation(){
    return(
        <header className="header">
            <Link to={routes.home}>
                <div className="logo">
                    <h1>Wod It Easy</h1>
                    <p>Your CrossFit Journey Starts Here</p>
                </div>
            </Link>
            <nav className="nav-bar">
                <ul>
                    <li>
                        <Link to={routes.login}>Login</Link>
                    </li>
                    <li>
                        <Link to={routes.register}>Register</Link>
                    </li>
                </ul>
            </nav>
        </header>
    )
}