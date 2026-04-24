const app = {
    currentUser: null,
    token: null,
    apiBaseUrl: 'http://localhost:5000/api',

    init() {
        this.loadTokenFromStorage();
        this.updateUI();
        this.showView('houses');
        this.loadHouses();
    },

    loadTokenFromStorage() {
        const token = localStorage.getItem('authToken');
        const user = localStorage.getItem('currentUser');
        if (token && user) {
            this.token = token;
            this.currentUser = JSON.parse(user);
        }
    },

    saveTokenToStorage(token, user) {
        localStorage.setItem('authToken', token);
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.token = token;
        this.currentUser = user;
        this.updateUI();
    },

    updateUI() {
        const loginBtn = document.getElementById('btnLogin');
        const registerBtn = document.getElementById('btnRegister');
        const userInfo = document.getElementById('userInfo');
        const userName = document.getElementById('userName');
        const browseBtn = document.getElementById('btnBrowseHouses');

        if (this.currentUser && this.token) {
            loginBtn.style.display = 'none';
            registerBtn.style.display = 'none';
            userInfo.style.display = 'block';
            userName.textContent = this.currentUser.email;
            browseBtn.style.display = 'block';
        } else {
            loginBtn.style.display = 'block';
            registerBtn.style.display = 'block';
            userInfo.style.display = 'none';
            browseBtn.style.display = 'block';
        }
    },

    showView(viewName) {
        // Hide all views
        const views = document.querySelectorAll('.view');
        views.forEach(view => view.classList.remove('active'));

        // Show selected view
        const view = document.getElementById(viewName);
        if (view) {
            view.classList.add('active');
        }

        // Load houses when switching to houses view
        if (viewName === 'houses') {
            this.loadHouses();
        }
    },

    async loadHouses() {
        const container = document.getElementById('housesContainer');
        const alert = document.getElementById('housesAlert');

        try {
            container.innerHTML = '<div class="loading">Loading houses...</div>';
            alert.classList.remove('show');

            const response = await fetch(`${this.apiBaseUrl}/house/all`);
            
            if (!response.ok) {
                throw new Error('Failed to load houses');
            }

            const houses = await response.json();
            this.renderHouses(houses, container);
        } catch (error) {
            console.error('Error loading houses:', error);
            this.showAlert(alert, 'Failed to load houses: ' + error.message, 'error');
            container.innerHTML = '';
        }
    },

    renderHouses(houses, container) {
        if (!houses || houses.length === 0) {
            container.innerHTML = '<p style="color: white; text-align: center;">No houses available</p>';
            return;
        }

        container.innerHTML = houses.map(house => `
            <div class="house-card">
                <img src="${house.imageUrl || 'https://via.placeholder.com/300x200?text=House'}" alt="${house.title}" class="house-image">
                <div class="house-content">
                    <h3 class="house-title">${this.escapeHtml(house.title)}</h3>
                    <p class="house-address">📍 ${this.escapeHtml(house.address)}</p>
                    <p class="house-price">$${house.pricePerMonth}/month</p>
                    <p class="house-description">${this.escapeHtml(house.description)}</p>
                    <div class="house-actions">
                        <button class="btn btn-primary" onclick="app.viewHouseDetails(${house.id})">View Details</button>
                        ${app.currentUser ? `<button class="btn btn-secondary" onclick="app.rentHouse(${house.id})">Rent Now</button>` : ''}
                    </div>
                </div>
            </div>
        `).join('');
    },

    viewHouseDetails(id) {
        alert('House Details - ID: ' + id + '\n(Detail page coming soon)');
    },

    rentHouse(id) {
        if (!this.currentUser) {
            this.showView('login');
            return;
        }
        alert('Rental feature coming soon!');
    },

    logout() {
        localStorage.removeItem('authToken');
        localStorage.removeItem('currentUser');
        this.token = null;
        this.currentUser = null;
        this.updateUI();
        this.showView('login');
        this.showAlert(document.getElementById('loginAlert'), 'You have been logged out', 'success');
    },

    showAlert(element, message, type) {
        element.textContent = message;
        element.className = `alert show alert-${type}`;
        setTimeout(() => {
            element.classList.remove('show');
        }, 5000);
    },

    escapeHtml(text) {
        const div = document.createElement('div');
        div.textContent = text;
        return div.innerHTML;
    }
};

// Initialize app when DOM is ready
document.addEventListener('DOMContentLoaded', () => {
    app.init();
});
