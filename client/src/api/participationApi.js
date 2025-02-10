import { baseUrl, routes, errorMessages } from "../common/constants";
import * as requester from "./requester";

export async function all(page, pageSize, token) {
  const url = `${baseUrl}${routes.participation.mine}?pageIndex=${page}&pageSize=${pageSize}`;

  const response = await requester.get(url, token);

  if (response.ok) {
    return await response.json();
  }

  throw new Error(errorMessages.participation.all);
}

export async function getParticipationId(athleteId, workoutId, token) {
  const response = await requester.get(
    `${baseUrl}${routes.participation.default}/${athleteId}/${workoutId}`,
    token
  );

  if (response.ok) {
    const result = await response.json();
    return result.id;
  }

  throw new Error();
}

export async function join(data, token) {
  const response = await requester.post(
    baseUrl + routes.participation.default,
    data,
    token
  );

  if (response.ok) {
    const result = await response.json();
    return result.id;
  }

  throw new Error(errorMessages.participation.join);
}

export async function leave(id, token) {
  const response = await requester.patch(
    `${baseUrl}${routes.participation.cancel}/${id}`,
    token
  );

  if (response.ok) {
    return true;
  }

  return false;
}

export async function reJoin(id, token) {
  const response = await requester.patch(
    `${baseUrl}${routes.participation.reJoin}/${id}`,
    token
  );

  if (response.ok) {
    const result = await response.json();
    return result.id;
  }

  throw new Error(errorMessages.participation.join);
}

export async function remove(id, token) {
  const response = await requester.remove(
    `${baseUrl}${routes.participation.default}/${id}`,
    token
  );

  if (response.ok) {
    return true;
  }

  return false;
}
