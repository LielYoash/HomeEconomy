import React, { useState } from 'react';
import { useHistory } from 'react-router-dom';
import { BrowserRouter as Router, Route, Redirect } from 'react-router-dom';
import ParentNotificationsPage from './ParentNotificationsPage';
import AddNotificationForm from '../services/AddNotifications'
import ChildPage from './ChildPage';
import ParentTasksPage from "./ParentTasksPage";
import AddTaskPage from "./AddTaskPage";

const LoginForm = ({ setLoggedIn }) => {
    const history = useHistory();

    const handleSubmit = (event) => {
        event.preventDefault();
        // Simulated successful authentication
        const isAuthenticated = true; // Replace this with your authentication logic

        if (isAuthenticated) {
            console.log('Successful login');
            setLoggedIn(true); // Set the logged-in state to true
            // Redirect to the desired page after successful login
            history.push('/ChildPage'); // Make sure '/ParentPage' is a valid route
        } else {
            console.log('Login failed');
            // Handle login failure scenario
        }
    };

    return (
        <div className="container">
            <h2>Login</h2>
            <form onSubmit={handleSubmit}>
                {/* Form fields */}
                <div>
                    <label htmlFor="username">Username:</label>
                    <input type="text" id="username" name="username" required />
                </div>
                <div>
                    <label htmlFor="password">Password:</label>
                    <input type="password" id="password" name="password" required />
                </div>
                <div>
                    <button type="submit">Login</button>
                </div>
            </form>
        </div>
    );
};

const Login = () => {
    const [loggedIn, setLoggedIn] = useState(false);
    return (
        <Router>
            <Route exact path="/">
                {loggedIn ? <Redirect to="/ParentNotificationsPage" /> : <LoginForm setLoggedIn={setLoggedIn} />}
            </Route>
            <Route path="/ParentNotificationsPage">
                <ParentNotificationsPage />
            </Route>
            <Route path="/ChildPage">
            <ChildPage />
            </Route>
            <Route path="/addNotifications">
                <AddNotificationForm/>
            </Route>
            <Route path="/parentTasks">
                <ParentTasksPage/>
            </Route>
            <Route path="/addTask">
                <AddTaskPage/>
            </Route>
        </Router>
    );
};

export default Login;
