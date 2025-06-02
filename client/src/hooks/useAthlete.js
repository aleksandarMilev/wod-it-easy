import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import * as api from "../api/athleteApi";
import { routes } from "../common/constants";

export function useMine(token) {
  const navigate = useNavigate();

  const [athlete, setAthlete] = useState(null);
  const [isFetching, setIsFetching] = useState(false);

  useEffect(() => {
    async function fetchData() {
      try {
        setIsFetching(true);
        setAthlete(await api.mine(token));
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

  return { athlete, isFetching };
}
