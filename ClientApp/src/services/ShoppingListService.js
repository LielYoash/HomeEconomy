// ShoppingListService.js

const API_URL = 'your_backend_api_url'; // Replace with your actual backend API URL

export const addShoppingListItem = (item) => {
    return fetch(`${API_URL}/shopping-list`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ item }),
    })
        .then((response) => {
            if (!response.ok) {
                throw new Error('Failed to add shopping list item');
            }
            return response.json();
        });
};

export const fetchShoppingList = () => {
    return fetch(`${API_URL}/shopping-list`)
        .then((response) => {
            if (!response.ok) {
                throw new Error('Failed to fetch shopping list');
            }
            return response.json();
        });
};
