import { useState, useEffect, useRef } from "react";
import { useLocation } from "react-router-dom";

import { pagination } from "../../../common/constants";
import { useAll } from "../../../hooks/useParticipation";

import ParticipationListItem from "./list-item/ParticipationListItem";
import DefaultSpinner from "../../common/default-spinner/DefaultSpinner";
import Pagination from "../../common/pagination/Pagination";

import image from "../../../assets/items-not-found.jpg";

import "./ParticipationList.css";

export default function ParticipationList() {
  const [page, setPage] = useState(pagination.defaultPageIndex);
  const pageSize = pagination.defaultPageSize;

  const {
    participations: fetchedParticipations,
    totalItems,
    isFetching,
  } = useAll(page, pageSize);
  const [participations, setParticipations] = useState([]);

  useEffect(() => {
    setParticipations(fetchedParticipations);
  }, [fetchedParticipations]);

  const handleDelete = (id) => {
    setParticipations((prev) => prev.filter((p) => p.id !== id));
  };

  const totalPages = Math.ceil(totalItems / pageSize);

  const handlePageChange = (newPage) => {
    setPage(newPage);
  };

  const location = useLocation();
  const searchParams = new URLSearchParams(location.search);
  const scrollToId = searchParams.get("scrollTo");

  useEffect(() => {
    if (scrollToId) {
      const index = participations.findIndex(
        (p) => p.id === parseInt(scrollToId)
      );

      if (index !== -1) {
        scrollToParticipation(index);
      }
    }
  }, [scrollToId, participations]);

  const participationRefs = useRef([]);

  const scrollToParticipation = (index) => {
    if (participationRefs.current[index]) {
      participationRefs.current[index].scrollIntoView({
        behavior: "smooth",
        block: "start",
      });
    }
  };

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
                {participations.map((p, index) => (
                  <div
                    key={p.workoutId}
                    ref={(el) => (participationRefs.current[index] = el)}
                  >
                    <ParticipationListItem
                      {...p}
                      onDelete={handleDelete}
                      onScrollTo={() => scrollToParticipation(index)}
                    />
                  </div>
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
              <img src={image} alt="No participations found" className="mb-4" />
              <h5>You haven't joined any workouts</h5>
              <p>
                Try joining a workout and come back here to see your
                participations.
              </p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
