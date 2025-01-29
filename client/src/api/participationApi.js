import { baseUrl, routes } from '../common/constants'
import * as requester from './requester'

export async function isParticipant(
    athleteId, 
    workoutId, 
    token
) {
    const response = await requester.get(
        `${baseUrl}${routes.participation.default}/${athleteId}/${workoutId}`,
        token)

    if(response.ok){
        return await response.json()
    }

    throw new Error()
}

export async function join(data, token) {
    const response = await requester.post(
        baseUrl + routes.participation.default,
        data,
        token)

    if(response.ok){
        return true
    }

    return false
}