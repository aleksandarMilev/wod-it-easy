import { useState } from "react";
import { FaSearch } from "react-icons/fa";

import { pagination } from "../../../common/constants";
import { useSearch } from "../../../hooks/useWorkout";

import WorkoutListItem from "./workout-list-item/WorkoutListItem";
import DefaultSpinner from "../../common/default-spinner/DefaultSpinner";

import image from "../../../assets/items-not-found.jpg";

import Pagination from "../../common/pagination/Pagination";

import "./WorkoutList.css";

export default function WorkoutList() {
  const [startsAt, setStartsAt] = useState(null);
  const [page, setPage] = useState(pagination.defaultPageIndex);
  const pageSize = pagination.defaultPageSize;

  const { workouts, totalItems, isFetching } = useSearch(
    startsAt,
    page,
    pageSize
  );

  const totalPages = Math.ceil(totalItems / pageSize);

  const handleSearchChange = (e) => {
    const value = e.target.value;

    setStartsAt(value ? new Date(value) : null);
    setPage(pagination.defaultPageIndex);
  };

  const handlePageChange = (newPage) => {
    setPage(newPage);
  };

  return (
    <div className="container mt-5 mb-5">
      <div className="row mb-4">
        <div className="col-md-10 mx-auto d-flex">
          <div className="search-bar-container d-flex">
            <input
              type="date"
              className="form-control search-input"
              placeholder="Search workouts by date..."
              value={startsAt ? startsAt.toISOString().split("T")[0] : ""}
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
              {workouts.map((w) => (
                <WorkoutListItem key={w.id} {...w} />
              ))}
              <Pagination
                currentPage={page}
                totalPages={totalPages}
                onPageChange={handlePageChange}
                isFetching={isFetching}
              />
            </>
          ) : (
            <div className="d-flex flex-column align-items-center justify-content-center mt-5">
              <img
                src={image}
                alt="No workouts found"
                className="mb-4"
                style={{ maxWidth: "200px", opacity: 0.7 }}
              />
              <h5 className="text-muted">We couldn't find any workouts</h5>
              <p
                className="text-muted text-center"
                style={{ maxWidth: "400px" }}
              >
                Try adjusting your search term.
              </p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
