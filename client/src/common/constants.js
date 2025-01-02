export const baseUrl = 'https://localhost:7141'

export const routes = {
    home: '/',
    login: '/identity/login',
    register: '/identity/register'
}

export const httpActions = {
    get: 'GET',
    post: 'POST',
    put: 'PUT',
    delete: 'DELETE'
}

export const httpHeaders = {
    authorization: 'Authorization',
    contentType: 'Content-Type'
}

export const authtenticationTypes = {
    bearer: 'Bearer',
}

export const contentTypes = {
    applicationJson: 'application/json'
}

export const errorMessages = {
    genericError: 'Sorry, something went wrong. Please try again later.'
}