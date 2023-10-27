import React from "react";
import { Container } from "react-bootstrap";

const Footer = () => {
    return (
        <Container fluid style={{backgroundColor: "#212529", color: "#fff", padding: "10px"}}>
            <Container style={{display: 'flex', justifyContent: 'center', padding: '10px', flexDirection: 'column', alignItems: 'center'}}>
                <p className="my-1">© 2023 Suppliers</p>
                <p className="my-0">All rights reserved</p>
            </Container>
        </Container>
    );
};

export default Footer;