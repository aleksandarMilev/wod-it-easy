// export const identityServiceUrl = import.meta.env.VITE_IDENTITY_SERVICE_URL;
// export const workoutsServiceUrl = import.meta.env.VITE_WORKOUTS_SERVICE_URL;
// export const workoutsServiceAdminUrl = `${workoutsServiceUrl}/administrator`;
// export const profileServiceUrl = import.meta.env.VITE_PROFILE_SERVICE_URL;

export const identityServiceUrl = "http://localhost:5000";
export const workoutsServiceUrl = "http://localhost:5001";
export const workoutsServiceAdminUrl = `${workoutsServiceUrl}/administrator`;
export const profileServiceUrl = "http://localhost:5002";

export const routes = {
  home: "/",
  login: "/identity/login",
  register: "/identity/register",
  registrationChoice: "/registration-choice",

  error: {
    badRequest: "/error/bad-request",
    notFound: "/error/not-found",
    accessDenied: "/error/access-denied",
  },

  profile: {
    default: "/profile",
    create: "/profile/new",
    update: "/profile/update",
  },

  athlete: {
    default: "/athlete",
    getId: "/athlete/id",
    create: "/athlete/new",
    update: "/athlete/update",
  },

  workout: {
    default: "/workout",
    search: "/workout/search",
    create: "/workout/create",
    update: "/workout/update",
  },

  participation: {
    default: "/participation",
    mine: "/participation/mine",
    cancel: "/participation/cancel",
    reJoin: "/participation/rejoin",
    count: "participation/count",
  },
};

export const httpActions = {
  get: "GET",
  post: "POST",
  put: "PUT",
  delete: "DELETE",
  patch: "PATCH",
};

export const httpHeaders = {
  authorization: "Authorization",
  contentType: "Content-Type",
};

export const authtenticationTypes = {
  bearer: "Bearer",
};

export const contentTypes = {
  applicationJson: "application/json",
};

export const errorMessages = {
  genericError: "Sorry, something went wrong. Please try again later.",

  workout: {
    search:
      "Sorry, something went wrong while searching for your workout. Please, try again.",
    notFound:
      "We couldn’t find the workout you’re looking for. Please check the URL or try again later.",
    create:
      "Sorry, something went wrong while creating the workout. Please try again later. If the issue persists, contact our support.",
    update:
      "Sorry, something went wrong while updating the workout. Please try again later. If the issue persists, contact our support.",
    remove:
      "Sorry, something went wrong while deleting the workout. Please try again later. If the issue persists, contact our support.",
  },

  profile: {
    mine: "Sorry, something went wrong while loading your profile. Please try again later.",
    create:
      "Sorry, something went wrong while creating your profile. Please try again later. If the issue persists, contact our support.",
    update:
      "Sorry, something went wrong while updating your profile. Please try again later. If the issue persists, contact our support.",
    remove:
      "Sorry, something went wrong while deleting your profile. Please try again later. If the issue persists, contact our support",
  },

  athlete: {
    getId:
      "Sorry, something went wrong while fetching the athlete's id. Please try again later.",
    create:
      "Sorry, something went wrong while creating your profile. Please try again later. If the issue persists, contact our support.",
    update:
      "Sorry, something went wrong while updating your profile. Please try again later. If the issue persists, contact our support.",
    remove:
      "Sorry, something went wrong while deleting your profile. Please try again later. If the issue persists, contact our support",
  },

  participation: {
    all: "Sorry, something went wrong while loading the participation list. Please try again later.",
    join: "Sorry, something went wrong while joining the workout. Please try again later. If the issue persists, contact our support.",
  },
};

export const workoutTypes = [
  { value: 1, label: "Weightlifting" },
  { value: 2, label: "Gymnastic" },
  { value: 3, label: "Cardiovascular" },
  { value: 4, label: "Mobility" },
  { value: 5, label: "Powerlifting" },
  { value: 6, label: "CrossFit" },
  { value: 7, label: "Other" },
];

export const participationStatuses = {
  joined: "joined",
  left: "left",
};

export const pagination = {
  defaultPageSize: 10,
  defaultPageIndex: 1,
};
