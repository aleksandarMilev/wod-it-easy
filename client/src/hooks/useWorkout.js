import { useState, useEffect, useContext } from 'react'
import { useNavigate } from 'react-router-dom'

import * as api from '../api/workout'
import { UserContext } from '../contexts/User'
import { routes } from '../common/constants'

export function useDetails(id){
    const navigate = useNavigate()
    const { token } = useContext(UserContext)
    
    const [workout, setWorkout] = useState(null)
    const [isFetching, setIsFetching] = useState(false)

    useEffect(() => {
        async function fetchData() {
            try {
                setIsFetching(true)
                const workout = await api.details(id, token)
                console.log(workout);
                setWorkout(workout)
            } catch {
                navigate(routes.notFound, { state: { message: 'Workout not found' } })
            } finally {
                setIsFetching(false)
            }
        }

        fetchData()
    }, [token, navigate])

    return { workout, isFetching }
}
