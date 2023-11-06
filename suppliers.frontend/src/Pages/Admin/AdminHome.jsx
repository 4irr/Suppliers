import React from "react";
import { Container, Row, Col, Card, Button } from "react-bootstrap";
import Header from "../../Components/Header";

const AdminHome = () => {
    return (
        <>
            <Header role='Admin'/>
            <Container className='content-container'>
                <h2 style={{textAlign: 'center', marginBottom: '30px'}}>Добро пожаловать</h2>
                <Row style={{borderBottom: '2px solid black', paddingBottom: '60px'}}>
                    <Col md={7}>
                        <img style={{width: '100%'}} src="images/admin.jpg" alt='admin.jpg'/>
                    </Col>
                    <Col md={5} style={{display: 'flex', flexDirection: 'column', justifyContent: 'center'}}>
                        <h3>Администратор</h3>
                        <p>Ведите администрирование системы, выполняйте работу с пользовательскими данными и правами доступа, выполняйте расчёт активности пользователей в системе и подтвержение лицензий поставщиков продуктов питания</p>
                    </Col>
                </Row>
            </Container>
            <Container style={{padding: '0 0 50px 0'}}>
                <h2 style={{textAlign: 'center', marginBottom: '20px'}}>Функционал</h2>
                <Row>
                    <Col className="my-3">
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                            <Card.Img style={{height: '170px'}} variant="top" src="images/data.jpg"/>
                            <Card.Body>
                                <Card.Title>Данные</Card.Title>
                                <Card.Text>Выполняйте работу с данными пользователей: блокирование пользователей, редактирование, работа с правами доступа</Card.Text>
                                <Button href='/admin/user-data'>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col className="my-3">
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                        <Card.Img style={{height: '170px'}} variant="top" src="images/activity.jpg"/>
                            <Card.Body>
                                <Card.Title>Активность</Card.Title>
                                <Card.Text>Просматривайте активность пользователей на сайте и выявляйте наиболее активных поставщиков продуктов питания</Card.Text>
                                <Button href='/admin/user-activity'>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col className="my-3">
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                            <Card.Img style={{height: '170px'}} variant="top" src="images/licenses.jpg"/>
                            <Card.Body>
                                <Card.Title>Лицензии</Card.Title>
                                <Card.Text>Подтвержайте отправленные поставщиками лицензии на ведение коммерческой деятельности</Card.Text>
                                <Button href='/admin/licenses'>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </Container>
        </>
    );
};

export default AdminHome;