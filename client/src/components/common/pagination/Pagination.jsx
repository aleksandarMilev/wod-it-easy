import { FaArrowLeft, FaArrowRight } from 'react-icons/fa'

export default function Pagination({
    page, 
    pageSize, 
    totalItems, 
    setPage 
}) {
    const totalPages = Math.ceil(totalItems / pageSize)
    
    const handlePageChange = (newPage) => {
        setPage(newPage)
    }

    return (
        <div className="pagination-container d-flex justify-content-center mt-4">
            <button
                className={`btn pagination-btn ${page === 1 ? 'disabled' : ''}`}
                onClick={() => handlePageChange(page - 1)}
                disabled={page === 1}
            >
                <FaArrowLeft /> Previous
            </button>
            <div className="pagination-info">
                <span className="current-page">{page}</span> / <span className="total-pages">{totalPages}</span>
            </div>
            <button
                className={`btn pagination-btn ${page === totalPages ? 'disabled' : ''}`}
                onClick={() => handlePageChange(page + 1)}
                disabled={page === totalPages}
            >
                Next <FaArrowRight />
            </button>
        </div>
    )
}