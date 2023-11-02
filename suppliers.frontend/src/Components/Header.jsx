import React from "react";
import { signinRedirect, signoutRedirect } from "../auth/user-service";
import { Button, Container, Nav, Navbar } from "react-bootstrap";

const Header = ({role}) => {
    return (
        <Navbar expand="lg" variant="dark" bg="dark" className="bg-body-dark">
            <Container>
                <Navbar.Brand href="/">Suppliers</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav"/>
                <Navbar.Collapse id="basic-navbar-nav" style={{background: "transparent", borderRadius: "10px", padding: "10px", margin: "20px 0"}}>
                    { role === undefined &&
                        <Nav className="me-auto">
                            <Nav.Link href="/">Главная</Nav.Link>
                        </Nav>
                    }
                    { role === 'Supplier' &&
                        <Nav className="me-auto">
                            <Nav.Link href="/supplier/products">Товары</Nav.Link>
                            <Nav.Link href="/supplier/orders">Заказы</Nav.Link>
                            <Nav.Link href="/supplier/analytics">Аналитика</Nav.Link>
                        </Nav>
                    }
                    { role === 'Admin' &&
                        <Nav className="me-auto">
                            <Nav.Link href="/admin/administration">Администрирование</Nav.Link>
                        </Nav>
                    }
                    { role === 'Client' &&
                        <Nav className="me-auto">
                            <Nav.Link href="/client/suppliers">Поставщики</Nav.Link>    
                            <Nav.Link href="/client/orders">Заказы</Nav.Link>
                            <Nav.Link href="/client/tenders">Тендеры</Nav.Link>
                            <Nav.Link href="/client/analytics">Аналитика</Nav.Link>
                        </Nav>
                    }
                    <Nav>
                        { role === undefined 
                        ?
                        <Button onClick={() => signinRedirect() }>Вход</Button>
                        :
                        <>
                            <h3 style={{color: "white", margin: '0 20px 0 0'}}>{role}</h3>
                            <Button onClick={() => signoutRedirect({ 'id_token_hint': localStorage.getItem('id-token') })}>Выход</Button>
                        </>
                        }
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
};

export default Header;