import { baseUrl, routes, errorMessages } from '../common/constants'
import { post, put } from './requester'

export async function create(data, token) {
    const response = await post(
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
    const response = await put(
        baseUrl + routes.athlete.default + `/${id}`, 
        data,
        token)

    if(!response.ok){
        throw new Error(errorMessages.genericError)
    }
}
