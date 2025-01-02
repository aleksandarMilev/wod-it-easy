import { useNavigate } from 'react-router-dom'
import { useContext } from 'react'
import { jwtDecode } from 'jwt-decode'

import * as api from '../api/identity'
import { UserContext } from '../contexts/User'
import { useMessage } from '../contexts/Message'
import { routes } from '../common/constants'

function useAuthentication() {
    const navigate = useNavigate()
    const { showMessage } = useMessage()

    const { changeAuthenticationState } = useContext(UserContext)

    const onAuthenticateAsync = async (apiCall, data) => {
        try {
            const token = await apiCall(data)
            const tokenEncoded = jwtDecode(token)
            const username = tokenEncoded["unique_name"]

            const user = {
                token: token,
                username: username,
                email: tokenEncoded.email,
                userId: tokenEncoded.nameid,
                isAdmin: !!tokenEncoded.role
            }

            changeAuthenticationState(user)
            
            showMessage(`Welcome, ${username}!`, true)
            navigate(routes.home)
        } catch (error) {
            showMessage(error.message, false)
        }
    }

    return onAuthenticateAsync
}

export function useLogin() {
    const onAuthenticateAsync = useAuthentication()
    const onLoginAsync = (data) => onAuthenticateAsync(api.loginAsync, data) 

    return onLoginAsync
}

export function useRegister() {
    const onAuthenticateAsync = useAuthentication()
    const onRegisterAsync = (data) => onAuthenticateAsync(api.registerAsync, data)

    return onRegisterAsync
}
