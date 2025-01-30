import { baseUrl, routes, errorMessages } from '../common/constants'
import * as requester from './requester'

export async function all(
    page,
    pageSize,
    token
) {
    const url = `${baseUrl}${routes.participation.default}?pageIndex=${page}&pageSize=${pageSize}`

    const response = await requester.get(url, token)

    if(response.ok){
        return await response.json()
    }

    throw new Error(errorMessages.participation.all)
}

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

export async function leave(
    athleteId, 
    workoutId, 
    token
) {
    const response = await requester.remove(
        `${baseUrl}${routes.participation.default}/${athleteId}/${workoutId}`,
        token)

    if(response.ok){
        return true
    }

    return false
}

export async function cancel(id, token) {
    const response = await requester.patch(
        `${baseUrl}${routes.participation.cancel}/${id}`,
        token)

    if(response.ok){
        return true
    }

    return false
}