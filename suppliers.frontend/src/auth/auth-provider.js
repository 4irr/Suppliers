import React, { useRef, useEffect } from 'react';
import { setAuthHeaders, setIsAuthenticated } from './auth-headers';

const AuthProvider = ({
    userManager: manager,
    children
}) => {

    let userManager = useRef();

    useEffect(() => {
        userManager.current = manager;
        const onUserLoaded = (user) => {
            console.log('User loaded: ', user);
            setAuthHeaders(user.access_token, user.id_token);
            setIsAuthenticated(true);
        };
        const onUserUnloaded = () => {
            setAuthHeaders(null, null);
            console.log('User unloaded');
            setIsAuthenticated(false);
        };
        const onAccessTokenExpiring = () => {
            console.log('User token expiring');
        };
        const onAccessTokenExpired = () => {
            console.log('User token expired');
        };
        const onUserSignedOut = () => {
            console.log('User signed out');
        };

        userManager.current.events.addUserLoaded(onUserLoaded);
        userManager.current.events.addUserUnloaded(onUserUnloaded);
        userManager.current.events.addAccessTokenExpiring(onAccessTokenExpiring);
        userManager.current.events.addAccessTokenExpired(onAccessTokenExpired);
        userManager.current.events.addUserSignedOut(onUserSignedOut);

        return function cleanup() {
            if (userManager && userManager.current) {
                userManager.current.events.removeUserLoaded(onUserLoaded);
                userManager.current.events.removeUserUnloaded(onUserUnloaded);
                userManager.current.events.removeAccessTokenExpiring(onAccessTokenExpiring);
                userManager.current.events.removeAccessTokenExpired(onAccessTokenExpired);
                userManager.current.events.removeUserSignedOut(onUserSignedOut);
            }
        };
    }, [manager]);

    return React.Children.only(children);
};

export default AuthProvider;