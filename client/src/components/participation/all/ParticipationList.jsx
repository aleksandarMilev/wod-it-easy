import { useState } from 'react'

import { pagination } from '../../../common/constants'
import { useAll } from '../../../hooks/useParticipation'

import ParticipationListItem from './list-item/ParticipationListItem'
import DefaultSpinner from '../../common/default-spinner/DefaultSpinner'
import Pagination from '../../common/pagination/Pagination'

import image from '../../../assets/items-not-found.jpg'

import './ParticipationList.css'

export default function ParticipationList() {
    const [page, setPage] = useState(pagination.defaultPageIndex)
    const pageSize = pagination.defaultPageSize

    const { participations, totalItems, isFetching } = useAll(page, pageSize)

    const totalPages = Math.ceil(totalItems / pageSize)

    const handlePageChange = (newPage) => {
        setPage(newPage)
    }

    return (
        <div className="participation-list-container container mt-5 mb-5">
            <h2 className="mb-4">Your Workout Participations</h2>

            <div className="d-flex justify-content-center row">
                <div className="col-md-10">
                    {isFetching ? (
                        <div className="participation-list-spinner">
                            <DefaultSpinner />
                        </div>
                    ) : participations.length > 0 ? (
                        <>
                            <div className="participation-list-items">
                                {participations.map(p => (
                                    console.log(p), <ParticipationListItem key={p.workoutId} {...p} />
                                ))}
                            </div>
                            <Pagination
                                currentPage={page}
                                totalPages={totalPages}
                                onPageChange={handlePageChange}
                                isFetching={isFetching}
                            />
                        </>
                    ) : (
                        <div className="participation-list-empty-state">
                            <img
                                src={image}
                                alt="No participations found"
                                className="mb-4"
                            />
                            <h5>You haven't joined any workouts</h5>
                            <p>
                                Try joining a workout and come back here to see your participations.
                            </p>
                        </div>
                    )}
                </div>
            </div>
        </div>
    )
}
