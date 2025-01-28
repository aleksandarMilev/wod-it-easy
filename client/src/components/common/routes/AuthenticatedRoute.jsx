import { useContext } from 'react'
import { Navigate } from 'react-router-dom'

import { UserContext } from '../../../contexts/User'
import { routes } from '../../../common/constants'

export default function AuthenticatedRoute({ element }) {
    const { isAuthenticated } = useContext(UserContext)

    return isAuthenticated 
        ? element 
        : <Navigate to={routes.login} replace />
}
