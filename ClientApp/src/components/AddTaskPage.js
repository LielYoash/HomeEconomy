// AddTaskPage.js
import React, { useState } from 'react';

const AddTaskPage = ({ onAddTask }) => {
    const [newTask, setNewTask] = useState('');

    const handleNewTaskChange = (event) => {
        setNewTask(event.target.value);
    };

    const handleAddTask = () => {
        if (newTask.trim() !== '') {
            // Call the parent component's onAddTask function to add the new task
            onAddTask(newTask);

            // Reset the input field
            setNewTask('');
        }
    };

    return (
        <div>
            <h2>Add New Task</h2>
            <div>
                <label htmlFor="newTask">Task Name:</label>
                <input
                    type="text"
                    id="newTask"
                    value={newTask}
                    onChange={handleNewTaskChange}
                />
                <button onClick={handleAddTask}>Add Task</button>
            </div>
        </div>
    );
};

export default AddTaskPage;
