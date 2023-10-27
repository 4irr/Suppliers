import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { signoutRedirectCallback } from './user-service';
import Loader from "../Components/Loader/Loader";
import { Container } from "react-bootstrap";

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
        <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
            <Loader/>
        </Container>
    );
};

export default SignoutOidc;