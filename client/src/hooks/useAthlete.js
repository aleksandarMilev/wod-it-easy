import { useState, useEffect, useContext } from 'react'
import { useNavigate } from 'react-router-dom'

import * as api from '../api/athleteApi'
import { UserContext } from '../contexts/User'
import { routes } from '../common/constants'

export function useMine() {
    const navigate = useNavigate()
    const { token } = useContext(UserContext)
    
    const [athlete, setAthlete] = useState(null)
    const [isFetching, setIsFetching] = useState(false)

    useEffect(() => {
        async function fetchData() {
            try {
                setIsFetching(true)
                setAthlete(await api.mine(token))
            } catch(error) {
                navigate(
                    routes.error.notFound,
                    {
                        state: {
                            message: error.message                       
                        }
                    }
                )
            } finally {
                setIsFetching(false)
            }
        }

        fetchData()
    }, [token, navigate])

    return { athlete, isFetching }
}