import { Route, Routes } from 'react-router-dom'

import Footer from './components/common/footer/Footer'
import Home from './components/home/Home'
import Navigation from './components/common/navigation/Navigation'
import Login from './components/identity/login/Login'
import Register from './components/identity/register/Register'

import { routes } from './common/routes'

import './App.css'

export default function App() {
    return (
        <div className="homepage-container">
            <Navigation/>
            <Routes>
                <Route path={routes.home} element={<Home/>} />
                <Route path={routes.login} element={<Login/>} />
                <Route path={routes.register} element={<Register/>} />
            </Routes>
            <Footer/>
        </div>
    )
}
