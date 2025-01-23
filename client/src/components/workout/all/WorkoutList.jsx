import { useState } from 'react'
import { FaSearch, FaArrowLeft, FaArrowRight } from 'react-icons/fa'

import { pagination } from '../../../common/constants'
import { useSearch } from '../../../hooks/useWorkout'

import WorkoutListItem from './workout-list-item/WorkoutListItem'
import DefaultSpinner from '../../common/default-spinner/DefaultSpinner'

import image from '../../../assets/items-not-found.jpg'

import './WorkoutList.css'

export default function WorkoutList() {
    const [searchTerm, setSearchTerm] = useState('')
    const [page, setPage] = useState(pagination.defaultPageIndex)
    const pageSize = pagination.defaultPageSize

    const { workouts, totalItems, isFetching } = useSearch(searchTerm, page, pageSize)

    const totalPages = Math.ceil(totalItems / pageSize)

    const handleSearchChange = (e) => {
        setSearchTerm(e.target.value)
        setPage(pagination.defaultPageIndex)
    }

    const handlePageChange = (newPage) => {
        setPage(newPage)
    }

    return (
        <div className="container mt-5 mb-5">
            <div className="row mb-4">
                <div className="col-md-10 mx-auto d-flex">
                    <div className="search-bar-container d-flex">
                        <input
                            type="text"
                            className="form-control search-input"
                            placeholder="Search workouts..."
                            value={searchTerm}
                            onChange={handleSearchChange}
                        />
                        <button className="btn btn-light search-btn" disabled={isFetching}>
                            <FaSearch size={20} />
                        </button>
                    </div>
                </div>
            </div>
            <div className="d-flex justify-content-center row">
                <div className="col-md-10">
                    {isFetching ? (
                        <DefaultSpinner />
                    ) : workouts.length > 0 ? (
                        <>
                            {workouts.map(w => (
                                <WorkoutListItem key={w.id} {...w} />
                            ))}
                            <div className="pagination-container d-flex justify-content-center mt-4">
                                <button
                                    className={`btn pagination-btn ${page === 1 ? 'disabled' : ''}`}
                                    onClick={() => handlePageChange(page - 1)}
                                    disabled={page === 1}
                                >
                                    <FaArrowLeft /> Previous
                                </button>
                                <div className="pagination-info">
                                    <span className="current-page">{page}</span> /{" "}
                                    <span className="total-pages">{totalPages}</span>
                                </div>
                                <button
                                    className={`btn pagination-btn ${page === totalPages ? 'disabled' : ''}`}
                                    onClick={() => handlePageChange(page + 1)}
                                    disabled={page === totalPages}
                                >
                                    Next <FaArrowRight />
                                </button>
                            </div>
                        </>
                    ) : (
                        <div className="d-flex flex-column align-items-center justify-content-center mt-5">
                            <img
                                src={image}
                                alt="No books found"
                                className="mb-4"
                                style={{ maxWidth: '200px', opacity: 0.7 }}
                            />
                            <h5 className="text-muted">We couldn't find any workouts</h5>
                            <p className="text-muted text-center" style={{ maxWidth: '400px' }}>
                                Try adjusting your search term.
                            </p>
                        </div>
                    )}
                </div>
            </div>
        </div>
    )
}
