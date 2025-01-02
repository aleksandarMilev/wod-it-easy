import { 
    httpActions, 
    errorMessages, 
    httpHeaders, 
    authtenticationTypes, 
    contentTypes 
} from '../common/constants'

async function requestAsync(url, method = httpActions.get, data = null, token = null) {
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
    } catch {
        throw new Error(errorMessages.genericError)
    }
}

export const getAsync = async (url, token = null) => await requestAsync(url, token)

export const postAsync = async (url, data, token = null) => await requestAsync(url, httpActions.post, data, token)

export const putAsync = async (url, data, token = null) => await requestAsync(url, httpActions.put, data, token)

export const deleteAsync = async (url, token = null) => await requestAsync(url, httpActions.delete, token)
