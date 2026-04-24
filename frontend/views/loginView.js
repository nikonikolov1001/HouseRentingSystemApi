const loginView = {
    form: null,
    alert: null,

    init() {
        this.form = document.getElementById('loginForm');
        this.alert = document.getElementById('loginAlert');
    },

    /**
     * Handle login form submission
     * @param {Event} event - Form submit event
     */
    async handleLogin(event) {
        event.preventDefault();

        const email = document.getElementById('loginEmail').value.trim();
        const password = document.getElementById('loginPassword').value;

        // Validate inputs
        if (!email || !password) {
            this.showAlert('Please fill in all fields', 'error');
            return;
        }

        if (!this.isValidEmail(email)) {
            this.showAlert('Please enter a valid email', 'error');
            return;
        }

        // Disable submit button during request
        const submitBtn = this.form.querySelector('button[type="submit"]');
        const originalText = submitBtn.textContent;
        submitBtn.disabled = true;
        submitBtn.textContent = 'Logging in...';

        try {
            // Make login request
            const response = await authApi.login(email, password);

            this.showAlert('Login successful! Redirecting...', 'success');

            // Reset form
            this.form.reset();

            // Redirect to houses view after short delay
            setTimeout(() => {
                app.showView('houses');
            }, 1000);
        } catch (error) {
            const errorMessage = error.data?.message || error.message || 'Login failed';
            this.showAlert(errorMessage, 'error');
            console.error('Login error:', error);
        } finally {
            // Re-enable submit button
            submitBtn.disabled = false;
            submitBtn.textContent = originalText;
        }
    },

    /**
     * Show alert message
     * @param {string} message - Alert message
     * @param {string} type - Alert type (success, error, info)
     */
    showAlert(message, type) {
        app.showAlert(this.alert, message, type);
    },

    /**
     * Validate email format
     * @param {string} email - Email to validate
     * @returns {boolean}
     */
    isValidEmail(email) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    }
};

// Initialize login view
document.addEventListener('DOMContentLoaded', () => {
    loginView.init();
});
