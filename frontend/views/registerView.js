const registerView = {
    form: null,
    alert: null,

    init() {
        this.form = document.getElementById('registerForm');
        this.alert = document.getElementById('registerAlert');
    },

    /**
     * Handle register form submission
     * @param {Event} event - Form submit event
     */
    async handleRegister(event) {
        event.preventDefault();

        const email = document.getElementById('registerEmail').value.trim();
        const password = document.getElementById('registerPassword').value;
        const confirmPassword = document.getElementById('registerConfirmPassword').value;

        // Validate inputs
        if (!email || !password || !confirmPassword) {
            this.showAlert('Please fill in all fields', 'error');
            return;
        }

        if (!this.isValidEmail(email)) {
            this.showAlert('Please enter a valid email', 'error');
            return;
        }

        if (password.length < 6) {
            this.showAlert('Password must be at least 6 characters long', 'error');
            return;
        }

        if (password !== confirmPassword) {
            this.showAlert('Passwords do not match', 'error');
            return;
        }

        // Disable submit button during request
        const submitBtn = this.form.querySelector('button[type="submit"]');
        const originalText = submitBtn.textContent;
        submitBtn.disabled = true;
        submitBtn.textContent = 'Registering...';

        try {
            // Make register request
            const response = await authApi.register(email, password, confirmPassword);

            this.showAlert('Registration successful! You can now login.', 'success');

            // Reset form
            this.form.reset();

            // Redirect to login view after short delay
            setTimeout(() => {
                app.showView('login');
                // Clear the email field in login form for user convenience
                document.getElementById('loginEmail').value = email;
                document.getElementById('loginPassword').focus();
            }, 1500);
        } catch (error) {
            const errorMessage = error.data?.message || error.message || 'Registration failed';
            this.showAlert(errorMessage, 'error');
            console.error('Registration error:', error);
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

// Initialize register view
document.addEventListener('DOMContentLoaded', () => {
    registerView.init();
});
