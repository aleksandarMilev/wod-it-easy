import * as requester from './requester'
import {
    routes,
    baseUrl,
    baseAdminUrl,
    errorMessages
} from '../common/constants'

export async function details(id, token){
    const response = await requester.get(
        `${baseUrl}${routes.workout.default}/${id}`,
        token)

    if(response.ok){
        return await response.json()
    } 

    throw new Error('Workout')
}

export async function create(data, token){
    const response = await requester.post(
        `${baseAdminUrl}${routes.workout.default}`,
        data,
        token)

    if(!response.ok){
        throw new Error(errorMessages.workout.create)
    } 
}
