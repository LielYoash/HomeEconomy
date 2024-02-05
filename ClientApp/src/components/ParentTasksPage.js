import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';

// Replace the following import with your actual service
// import getAllTasks from '../services/GetAllTasks';

const fakeTasks = [
    { id: 1, description: 'Task 1', assignedTo: 'Kid1' },
    { id: 2, description: 'Task 2', assignedTo: 'Kid2' },
    { id: 3, description: 'Task 3', assignedTo: 'Kid3' },
];

const fakeKidsList = ['Kid1', 'Kid2', 'Kid3'];

const ParentTasksPage = () => {
    const [tasks, setTasks] = useState([]);
    const [kidsList] = useState(fakeKidsList);

    useEffect(() => {
        // Use this line for fetching data from a service
        // fetchTasks();

        // For demonstration purposes, using fake data directly
        setTasks(fakeTasks);
    }, []);

    // Replace the following function with your actual service call
    // const fetchTasks = () => {
    //     // getAllTasks()
    //     //     .then((data) => {
    //     //         setTasks(data);
    //     //     })
    //     //     .catch((error) => {
    //     //         console.error('Error fetching tasks:', error);
    //     //     });
    //
    //     // For demonstration purposes, using fake data directly
    //     setTasks(fakeTasks);
    // };

    const handleKidSelection = (taskId, selectedKid) => {
        setTasks((prevTasks) =>
            prevTasks.map((task) =>
                task.id === taskId ? { ...task, assignedTo: selectedKid } : task
            )
        );
    };

    return (
        <div className="container" style={{ padding: '20px', border: '1px solid #ccc' }}>
            <h2>All Tasks Created by Parent</h2>
            <ul style={{ listStyle: 'none', padding: 0 }}>
                {tasks.map((task) => (
                    <li key={task.id} style={{ borderBottom: '1px solid #ccc', padding: '10px 0' }}>
                        <span>{task.description}</span>
                        <select
                            value={task.assignedTo || ''}
                            onChange={(e) => handleKidSelection(task.id, e.target.value)}
                            style={{ marginLeft: '10px' }}
                        >
                            {kidsList.map((kid) => (
                                <option key={kid} value={kid}>
                                    {kid}
                                </option>
                            ))}
                        </select>
                    </li>
                ))}
            </ul>
            <button type={"submit"}>Submit</button>
            <Link to="/ParentNotificationsPage">
                <button>Go Back</button>
            </Link>
        </div>
    );
};

export default ParentTasksPage;
