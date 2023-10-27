import { UserManager } from "oidc-client";

const userManagerSettings = {
    client_id: 'suppliers-web-api',
    redirect_uri: 'http://localhost:3000/signin-oidc',
    post_logout_redirect_uri: 'http://localhost:3000/signout-oidc',
    response_type: 'code',
    scope: 'SuppliersWebAPI openid profile',
    authority: 'https://localhost:7073'
};

const userManager = new UserManager(userManagerSettings);

export async function loadUser() {
    return await userManager.getUser();
};

export const signinRedirect = () => userManager.signinRedirect();

export const signinRedirectCallback = () => userManager.signinRedirectCallback();

export const signoutRedirect = (args) => {
    userManager.clearStaleState();
    userManager.removeUser();
    return userManager.signoutRedirect(args);
}

export const signoutRedirectCallback = () => {
    userManager.clearStaleState();
    userManager.removeUser();
    return userManager.signoutRedirectCallback();
}

export default userManager;