import { baseUrl, routes } from '../common/constants'
import * as requester from './requester'

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