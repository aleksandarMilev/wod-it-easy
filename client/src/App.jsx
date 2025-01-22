import { Route, Routes } from 'react-router-dom'

import { routes } from './common/constants'
import { UserContextProvider } from './contexts/User'
import { MessageContextProvider } from './contexts/Message'

import AdminRoute from './components/common/routes/AdminRoute'

import Navigation from './components/common/navigation/Navigation'
import Footer from './components/common/footer/Footer'

import Home from './components/home/Home'
import Login from './components/identity/login/Login'
import Register from './components/identity/register/Register'

import CreateWorkout from './components/workout/create/CreateWorkout'

import './App.css'

export default function App() {
    return (
        <MessageContextProvider>
            <UserContextProvider>
                <div className="homepage-container">
                    <Navigation/>
                    <Routes>
                        <Route path={routes.home} element={<Home />} />
                        <Route path={routes.login} element={<Login />} />
                        <Route path={routes.register} element={<Register />} />

                        <Route path={routes.workout.create} element={<AdminRoute element={<CreateWorkout />} />}/>
                    </Routes>
                    <Footer/>
                </div>
            </UserContextProvider>
        </MessageContextProvider>
    )
}
