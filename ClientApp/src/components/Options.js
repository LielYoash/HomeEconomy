// Options.js
import React from 'react';
import Landing from './Landing'; // Import the Landing component

const Options = ({ showLanding, handleRegisterClick, handleLoginClick }) => {
  return (
    <div>
      {showLanding && <Landing handleRegisterClick={handleRegisterClick} handleLoginClick={handleLoginClick} />}
      {/* You can add other components or content here */}
    </div>
  );
};

export default Options;
