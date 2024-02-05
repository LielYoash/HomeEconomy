const getAllTasks = () => {
    // Replace this with your actual API endpoint or logic to fetch tasks
    return fetch('/api/tasks')
        .then(response => response.json())
        .then(data => data.tasks)
        .catch(error => {
            console.error('Error fetching tasks:', error);
            throw error; // Rethrow the error to be caught by the component
        });
};

export default getAllTasks;
