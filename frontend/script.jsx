const apiUrl = 'http://localhost:5027/api/user';

const form = document.getElementById('userForm');
const nameInput = document.getElementById('name');
const emailInput = document.getElementById('email');
const userTableBody = document.querySelector('#userTable tbody');

form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const user = {
        name: nameInput.value,
        email: emailInput.value
    };

    const res = await fetch(apiUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    });

    if (res.ok) {
        loadUsers();
        form.reset();
    }
});

async function loadUsers() {
    const res = await fetch(apiUrl);
    const users = await res.json();

    userTableBody.innerHTML = '';
    users.forEach(user => {
        const row = document.createElement('tr');

        row.innerHTML = `
            <td>${user.id}</td>
            <td><input value="${user.name}" id="name-${user.id}"></td>
            <td><input value="${user.email}" id="email-${user.id}"></td>
            <td>
                <button onclick="updateUser(${user.id})">Update</button>
                <button onclick="deleteUser(${user.id})">Delete</button>
            </td>
        `;
        userTableBody.appendChild(row);
    });
}

async function updateUser(id) {
    const name = document.getElementById(`name-${id}`).value;
    const email = document.getElementById(`email-${id}`).value;

    const res = await fetch(`${apiUrl}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ id, name, email })
    });

    if (res.ok) {
        loadUsers();
    }
}

async function deleteUser(id) {
    const res = await fetch(`${apiUrl}/${id}`, { method: 'DELETE' });

    if (res.ok) {
        loadUsers();
    }
}

loadUsers();
