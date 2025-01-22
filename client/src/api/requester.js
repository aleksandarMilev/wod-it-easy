import { 
    httpActions, 
    errorMessages, 
    httpHeaders, 
    authtenticationTypes, 
    contentTypes 
} from '../common/constants'

async function request(
    url, 
    method = httpActions.get, 
    data = null, 
    token = null
) {
    const options = {
        method,
        headers: {}
    }

    if(token) {
        options.headers.Authorization = `${authtenticationTypes.bearer} ${token}`
    }

    if(data) {
        options.body = JSON.stringify(data)
        options.headers[httpHeaders.contentType] = contentTypes.applicationJson
    }
    
    try{
        return await fetch(url, options)
    } catch(error) {
        throw new Error(errorMessages.genericError)
    }
}

export const get = async (url, token = null) => await request(url, httpActions.get, null, token)

export const post = async (url, data, token = null) => await request(url, httpActions.post, data, token)

export const put = async (url, data, token = null) => await request(url, httpActions.put, data, token)

export const remove = async (url, token = null) => await request(url, httpActions.delete, null ,token)
