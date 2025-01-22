import { Route, Routes } from 'react-router-dom'

import { routes } from './common/constants'
import { UserContextProvider } from './contexts/User'
import { MessageContextProvider } from './contexts/Message'

import AdminRoute from './components/common/routes/AdminRoute'
import AuthenticatedRoute from './components/common/routes/AuthenticatedRoute'

import Home from './components/home/Home'
import Navigation from './components/common/navigation/Navigation'
import Footer from './components/common/footer/Footer'

import Login from './components/identity/login/Login'
import Register from './components/identity/register/Register'

import WorkoutDetails from './components/workout/details/WorkoutDetails'
import CreateWorkout from './components/workout/create/CreateWorkout'

import NotFound from './components/common/errors/NotFound'
import AccessDenied from './components/common/errors/AccessDenied'
import BadRequest from './components/common/errors/BadRequest'

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

                        <Route path={routes.workout.id} element={<AuthenticatedRoute element={<WorkoutDetails />} />}/>
                        <Route path={routes.workout.create} element={<AdminRoute element={<CreateWorkout />} />}/>

                        <Route path={routes.error.notFound} element={<NotFound />} />
                        <Route path={routes.error.accessDenied} element={<AccessDenied />} />
                        <Route path={routes.error.badRequest} element={<BadRequest />} />
                    </Routes>
                    <Footer/>
                </div>
            </UserContextProvider>
        </MessageContextProvider>
    )
}
