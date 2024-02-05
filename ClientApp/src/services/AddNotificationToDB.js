const addNotificationToDB = async (notification) => {
    try {
      const response = await fetch('http://localhost:5000/api/addNotification', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(notification),
      });
  
      if (!response.ok) {
        throw new Error('Failed to add notification');
      }
  
      const data = await response.json();
      return data; // You can return any relevant data from the server response
    } catch (error) {
      throw new Error(error.message);
    }
  };
  
  export default addNotificationToDB;
  