import React, { useState } from 'react';
import { useHistory } from 'react-router-dom'; // Import useHistory from react-router-dom
import addNotificationToDB from '../services/AddNotificationToDB';

const AddNotificationForm = () => {
    const history = useHistory(); // Initialize history hook

    const [notification, setNotification] = useState({
        addressee: '',
        message: '',
    });

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNotification({
            ...notification,
            [name]: value,
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        
        addNotificationToDB(notification)
            .then((response) => {
                console.log('Notification added:', response);
                // Optionally, update state or perform any other actions upon successful addition
                
                // Redirect to /ChildPage after successfully adding the notification
                history.push('/ChildPage');
            })
            .catch((error) => {
                console.error('Error adding notification:', error);
            });
    };

    return (
        <div>
            <h2>Add Notification</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="notificationTo">Name:</label>
                    <input
                        type="text"
                        id="notificationTo"
                        name="addressee"
                        value={notification.addressee} // Fix the value attribute to use 'addressee'
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="notificationMessage">Message:</label>
                    <input
                        type="text"
                        id="notificationMessage"
                        name="message"
                        value={notification.message}
                        onChange={handleInputChange}
                        required
                    />
                </div>
                <div>
                    <button type="submit">Add Notification</button>
                </div>
            </form>
        </div>
    );
};

export default AddNotificationForm;
