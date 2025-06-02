import { useState, useEffect, useContext } from "react";
import { useNavigate } from "react-router-dom";

import { routes } from "../common/constants";
import { UserContext } from "../contexts/User";

import * as api from "../api/participationApi";

export function useAll(page, pageSize) {
  const navigate = useNavigate();
  const { token } = useContext(UserContext);

  const [participations, setParticipations] = useState([]);
  const [totalItems, setTotalItems] = useState(0);
  const [isFetching, setIsFetching] = useState(false);

  useEffect(() => {
    async function fetchData() {
      try {
        setIsFetching(true);

        const result = await api.all(page, pageSize, token);

        setParticipations(result.items);
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
  }, [page, pageSize, token]);

  return { participations, totalItems, isFetching };
}
