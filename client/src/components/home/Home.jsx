import './Home.css'

import img from '../../assets/home.jpg'

export default function Home(){
    return(
        <>
            <section className="intro">
                <div className="intro-text">
                <h2>Get Stronger, Faster, Better</h2>
                <p>Join our CrossFit community and elevate your fitness with personalized WODs and expert coaching.</p>
                <button className="cta-button">Get Started</button>
                </div>
                <div className="intro-image">
                <img src={img} alt="CrossFit Workout" />
                </div>
            </section>
            <section className="features" id="about">
                <h2>Why Choose Wod It Easy?</h2>
                <div className="feature-list">
                <div className="feature-item">
                    <h3>Expert Coaching</h3>
                    <p>Our certified trainers will guide you through every workout.</p>
                </div>
                <div className="feature-item">
                    <h3>Community</h3>
                    <p>Be part of an encouraging, motivating group of athletes.</p>
                </div>
                <div className="feature-item">
                    <h3>Flexible Programs</h3>
                    <p>Choose from a variety of training schedules that fit your lifestyle.</p>
                </div>
                </div>
            </section>
        </>
    )
}