import { createContext } from 'react'
import { useNavigate } from 'react-router-dom'

import { routes } from '../common/constants'
import { useMessage } from './Message'
import usePersistedState from '../hooks/usePersistedState'

export const UserContext = createContext({
    userId: '',
    username: '',
    email: '',
    token: '',
    isAdmin: false,
    isAuthenticated: false,
    changeAuthenticationState: (state) => {},
    logout: () => {}
})

export function UserContextProvider(props) {
    const { showMessage } = useMessage()
    const navigate = useNavigate()

    const getInitUser = () => {
        const storedUser = localStorage.getItem('user')
        return storedUser ? JSON.parse(storedUser) : {}
    }

    const [user, setUser] = usePersistedState('user', getInitUser())

    const changeAuthenticationState = (state) => setUser(state)

    const logout = () => {
        const username = user.username
        
        setUser({})
        localStorage.removeItem('user')

        showMessage(`Goodbuy, ${username}!`, true)
        navigate(routes.home)
    }

    const userData = {
        userId: user.userId,
        username: user.username,
        email: user.email,
        isAdmin: user.isAdmin,
        token: user.token,
        isAuthenticated: !!user.username,
        changeAuthenticationState,
        logout
    }

    return (
        <UserContext.Provider value={userData}>
            {props.children}
        </UserContext.Provider>
    )
}
