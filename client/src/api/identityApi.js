import { baseUrl, routes, errorMessages } from "../common/constants";
import { post } from "./requester";

export async function login(data) {
  const response = await post(baseUrl + routes.login, data);
  const result = await response.json();

  if (response.ok) {
    return result.token;
  }

  throw new Error(result || errorMessages.genericError);
}

export async function register(data) {
  const response = await post(baseUrl + routes.register, data);
  const result = await response.json();

  if (response.ok) {
    return result.token;
  }

  throw new Error(result || errorMessages.genericError);
}
