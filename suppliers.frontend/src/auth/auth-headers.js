export function setAuthHeaders(access_token, id_token) {
    localStorage.setItem('token', access_token ? access_token : '');
    localStorage.setItem('id-token', id_token ? id_token : '');
}

export function setIsAuthenticated(isAuthenticated) {
    localStorage.setItem('isAuthenticated', isAuthenticated);
}