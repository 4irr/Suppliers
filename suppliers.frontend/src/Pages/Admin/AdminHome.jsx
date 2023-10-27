import React from "react";
import { Container } from "react-bootstrap";
import Header from "../../Components/Header";

const AdminHome = () => {
    return (
        <>
            <Header role='Admin'/>
            <Container className='content-container'>
                <h2>Страница администратора</h2>
            </Container>
        </>
    );
};

export default AdminHome;