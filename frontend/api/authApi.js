const authApi = {
    /**
     * Login user
     * @param {string} email - User email
     * @param {string} password - User password
     * @returns {Promise<{token: string, user: Object}>}
     */
    async login(email, password) {
        try {
            const response = await http.post('/auth/login', {
                email: email,
                password: password
            });

            if (response.token && response.user) {
                app.saveTokenToStorage(response.token, response.user);
                return response;
            }

            throw new Error('Invalid response from server');
        } catch (error) {
            console.error('Login error:', error);
            throw error;
        }
    },

    /**
     * Register new user
     * @param {string} email - User email
     * @param {string} password - User password
     * @param {string} confirmPassword - Confirm password
     * @returns {Promise<Object>}
     */
    async register(email, password, confirmPassword) {
        try {
            // Validate inputs
            if (!email || !password || !confirmPassword) {
                throw new Error('All fields are required');
            }

            if (password.length < 6) {
                throw new Error('Password must be at least 6 characters long');
            }

            if (password !== confirmPassword) {
                throw new Error('Passwords do not match');
            }

            const response = await http.post('/auth/register', {
                email: email,
                password: password,
                confirmPassword: confirmPassword
            });

            return response;
        } catch (error) {
            console.error('Registration error:', error);
            throw error;
        }
    },

    /**
     * Logout user
     */
    logout() {
        app.logout();
    },

    /**
     * Get current user
     * @returns {Object|null}
     */
    getCurrentUser() {
        return app.currentUser;
    },

    /**
     * Check if user is authenticated
     * @returns {boolean}
     */
    isAuthenticated() {
        return !!(app.token && app.currentUser);
    },

    /**
     * Get auth token
     * @returns {string|null}
     */
    getToken() {
        return app.token;
    }
};
