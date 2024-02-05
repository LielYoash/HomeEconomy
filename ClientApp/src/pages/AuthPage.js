// AuthPage.js
import React, { useState } from 'react';
import Options from '../components/Options';
import Register from '../components/Register';
import Login from '../components/Login';

const AuthPage = () => {
  const [showLanding, setShowLanding] = useState(true);
  const [showRegister, setShowRegister] = useState(false);
  const [showLogin, setShowLogin] = useState(false);

  const handleRegisterClick = () => {
    setShowLanding(false);
    setShowRegister(true);
    setShowLogin(false);
  };

  const handleLoginClick = () => {
    setShowLanding(false);
    setShowRegister(false);
    setShowLogin(true);
  };

  const goToLanding = () => {
    setShowLanding(true);
    setShowRegister(false);
    setShowLogin(false);
  };

  return (
    <div>
      {showLanding && (
        <Options
          showLanding={showLanding}
          handleRegisterClick={handleRegisterClick}
          handleLoginClick={handleLoginClick}
        />
      )}
      {showRegister && <Register goToLanding={goToLanding} />}
      {showLogin && <Login goToLanding={goToLanding} />}
      {/* Add other components as needed */}
    </div>
  );
};

export default AuthPage;
