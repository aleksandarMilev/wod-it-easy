import { FaUser } from 'react-icons/fa'
import { Link } from 'react-router-dom'

import { routes } from '../../../common/constants'
import { useMine } from '../../../hooks/useAthlete'

import DefaultSpinner from '../../common/default-spinner/DefaultSpinner'

import './AthleteProfile.css'

export default function AthleteProfile() {
    const { isFetching, athlete } = useMine()

    if (isFetching || !athlete) {
        return <DefaultSpinner />
    }

    return (
        <div className="athlete-profile-container">
            <div className="athlete-profile-card">
                <div className="athlete-icon-container">
                    <FaUser className="athlete-icon" />
                </div>
                <h1 className="athlete-name">{athlete.name}</h1>
                <Link
                    to={routes.athlete.update}
                    className="update-button"
                >
                    Update
                </Link>
            </div>
        </div>
    )
}