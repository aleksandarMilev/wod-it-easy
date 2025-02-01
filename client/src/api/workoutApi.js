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
    const url = startsAtDate
        ? `${baseUrl}${routes.workout.search}?startsAtDate=${encodeURIComponent(startsAtDate.toISOString().split('T')[0])}&pageIndex=${page}&pageSize=${pageSize}`
        : `${baseUrl}${routes.workout.search}?pageIndex=${page}&pageSize=${pageSize}`

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

    const result = await response.json()

    if(response.ok){
        return result
    }
    
    throw new Error(
        response.status === 400 
            ? result 
            : errorMessages.workout.create)
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

export async function remove(id, token){
    const response = await requester.remove(
        `${baseAdminUrl}${routes.workout.default}/${id}`,
        token)

    if(!response.ok){
        throw new Error(errorMessages.workout.remove)
    }
}
