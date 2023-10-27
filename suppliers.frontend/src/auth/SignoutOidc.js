import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { signoutRedirectCallback } from './user-service';
import Loader from "../Components/Loader/Loader";
import { Container } from "react-bootstrap";
import Header from "../Components/Header";

const SignoutOidc = () => {
    const router = useNavigate();
    useEffect(() =>{
        async function signoutAsync() {
            await signoutRedirectCallback();
            router('/');
        }
        signoutAsync();
    }, []);
    return (
        <>
            <Header/>
            <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
                <Loader/>
            </Container>
        </>
    );
};

export default SignoutOidc;