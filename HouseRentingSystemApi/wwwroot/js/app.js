const tokenKey = "hrs_jwt_token";

const housesGrid = document.getElementById("housesGrid");
const myHousesGrid = document.getElementById("myHousesGrid");
const messageBox = document.getElementById("messageBox");

const authState = document.getElementById("authState");
const createForm = document.getElementById("createHouseForm");
const createAuthNote = document.getElementById("createAuthNote");

const guestActions = document.getElementById("guestActions");
const userActions = document.getElementById("userActions");

const authModalTitle = document.getElementById("authModalTitle");
const loginForm = document.getElementById("loginForm");
const registerForm = document.getElementById("registerForm");

const openLoginBtn = document.getElementById("openLoginBtn");
const openRegisterBtn = document.getElementById("openRegisterBtn");

const housesSection = document.getElementById("houses");
const myHousesSection = document.getElementById("myHouses");
const createSection = document.getElementById("create");

const addHouseNavItem = document.getElementById("addHouseNavItem");
const myHousesNavItem = document.getElementById("myHousesNavItem");

const addHouseLink = document.getElementById("addHouseLink");
const myHousesLink = document.getElementById("myHousesLink");
const housesLink = document.getElementById("housesLink");

const categoryFilter = document.getElementById("categoryFilter");
const searchInput = document.getElementById("searchInput");
const sortingSelect = document.getElementById("sortingSelect");

const previousPageBtn = document.getElementById("previousPageBtn");
const nextPageBtn = document.getElementById("nextPageBtn");
const pageInfo = document.getElementById("pageInfo");

const authModal = new bootstrap.Modal(document.getElementById("authModal"));

let currentPage = 1;
let maxPage = 1;

document.getElementById("refreshBtn").addEventListener("click", () => {
    currentPage = 1;
    loadHouses();
});

document.getElementById("refreshMyHousesBtn").addEventListener("click", loadMyHouses);
document.getElementById("logoutBtn").addEventListener("click", logout);

loginForm.addEventListener("submit", login);
registerForm.addEventListener("submit", register);
createForm.addEventListener("submit", createHouse);

openLoginBtn.addEventListener("click", () => openAuthModal("login"));
openRegisterBtn.addEventListener("click", () => openAuthModal("register"));

categoryFilter.addEventListener("change", () => {
    currentPage = 1;
    loadHouses();
});

sortingSelect.addEventListener("change", () => {
    currentPage = 1;
    loadHouses();
});

searchInput.addEventListener("input", () => {
    currentPage = 1;
    loadHouses();
});

previousPageBtn.addEventListener("click", () => {
    if (currentPage > 1) {
        currentPage--;
        loadHouses();
    }
});

nextPageBtn.addEventListener("click", () => {
    if (currentPage < maxPage) {
        currentPage++;
        loadHouses();
    }
});

addHouseLink.addEventListener("click", openCreateSection);
myHousesLink.addEventListener("click", openMyHousesSection);
housesLink.addEventListener("click", openHousesSection);

updateAuthState();
loadHouses();

function showMessage(text, type = "info") {
    messageBox.className = `alert alert-${type} mt-3`;
    messageBox.textContent = text;
    messageBox.classList.remove("d-none");
}

function hideMessage() {
    messageBox.classList.add("d-none");
}

function getToken() {
    return localStorage.getItem(tokenKey);
}

function getTokenPayload() {
    const token = getToken();
    if (!token) {
        return null;
    }

    try {
        const payload = token.split(".")[1];
        const normalizedPayload = payload.replace(/-/g, "+").replace(/_/g, "/");
        const decodedPayload = atob(normalizedPayload);

        return JSON.parse(decodedPayload);
    } catch (err) {
        console.error("Invalid token:", err);
        localStorage.removeItem(tokenKey);
        return null;
    }
}

