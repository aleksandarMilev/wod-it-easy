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

    throw new Error(errorMessages.workout.notFound)
}

export async function search(
    startsAtDate,
    page,
    pageSize,
    token
) {
    const date = startsAtDate || new Date()
    const formattedDate = date.toISOString().split('T')[0]

    const url = `${baseUrl}${routes.workout.search}?startsAtDate=${encodeURIComponent(formattedDate)}&pageIndex=${page}&pageSize=${pageSize}`
    
    const response = await requester.get(url, token)

    if(response.ok){
        return await response.json()
    } 

    throw new Error(errorMessages.workout.search)
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

export async function update(id, data, token){
    const response = await requester.put(
        `${baseAdminUrl}${routes.workout.default}/${id}`,
        data,
        token)

    if(!response.ok){
        throw new Error(errorMessages.workout.update)
    } 
}
