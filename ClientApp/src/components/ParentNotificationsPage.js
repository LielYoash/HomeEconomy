import React, { useEffect, useState } from 'react';
import getNotifications from '../services/GetNotifications';
import { Link } from 'react-router-dom';
import { fetchShoppingList, addShoppingListItem } from '../services/ShoppingListService'; // Assuming you have these services

const ParentNotificationsPage = () => {
    const [notifications, setNotifications] = useState([]);
    const [doneTasks] = useState(['task1', 'task2', 'task3']); // Fake list of done tasks
    const [shoppingList, setShoppingList] = useState(['Item1', 'Item2', 'Item3']); // Fake shopping list
    const [newShoppingItem, setNewShoppingItem] = useState('');

    useEffect(() => {
        fetchNotifications(); // Fetch notifications on initial render
        fetchShoppingListData(); // Fetch shopping list on initial render
    }, []);

    const fetchNotifications = () => {
        getNotifications()
            .then((data) => {
                setNotifications(data); // Set notifications in state
            })
            .catch((error) => {
                console.error('Error fetching notifications:', error);
            });
    };

    const fetchShoppingListData = () => {
        fetchShoppingList()
            .then((data) => {
                setShoppingList(data); // Set shopping list in state
            })
            .catch((error) => {
                console.error('Error fetching shopping list:', error);
            });
    };

    const handleAddToShoppingList = () => {
        setShoppingList((prevShoppingList) => [...prevShoppingList, ...doneTasks]);
    };

    const handleNewShoppingItemChange = (event) => {
        setNewShoppingItem(event.target.value);
    };

    const handleAddNewItem = () => {
        if (newShoppingItem.trim() !== '') {
            // Add the new item to the database
            addShoppingListItem(newShoppingItem)
                .then(() => {
                    // After successfully adding the item, fetch the updated shopping list
                    fetchShoppingListData();
                })
                .catch((error) => {
                    console.error('Error adding new shopping item:', error);
                });

            setNewShoppingItem('');
        }
    };
    const [taskList, setTaskList] = useState([
        { id: '1', name: 'Item1', done: false },
        { id: '2', name: 'Item2', done: false },
        { id: '3', name: 'Item3', done: false },
    ]);
    const handleTaskCheckboxChange = (taskId) => {
        setTaskList((prevTaskList) =>
            prevTaskList.map((task) =>
                task.id === taskId ? { ...task, done: !task.done } : task
            )
        );
    };

    return (
        <div className="container" style={{display: 'flex', border: '1px solid #ccc'}}>
            <div style={{flex: '1', padding: '20px', borderRight: '1px solid #ccc'}}>
                <h3>Task List</h3>
                <ul style={{listStyle: 'none', padding: 0}}>
                    {taskList.map((task) => (
                        <li key={task.id} style={{display: 'flex', alignItems: 'center', marginBottom: '8px'}}>
                            <input
                                type="checkbox"
                                checked={task.done}
                                onChange={() => handleTaskCheckboxChange(task.id)}
                                style={{marginRight: '4px', width: '30px', height: '16px'}}
                            />
                            <span style={{flex: '1'}}>{task.name}</span>
                        </li>
                    ))}
                </ul>
            </div>
            <div style={{flex: '1', padding: '20px', borderRight: '1px solid #ccc'}}>
                <h3>Unfinished tasks for the week</h3>
                <ul>
                    {taskList.map((task) => (
                        <li key={task.id}>{task.name}</li>
                    ))}
                </ul>
                <Link to="/addTask">
                    <button style={{margin: '5px'}}>Add Task</button>
                </Link>
            </div>
            <div style={{flex: '1', padding: '20px', borderRight: '1px solid #ccc'}}>
                <h3>Done tasks for this week</h3>
                <ul>
                    {doneTasks.map((task, index) => (
                        <li key={index}>{task}</li>
                    ))}
                </ul>
            </div>
            <div style={{
                flex: '1',
                padding: '20px',
                borderLeft: '1px solid #ccc',
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center'
            }}>
                <h2>Notifications for Parent User</h2>
                <ul style={{listStyle: 'none', padding: 0}}>
                    {notifications.map((notification) => (
                        <li key={notification.id} style={{borderBottom: '1px solid #ccc', padding: '10px 0'}}>
                            {notification.message}
                        </li>
                    ))}
                </ul>
                <Link to="/addNotifications">
                    <button style={{margin: '5px'}}>Add Notification</button>
                </Link>
                <Link to="/parentTasks">
                    <button style={{margin: '5px'}}>Go to Parent Tasks</button>
                </Link>
            </div>
            <div style={{flex: '1', padding: '20px', borderLeft: '1px solid #ccc'}}>
                <h3>Shopping List</h3>
                <ul>
                    {shoppingList.map((item, index) => (
                        <li key={index}>{item}</li>
                    ))}
                    <div style={{marginBottom: '1px', marginTop: '100px'}}>
                        <input
                            type="text"
                            value={newShoppingItem}
                            onChange={handleNewShoppingItemChange}
                            placeholder="New Item"
                            style={{marginRight: '5px'}}
                        />
                        <button onClick={handleAddNewItem} style={{margin: '5px'}}>Add New Item</button>
                    </div>
                </ul>
            </div>
        </div>
    );
};

export default ParentNotificationsPage;