function getUserRoles() {
    const payload = getTokenPayload();
    if (!payload) {
        return [];
    }

    const roleClaim = payload["role"] ||
        payload["roles"] ||
        payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    if (!roleClaim) {
        return [];
    }

    return Array.isArray(roleClaim) ? roleClaim : [roleClaim];
}

function isAgent() {
    return getUserRoles().includes("Agent");
}

function updateAuthState() {
    const isLogged = !!getToken();
    const roles = getUserRoles();
    const agent = roles.includes("Agent");

    if (isLogged) {
        authState.textContent = roles.length ? roles.join(", ") : "Logged In";
        authState.className = agent ? "badge text-bg-primary" : "badge text-bg-success";

        guestActions.classList.add("d-none");
        userActions.classList.remove("d-none");

        addHouseNavItem.classList.toggle("d-none", !agent);
        myHousesNavItem.classList.remove("d-none");

        createSection.classList.add("d-none");
    } else {
        authState.textContent = "Guest";
        authState.className = "badge text-bg-secondary";

        guestActions.classList.remove("d-none");
        userActions.classList.add("d-none");

        addHouseNavItem.classList.add("d-none");
        myHousesNavItem.classList.add("d-none");

        createSection.classList.add("d-none");
        myHousesSection.classList.add("d-none");
        housesSection.classList.remove("d-none");
    }

    Array.from(createForm.elements).forEach(el => {
        el.disabled = !agent;
    });

    if (!isLogged) {
        createAuthNote.textContent = "You must be logged in as an agent to add houses.";
        createAuthNote.classList.remove("d-none");
    } else if (!agent) {
        createAuthNote.textContent = "Only agents can add houses.";
        createAuthNote.classList.remove("d-none");
    } else {
        createAuthNote.classList.add("d-none");
    }
}

function logout() {
    localStorage.removeItem(tokenKey);
    updateAuthState();
    showMessage("Logged out.", "secondary");
}

function openAuthModal(mode) {
    if (mode === "register") {
        authModalTitle.textContent = "Register";
        loginForm.classList.add("d-none");
        registerForm.classList.remove("d-none");
    } else {
        authModalTitle.textContent = "Login";
        registerForm.classList.add("d-none");
        loginForm.classList.remove("d-none");
    }

    authModal.show();
}

function openHousesSection(e) {
    e.preventDefault();

    housesSection.classList.remove("d-none");
    myHousesSection.classList.add("d-none");
    createSection.classList.add("d-none");

    housesSection.scrollIntoView({ behavior: "smooth", block: "start" });
}

function openCreateSection(e) {
    e.preventDefault();

    if (!getToken()) {
        showMessage("You must be logged in as an agent to add houses.", "warning");
        openAuthModal("login");
        return;
    }

    if (!isAgent()) {
        showMessage("Only agents can add houses.", "warning");
        return;
    }

    housesSection.classList.add("d-none");
    myHousesSection.classList.add("d-none");
    createSection.classList.remove("d-none");

    createSection.scrollIntoView({ behavior: "smooth", block: "start" });
}

async function openMyHousesSection(e) {
    e.preventDefault();

    if (!getToken()) {
        showMessage("You must be logged in to see your houses.", "warning");
        openAuthModal("login");
        return;
    }

    housesSection.classList.add("d-none");
    createSection.classList.add("d-none");
    myHousesSection.classList.remove("d-none");

    await loadMyHouses();

    myHousesSection.scrollIntoView({ behavior: "smooth", block: "start" });
}
async function login(e) {
    e.preventDefault();
    hideMessage();

    const payload = {
        email: document.getElementById("loginEmail").value.trim(),
        password: document.getElementById("loginPassword").value.trim()
    };

    const response = await fetch("/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(payload)
    });

    if (!response.ok) {
        showMessage("Invalid login credentials.", "danger");
        return;
    }

    const loginResult = await response.json();

    localStorage.setItem(tokenKey, loginResult.token);

    updateAuthState();
    authModal.hide();
    loginForm.reset();

    showMessage("Login successful.", "success");
}

