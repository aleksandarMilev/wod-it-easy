import { useState, useEffect, useContext } from 'react'
import { useNavigate } from 'react-router-dom'

import * as api from '../api/workout'
import { UserContext } from '../contexts/User'
import { routes, errorMessages } from '../common/constants'

export function useDetails(id){
    const navigate = useNavigate()
    const { token } = useContext(UserContext)
    
    const [workout, setWorkout] = useState(null)
    const [isFetching, setIsFetching] = useState(false)

    useEffect(() => {
        async function fetchData() {
            try {
                setIsFetching(true)
                setWorkout(await api.details(id, token))
            } catch {
                navigate(routes.error.notFound, { state: { message: errorMessages.workout.notFound } })
            } finally {
                setIsFetching(false)
            }
        }

        fetchData()
    }, [token, navigate])

    return { workout, isFetching }
}
