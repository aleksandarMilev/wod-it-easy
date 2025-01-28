import { baseUrl, routes, errorMessages } from '../common/constants'
import * as requester from './requester'

export async function getId(token) {
    const response = await requester.get(
        baseUrl + routes.athlete.getId,
        token)

    if(response.ok){
        return await response.json()
    } else if(response.status === 404){
        return null
    }

    throw new Error(errorMessages.genericError)
}

export async function create(data, token) {
    const response = await requester.post(
        baseUrl + routes.athlete.default, 
        data,
        token)

    const result = await response.json()

    if(response.ok){
        return result.id
    }

    throw new Error(errorMessages.genericError)
}

export async function update(id, data, token) {
    const response = await requester.put(
        baseUrl + routes.athlete.default + `/${id}`, 
        data,
        token)

    if(!response.ok){
        throw new Error(errorMessages.genericError)
    }
}