async function register(e) {
    e.preventDefault();
    hideMessage();

    const payload = {
        username: document.getElementById("registerUsername").value.trim(),
        email: document.getElementById("registerEmail").value.trim(),
        password: document.getElementById("registerPassword").value.trim()
    };

    const response = await fetch("/register", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(payload)
    });

    if (!response.ok) {
        const errorText = await response.text();
        console.error("Registration failed:", response.status, errorText);
        showMessage("Registration failed.", "danger");
        return;
    }

    showMessage("Registration successful. You can now login.", "success");
    registerForm.reset();
    openAuthModal("login");
}

async function loadHouses() {
    housesGrid.innerHTML = `
        <div class="col-12">
            <div class="alert alert-light">Loading houses...</div>
        </div>`;

    try {
        const query = new URLSearchParams();

        if (categoryFilter.value) {
            query.append("Category", categoryFilter.value);
        }

        if (searchInput.value.trim()) {
            query.append("SearchTerm", searchInput.value.trim());
        }

        query.append("Sorting", sortingSelect.value);
        query.append("CurrentPage", currentPage.toString());

        const response = await fetch(`/api/House/All?${query.toString()}`);

        if (!response.ok) {
            housesGrid.innerHTML = `
                <div class="col-12">
                    <div class="alert alert-danger">Could not load houses.</div>
                </div>`;
            return;
        }

        const result = await response.json();

        const houses = result.houses || result.Houses || [];
        const categories = result.categories || result.Categories || [];
        const totalHousesCount = result.totalHousesCount || result.TotalHousesCount || 0;

        fillCategories(categories);

        maxPage = Math.ceil(totalHousesCount / 3);
        if (maxPage < 1) {
            maxPage = 1;
        }

        updatePagination();

        if (!houses.length) {
            housesGrid.innerHTML = `
                <div class="col-12">
                    <div class="alert alert-secondary">No houses found.</div>
                </div>`;
            return;
        }

        housesGrid.innerHTML = houses.map(renderHouseCard).join("");

    } catch (err) {
        console.error(err);

        housesGrid.innerHTML = `
            <div class="col-12">
                <div class="alert alert-danger">Error while loading houses.</div>
            </div>`;
    }
}

