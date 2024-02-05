import React, { useEffect, useState } from 'react';
import getNotifications from '../services/GetNotifications';
import { Link } from 'react-router-dom';

const ChildPage = () => {
    const [notifications, setNotifications] = useState([]);
    const [taskList, setTaskList] = useState([
        { id: '1', name: 'Item1', done: false },
        { id: '2', name: 'Item2', done: false },
        { id: '3', name: 'Item3', done: false },
    ]);

    useEffect(() => {
        fetchNotifications();
    }, []);

    const fetchNotifications = () => {
        getNotifications()
            .then((data) => {
                setNotifications(data);
            })
            .catch((error) => {
                console.error('Error fetching notifications:', error);
            });
    };

    const handleTaskCheckboxChange = (taskId) => {
        setTaskList((prevTaskList) =>
            prevTaskList.map((task) =>
                task.id === taskId ? { ...task, done: !task.done } : task
            )
        );
    };

    return (
        <div className="container" style={{ display: 'flex', border: '1px solid #ccc' }}>
            <div style={{ flex: '1', padding: '20px' }}>
                <h3>Task List</h3>
                <ul style={{ listStyle: 'none', padding: 0 }}>
                    {taskList.map((task) => (
                        <li key={task.id} style={{ display: 'flex', alignItems: 'center', marginBottom: '8px' }}>
                            <input
                                type="checkbox"
                                checked={task.done}
                                onChange={() => handleTaskCheckboxChange(task.id)}
                                style={{ marginRight: '4px', width: '30px', height: '16px' }}
                            />
                            <span style={{ flex: '1' }}>{task.name}</span>
                        </li>
                    ))}
                </ul>
            </div>
            <div style={{ flex: '1', padding: '20px', borderLeft: '1px solid #ccc', display: 'flex', flexDirection: 'column' }}>
                <h2>Notifications for "ChildName"</h2>
                <ul style={{ listStyle: 'none', padding: 0 }}>
                    {notifications.map((notification) => (
                        <li key={notification.id} style={{ borderBottom: '1px solid #ccc', padding: '10px 0' }}>
                            {notification.message}
                        </li>
                    ))}
                </ul>
                <Link to="/addNotifications">
                    <button>Add Notification</button>
                </Link>
            </div>
        </div>
    );
};

export default ChildPage;
