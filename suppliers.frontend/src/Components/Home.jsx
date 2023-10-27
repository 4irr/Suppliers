import React from "react";
import { Container } from "react-bootstrap";
import Header from "./Header";
import { loadUser, signinRedirect } from "../auth/user-service";
import { useNavigate } from "react-router-dom";

const Home = () => {
    var router = useNavigate();

    if(localStorage.getItem('isAuthenticated') === 'true' && !sessionStorage.getItem('oidc.user:https://localhost:7073:suppliers-web-api'))
        signinRedirect();

    loadUser().then(value => {
        if(value===null)
            return;
        else {
            if(value.profile.role === 'Admin')
                router('/admin');
        }
    });

    return (
        <>
            <Header/>
            <Container>
                <h2>Домашняя страница</h2>
            </Container>
        </>
    );
};

export default Home;