async function loadMyHouses() {
    myHousesGrid.innerHTML = `
        <div class="col-12">
            <div class="alert alert-light">Loading your houses...</div>
        </div>`;

    const token = getToken();

    if (!token) {
        myHousesGrid.innerHTML = `
            <div class="col-12">
                <div class="alert alert-warning">Login first to see your houses.</div>
            </div>`;
        return;
    }

    try {
        const response = await fetch("/api/House/Mine", {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (!response.ok) {
            const errorText = await response.text();
            console.error("My Houses failed:", response.status, errorText);

            myHousesGrid.innerHTML = `
                <div class="col-12">
                    <div class="alert alert-danger">Could not load your houses.</div>
                </div>`;
            return;
        }

        const houses = await response.json();

        if (!houses.length) {
            myHousesGrid.innerHTML = `
                <div class="col-12">
                    <div class="alert alert-secondary">You have not added any houses yet.</div>
                </div>`;
            return;
        }

        myHousesGrid.innerHTML = houses.map(renderHouseCard).join("");

    } catch (err) {
        console.error(err);

        myHousesGrid.innerHTML = `
            <div class="col-12">
                <div class="alert alert-danger">Error while loading your houses.</div>
            </div>`;
    }
}

function renderHouseCard(h) {
    return `
        <div class="col-12 col-sm-6 col-lg-4">
            <div class="card house-card h-100 shadow-sm">
                <img src="${escapeHtml(h.imageUrl)}" class="card-img-top" alt="${escapeHtml(h.title)}">
                <div class="card-body d-flex flex-column">
                    <h5 class="card-title">${escapeHtml(h.title)}</h5>
                    <p class="card-text mb-1">${escapeHtml(h.address)}</p>
                    <p class="small text-muted mb-1">${escapeHtml(h.category || "")}</p>
                    <p class="small text-muted mb-3">$${Number(h.pricePerMonth || 0).toFixed(2)} / month</p>
                    <button class="btn btn-outline-primary mt-auto" onclick="showDetails(${h.id})">
                        Details
                    </button>
                </div>
            </div>
        </div>`;
}

function fillCategories(categories) {
    const currentValue = categoryFilter.value;

    categoryFilter.innerHTML = `<option value="">All</option>`;

    categories.forEach(category => {
        categoryFilter.innerHTML += `
            <option value="${escapeHtml(category)}">${escapeHtml(category)}</option>`;
    });

    categoryFilter.value = currentValue;
}

function updatePagination() {
    pageInfo.textContent = `Page ${currentPage} of ${maxPage}`;

    previousPageBtn.disabled = currentPage <= 1;
    nextPageBtn.disabled = currentPage >= maxPage;
}

async function showDetails(id) {
    const response = await fetch(`/api/House/${id}`);

    if (!response.ok) {
        showMessage("House not found.", "warning");
        return;
    }

    const h = await response.json();

    const detailsBody = document.getElementById("detailsBody");

    detailsBody.innerHTML = `
        <img src="${escapeHtml(h.imageUrl)}" alt="${escapeHtml(h.title)}">
        <h4>${escapeHtml(h.title)}</h4>
        <p><strong>Address:</strong> ${escapeHtml(h.address)}</p>
        <p><strong>Description:</strong> ${escapeHtml(h.description || "No description")}</p>
        <p><strong>Price:</strong> $${Number(h.pricePerMonth || 0).toFixed(2)} / month</p>
        <p><strong>Category:</strong> ${escapeHtml(h.category || "")}</p>
        <p><strong>Status:</strong> ${h.isRented ? "Rented" : "Not rented"}</p>
        <hr>
        <h5>Owner Info</h5>
        <p><strong>Name:</strong> ${escapeHtml(h.ownerName || "Unknown")}</p>
        <p><strong>Email:</strong> ${escapeHtml(h.ownerEmail || "No email")}</p>
    `;

    const modal = new bootstrap.Modal(document.getElementById("detailsModal"));
    modal.show();
}

async function createHouse(e) {
    e.preventDefault();
    hideMessage();

    const token = getToken();

    if (!token) {
        showMessage("Login as an agent first to create a house.", "warning");
        return;
    }

    if (!isAgent()) {
        showMessage("Only agents can create houses.", "warning");
        return;
    }

    const payload = {
        title: document.getElementById("houseTitle").value.trim(),
        address: document.getElementById("houseAddress").value.trim(),
        imageUrl: document.getElementById("houseImageUrl").value.trim(),
        description: document.getElementById("houseDescription").value.trim(),
        pricePerMonth: Number(document.getElementById("housePrice").value),
        category: Number(document.getElementById("houseCategory").value)
    };

    const response = await fetch("/api/House", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify(payload)
    });

    if (!response.ok) {
        const errorText = await response.text();
        console.error("Create failed:", response.status, errorText);
        showMessage(`Create failed. Status: ${response.status}`, "danger");
        return;
    }

    showMessage("House created successfully.", "success");
    e.target.reset();

    currentPage = 1;
    await loadHouses();

    if (!myHousesSection.classList.contains("d-none")) {
        await loadMyHouses();
    }
}

function resetFilters() {
    categoryFilter.value = "";
    searchInput.value = "";
    sortingSelect.value = "0";
    currentPage = 1;

    loadHouses();
}

function escapeHtml(text) {
    const div = document.createElement("div");
    div.textContent = text ?? "";
    return div.innerHTML;
}

window.showDetails = showDetails;
window.resetFilters = resetFilters;
