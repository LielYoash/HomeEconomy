// Landing.js
import React from 'react';
import './Landing.css'; // Import your CSS file

const Landing = ({ handleRegisterClick, handleLoginClick }) => {
    return (
        <div className="container">
            <h1>Welcome to Your App!</h1>
            <p>Please select an option:</p>
            <button onClick={handleRegisterClick}>Register</button>
            <button onClick={handleLoginClick}>Login</button>
        </div>
    );
};

export default Landing;
