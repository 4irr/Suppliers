import React from "react";
import { signinRedirect, signoutRedirect } from "../auth/user-service";
import { Button, Container, Nav, NavDropdown, Navbar } from "react-bootstrap";

const Header = ({role}) => {
    return (
        <Navbar expand="lg" variant="dark" bg="dark" className="bg-body-dark">
            <Container>
                <Navbar.Brand href={role===undefined ? '/' : (role === 'Admin' ? '/admin' : (role === 'Client' ? '/client' : '/supplier'))}>
                    Suppliers
                </Navbar.Brand>
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
                            <Nav.Link href="/supplier/tenders">Тендеры</Nav.Link>
                        </Nav>
                    }
                    { role === 'Admin' &&
                        <Nav className="me-auto">
                            <Nav.Link href="/admin/user-data">Данные</Nav.Link>
                            <Nav.Link href="/admin/user-activity">Активность</Nav.Link>
                            <Nav.Link href="/admin/licenses">Лицензии</Nav.Link>
                            <Nav.Link href="/admin/orders">Заказы</Nav.Link>
                            <Nav.Link href="/admin/tenders">Тендеры</Nav.Link>
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
                            <NavDropdown title="Меню" id="basic-nav-dropdown" menuVariant="light" style={{marginRight: '20px'}}>
                                {role === 'Supplier' && <NavDropdown.ItemText>Поставщик</NavDropdown.ItemText>}
                                {role === 'Client' && <NavDropdown.ItemText>Заказчик</NavDropdown.ItemText>}
                                {role === 'Admin' && <NavDropdown.ItemText>Администратор</NavDropdown.ItemText>}
                                <NavDropdown.Divider/>
                                <NavDropdown.Item href="/user/profile">Мой профиль</NavDropdown.Item>
                                {role === 'Supplier' && <NavDropdown.Item href="/supplier/save-license">Загрузить лицензию</NavDropdown.Item>}
                            </NavDropdown>
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