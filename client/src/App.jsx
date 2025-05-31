import { Route, Routes } from "react-router-dom";

import { routes } from "./common/constants";
import { UserContextProvider } from "./contexts/User";
import { MessageContextProvider } from "./contexts/Message";

import AdminRoute from "./components/common/routes/AdminRoute";
import AuthenticatedRoute from "./components/common/routes/AuthenticatedRoute";
import NonAthleteRoute from "./components/common/routes/NonAthleteRoute";
import AthleteRoute from "./components/common/routes/AthleteRoute";

import Home from "./components/home/Home";
import Navigation from "./components/common/navigation/Navigation";
import Footer from "./components/common/footer/Footer";

import Login from "./components/identity/login/Login";
import Register from "./components/identity/register/Register";
import RegistrationChoice from "./components/identity/registration-choice/RegistrationChoice";

import Profile from "./components/profile/mine/Profile";
import CreateProfile from "./components/profile/create/CreateProfile";
import UpdateProfile from "./components/profile/update/UpdateProfile";

import CreateAthlete from "./components/athlete/register/create/CreateAthlete";
import UpdateAthlete from "./components/athlete/register/update/UpdateAthlete";

import WorkoutList from "./components/workout/all/WorkoutList";
import WorkoutDetails from "./components/workout/details/WorkoutDetails";
import CreateWorkout from "./components/workout/create/CreateWorkout";
import UpdateWorkout from "./components/workout/update/UpdateWorkout";

import ParticipationList from "./components/participation/all/ParticipationList";

import NotFound from "./components/common/errors/NotFound";
import AccessDenied from "./components/common/errors/AccessDenied";
import BadRequest from "./components/common/errors/BadRequest";

import "./App.css";

export default function App() {
  return (
    <MessageContextProvider>
      <UserContextProvider>
        <div className="homepage-container">
          <Navigation />
          <main>
            <Routes>
              <Route path={routes.home} element={<Home />} />
              <Route path={routes.login} element={<Login />} />
              <Route path={routes.register} element={<Register />} />
              <Route
                path={routes.registrationChoice}
                element={<NonAthleteRoute element={<RegistrationChoice />} />}
              />

              <Route
                path={routes.profile.default}
                element={<AuthenticatedRoute element={<Profile />} />}
              />
              <Route
                path={routes.profile.create}
                element={<AuthenticatedRoute element={<CreateProfile />} />}
              />
              <Route
                path={routes.profile.update}
                element={<AuthenticatedRoute element={<UpdateProfile />} />}
              />

              <Route
                path={routes.athlete.create}
                element={<AuthenticatedRoute element={<CreateAthlete />} />}
              />
              <Route
                path={routes.athlete.update}
                element={<AthleteRoute element={<UpdateAthlete />} />}
              />

              <Route
                path={routes.workout.search}
                element={<AuthenticatedRoute element={<WorkoutList />} />}
              />
              <Route
                path={`${routes.workout.default}/:id`}
                element={<AuthenticatedRoute element={<WorkoutDetails />} />}
              />
              <Route
                path={routes.workout.create}
                element={<AdminRoute element={<CreateWorkout />} />}
              />
              <Route
                path={`${routes.workout.update}/:id`}
                element={<AdminRoute element={<UpdateWorkout />} />}
              />

              <Route
                path={routes.participation.default}
                element={<AthleteRoute element={<ParticipationList />} />}
              />

              <Route path={routes.error.notFound} element={<NotFound />} />
              <Route
                path={routes.error.accessDenied}
                element={<AccessDenied />}
              />
              <Route path={routes.error.badRequest} element={<BadRequest />} />
            </Routes>
          </main>
          <Footer />
        </div>
      </UserContextProvider>
    </MessageContextProvider>
  );
}
