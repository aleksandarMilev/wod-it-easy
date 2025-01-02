import { baseUrl, routes, errorMessages } from '../common/constants'
import { postAsync } from './requester'

export async function loginAsync(data){
    const response = await postAsync(baseUrl + routes.login, data)
    const result = await response.json()

    if(response.ok){
        return result.token
    } else {
        throw new Error(result || errorMessages.genericError)
    }
}

export async function registerAsync(data){
    const response = await postAsync(baseUrl + routes.register, data)
    const result = await response.json()
    
    if(response.ok){
        return result.token
    } else {
        throw new Error(result || errorMessages.genericError)
    }
}
