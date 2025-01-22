import { post } from './requester'
import { 
    baseAdminUrl, 
    routes, 
    errorMessages
} from '../common/constants'

export async function create(data, token){
    const response = await post(
        baseAdminUrl + routes.workout.default,
        data,
        token)

    if(!response.ok){
        throw new Error(errorMessages.workout.create)
    } 
}
