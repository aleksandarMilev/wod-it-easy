import { Link } from "react-router-dom";
import { FaDumbbell, FaFireAlt, FaChartLine } from "react-icons/fa";

import { routes } from "../../common/constants";

import "./Home.css";

export default function Home() {
  return (
    <>
      <section className="intro">
        <div className="intro-overlay">
          <div className="intro-text">
            <h2>Achieve Your Peak Performance</h2>
            <p>
              Make trivial daily routines a thing of the past with Wod It Easy
            </p>
          </div>
        </div>
      </section>
      <section className="features" id="about">
        <h2>Why Wod It Easy?</h2>
        <div className="feature-list">
          <div className="feature-item">
            <div className="feature-icon">
              <FaDumbbell />
            </div>
            <h3>Workout Scheduler</h3>
            <p>
              Plan your workouts effortlessly by choosing from a wide range
              tailored to your specific goals.
            </p>
            <Link to={routes.workout.search} className="feature-link">
              View Workouts
            </Link>
          </div>
          <div className="feature-item upcoming-feature">
            <div className="feature-icon">
              <FaFireAlt />
            </div>
            <h3>Calories Tracker</h3>
            <h6>(Upcoming)</h6>
            <p>
              Track your daily calorie intake and manage your nutrition
              effortlessly for optimal results.
            </p>
          </div>
          <div className="feature-item upcoming-feature">
            <div className="feature-icon">
              <FaChartLine />
            </div>
            <h3>1RM Calculator & Daily Results Logging</h3>
            <h6>(Upcoming)</h6>
            <p>
              Keep track of your progress with tools for calculating one-rep max
              lifts and logging daily workout results.
            </p>
          </div>
        </div>
      </section>
    </>
  );
}
