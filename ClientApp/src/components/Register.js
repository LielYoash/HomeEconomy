// Register.js
import React, { useState } from 'react';

const Register = ({ goToLanding }) => {
  const [accountType, setAccountType] = useState(''); // State to store the selected account type

  const handleSubmit = (event) => {
    event.preventDefault();
    // Handle form submission
    // Access the selected account type with the 'accountType' state
  };

  const handleSelectChange = (event) => {
    setAccountType(event.target.value); // Update the selected account type
  };

  return (
    <div className="container">
      <h2>Register</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="username">Username:</label>
          <input type="text" id="username" name="username" required />
        </div>
        <div>
          <label htmlFor="email">Email:</label>
          <input type="email" id="email" name="email" required />
        </div>
        <div>
          <label htmlFor="password">Password:</label>
          <input type="password" id="password" name="password" required />
        </div>
        <div>
          <label htmlFor="accountType">Account Type:</label>
          <select id="accountType" name="accountType" value={accountType} onChange={handleSelectChange} required>
            <option value="parent">Parent</option>
            <option value="child">Child</option>
          </select>
        </div>
        <div>
          <button type="submit">Register</button>
          <button type="button" onClick={goToLanding}>Go back to Landing</button>
        </div>
      </form>
    </div>
  );
};

export default Register;
