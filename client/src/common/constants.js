export const baseUrl = 'https://localhost:7141'
export const baseAdminUrl = `${baseUrl}/administrator`

export const routes = {
    home: '/',
    login: '/identity/login',
    register: '/identity/register',

    error: {
        badRequest: '/error/bad-request',
        notFound: '/error/not-found',
        accessDenied: '/error/access-denied'
    },

    workout: {
        default: '/workout',
        search: '/workout/search',
        create: '/workout/create',
        update: '/workout/update'
    }
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
    bearer: 'Bearer'
}

export const contentTypes = {
    applicationJson: 'application/json'
}

export const errorMessages = {
    genericError: 'Sorry, something went wrong. Please try again later.',

    workout: {
        search: 'Sorry, something went wrong while searching for your workout. Please, try again.',
        notFound: 'We couldn’t find the workout you’re looking for. Please check the URL or try again later.',
        create: 'Sorry, something went wrong while creating the workout. Please try again later. If the issue persists, contact our support.',
        update: 'Sorry, something went wrong while updating the workout. Please try again later. If the issue persists, contact our support.'
    }
}

export const workoutTypes = [
    { value: 1, label: 'Weightlifting' },
    { value: 2, label: 'Gymnastic' },
    { value: 3, label: 'Cardiovascular' },
    { value: 4, label: 'Mobility' },
    { value: 5, label: 'Powerlifting' },
    { value: 6, label: 'CrossFit' },
    { value: 7, label: 'Other' }
]

export const pagination = {
    defaultPageSize: 10,
    defaultPageIndex: 1
}
