import { useState, useEffect } from 'react'

import * as api from '../api/participationApi'

export function useIsParticipant(
    athleteId,
    workoutId,
    token
) {
    const [isParticipant, setIsParticipant] = useState(false)

    useEffect(() => {
        async function fetchData() {
            try {
                setIsParticipant(await api.isParticipant(athleteId, workoutId, token))
            } catch {
                setIsParticipant(false)
            }
        }

        fetchData()
    }, [token])

    return { isParticipant, setIsParticipant }
}
