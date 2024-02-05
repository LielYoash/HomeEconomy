// Simulated notifications data
const notificationsData = [
    { id: 1, message: 'New message 1' },
    { id: 2, message: 'New message 2' },
    { id: 3, message: 'New message 3' },
    // Add more notifications as needed
];

// Simulated API endpoint
const getNotifications = () => {
    return new Promise((resolve, reject) => {
        // Simulating API call delay with setTimeout (you can replace this with your actual fetch)
        setTimeout(() => {
            resolve(notificationsData); // Resolve the promise with the simulated data
        }, 1000); // Simulated delay of 1 second
    });
};

export default getNotifications;
