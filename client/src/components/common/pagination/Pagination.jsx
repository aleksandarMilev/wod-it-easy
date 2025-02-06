import { FaArrowLeft, FaArrowRight } from "react-icons/fa";

import "./Pagination.css";

export default function Pagination({
  currentPage,
  totalPages,
  onPageChange,
  isFetching,
}) {
  return (
    <div className="pagination-container d-flex justify-content-center mt-4">
      <button
        className={`btn pagination-btn ${currentPage === 1 ? "disabled" : ""}`}
        onClick={() => onPageChange(currentPage - 1)}
        disabled={currentPage === 1 || isFetching}
      >
        <FaArrowLeft /> Previous
      </button>
      <div className="pagination-info">
        <span className="current-page">{currentPage}</span> /{" "}
        <span className="total-pages">{totalPages}</span>
      </div>
      <button
        className={`btn pagination-btn ${
          currentPage === totalPages ? "disabled" : ""
        }`}
        onClick={() => onPageChange(currentPage + 1)}
        disabled={currentPage === totalPages || isFetching}
      >
        Next <FaArrowRight />
      </button>
    </div>
  );
}
