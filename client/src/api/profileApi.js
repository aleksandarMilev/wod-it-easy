import {
  profileServiceUrl as baseUrl,
  routes,
  errorMessages,
} from "../common/constants";
import * as requester from "./requester";

export async function mine(token) {
  const response = await requester.get(baseUrl + routes.profile.default, token);

  if (response.ok) {
    return await response.json();
  }

  throw new Error(errorMessages.profile.mine);
}

export async function create(data, token) {
  const response = await requester.post(
    baseUrl + routes.profile.default,
    data,
    token
  );

  if (response.ok) {
    const result = await response.json();
    return result.id;
  }

  throw new Error(errorMessages.genericError);
}

export async function update(data, token) {
  const response = await requester.put(
    baseUrl + routes.profile.default,
    data,
    token
  );

  if (!response.ok) {
    throw new Error(errorMessages.profile.update);
  }
}

export async function remove(token) {
  const response = await requester.remove(
    baseUrl + routes.profile.default,
    token
  );

  if (!response.ok) {
    throw new Error(errorMessages.profile.remove);
  }
}
