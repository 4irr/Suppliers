import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { signinRedirectCallback } from './user-service';
import { Container } from "react-bootstrap";
import Loader from "../Components/Loader/Loader";

const SigninOidc = () => {
    const router = useNavigate();
    useEffect(() => {
        async function signinAsync() {
            try {
                await signinRedirectCallback();
                router('/');
            }
            catch(e) {
                console.error(e);
            }
        }
        signinAsync();
    }, []);
    return (
        <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
            <Loader/>
        </Container>
    );
};

export default SigninOidc;