import React from "react";
import { signinRedirect, signoutRedirect } from "../auth/user-service";
import { Button, Container, Nav, Navbar, NavDropdown } from "react-bootstrap";

const Header = ({role}) => {
    return (
        <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark" style={{height: "80px"}}>
            <Container>
                <Navbar.Brand href="/">Suppliers</Navbar.Brand>
                <Navbar.Toggle aria-controls="responsive-navbar-nav"/>
                <Navbar.Collapse id="responsive-navbar-nav">
                    { role === undefined &&
                        <Nav className="me-auto">
                            <Nav.Link href="/">Главная</Nav.Link>
                        </Nav>
                    }
                    { role === 'Admin' &&
                        <Nav className="me-auto">
                            <Nav.Link href="/products">Товары</Nav.Link>
                            <Nav.Link href="/orders">Заказы</Nav.Link>
                            <Nav.Link href="/analytics">Аналитика</Nav.Link>
                        </Nav>
                    }
                    <Nav>
                        { role === undefined 
                        ?
                        <Button onClick={() => signinRedirect() }>Вход</Button>
                        :
                        <Button onClick={() => signoutRedirect({ 'id_token_hint': localStorage.getItem('id-token') })}>Выход</Button>
                        }
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
};

export default Header;