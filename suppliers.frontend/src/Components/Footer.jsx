import React from "react";
import { Container } from "react-bootstrap";

const Footer = () => {
    return (
        <Container fluid style={{backgroundColor: "#212529", color: "#fff", padding: "10px"}}>
            <Container style={{display: 'flex', justifyContent: 'center', padding: '10px'}}>
                <p>Suppliers footer</p>
            </Container>
        </Container>
    );
};

export default Footer;