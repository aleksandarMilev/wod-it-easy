import { baseUrl, routes, errorMessages } from '../common/constants'
import * as requester from './requester'

export async function mine(token) {
    const response = await requester.get(
        baseUrl + routes.athlete.default,
        token)

    if(response.ok){
        return await response.json()
    } 

    throw new Error(errorMessages.athlete.mine)
}

export async function getId(token) {
    const response = await requester.get(
        baseUrl + routes.athlete.getId,
        token)

    if(response.ok){
        return await response.json()
    } else if(response.status === 404){
        return null
    }

    throw new Error(errorMessages.athlete.getId)
}

export async function create(data, token) {
    const response = await requester.post(
        baseUrl + routes.athlete.default, 
        data,
        token)
    
    if(response.ok){
        return await response.json()
    }

    throw new Error(errorMessages.genericError)
}

export async function update(data, token) {
    const response = await requester.put(
        baseUrl + routes.athlete.default, 
        data,
        token)

    if(!response.ok){
        throw new Error(errorMessages.athlete.update)
    }
}

export async function remove(token){
    const response = await requester.remove(
        baseUrl + routes.athlete.default,
        token)

    if(!response.ok){
        throw new Error(errorMessages.athlete.remove)
    }
}
