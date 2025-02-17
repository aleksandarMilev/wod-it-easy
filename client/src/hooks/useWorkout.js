import { useState, useEffect, useContext } from "react";
import { useNavigate } from "react-router-dom";

import * as api from "../api/workoutApi";
import { UserContext } from "../contexts/User";
import { routes } from "../common/constants";

export function useDetails(id) {
  const navigate = useNavigate();
  const { token } = useContext(UserContext);

  const [workout, setWorkout] = useState(null);
  const [isFetching, setIsFetching] = useState(false);

  useEffect(() => {
    async function fetchData() {
      try {
        setIsFetching(true);
        setWorkout(await api.details(id, token));
      } catch (error) {
        navigate(routes.error.notFound, {
          state: {
            message: error.message,
          },
        });
      } finally {
        setIsFetching(false);
      }
    }

    fetchData();
  }, [token, navigate]);

  return { workout, isFetching };
}

export function useSearch(startsAt, page, pageSize) {
  const navigate = useNavigate();
  const { token } = useContext(UserContext);

  const [workouts, setWorkouts] = useState([]);
  const [totalItems, setTotalItems] = useState(0);
  const [isFetching, setIsFetching] = useState(false);

  useEffect(() => {
    async function fetchData() {
      try {
        setIsFetching(true);

        const result = await api.search(startsAt, page, pageSize, token);

        setWorkouts(result.items);
        setTotalItems(result.totalItems);
      } catch (error) {
        navigate(routes.badRequest, {
          state: {
            message: error.message,
          },
        });
      } finally {
        setIsFetching(false);
      }
    }

    fetchData();
  }, [startsAt, page, pageSize, token, navigate]);

  return { workouts, totalItems, isFetching };
}
