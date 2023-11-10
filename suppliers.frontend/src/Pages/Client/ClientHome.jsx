import React from "react";
import { Container, Row, Col, Card, Button } from "react-bootstrap";
import Header from "../../Components/Header";

const ClientHome = () => {
    return (
        <div>
            <Header role='Client'/>
            <Container className='content-container'>
                <h2 style={{textAlign: 'center', marginBottom: '30px'}}>Добро пожаловать</h2>
                <h1 style={{fontSize: 30px}}>Олег топ нах</h1>
                <Row style={{borderBottom: '2px solid black', paddingBottom: '60px'}}>
                    <Col md={7}>
                        <img style={{width: '100%'}} src="images/customer.jpg" alt='customer.jpg'/>
                    </Col>
                    <Col md={5} style={{display: 'flex', flexDirection: 'column', justifyContent: 'center'}}>
                        <h3>Заказчик</h3>
                        <p>Выполняйте поиск поставщиков в качестве заказчика, оформляйте заказы партий товаров в онлайн режиме, заключайте договора с поставщиками, публикуйте тендеры на закупку товаров и ведите собственную аналитику торговли</p>
                    </Col>
                </Row>
            </Container>
            <Container style={{padding: '0 0 50px 0'}}>
                <h2 style={{textAlign: 'center', marginBottom: '20px'}}>Функционал</h2>
                <Row className="justify-content-md-center">
                    <Col className="my-3" md='5'>
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                            <Card.Img style={{height: '170px'}} variant="top" src="images/client_home_suppliers.jpg"/>
                            <Card.Body>
                                <Card.Title>Поставщики</Card.Title>
                                <Card.Text>Выполняйте поиск поставщиков и фильтрацию зарегистрированных в системе поставщиков по необходимым критериям</Card.Text>
                                <Button href='/client/suppliers'>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col className="my-3" md='5'>
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                        <Card.Img style={{height: '170px'}} variant="top" src="images/client_home_orders.jpg"/>
                            <Card.Body>
                                <Card.Title>Заказы</Card.Title>
                                <Card.Text>Оформляйте заказы партий товаров продуктов питания в онлайн режиме и заключайте договора с поставщиками</Card.Text>
                                <Button href='/client/orders'>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
                <Row className="justify-content-md-center my-4">
                    <Col className="my-3" md='5'>
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                            <Card.Img style={{height: '170px'}} variant="top" src="images/tender.jpg"/>
                            <Card.Body>
                                <Card.Title>Тендеры</Card.Title>
                                <Card.Text>Публикуйте тендеры для поставщиков на закупку необходимых видов товаров по выгодным условиям</Card.Text>
                                <Button href='/client/tenders'>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col className="my-3" md='5'>
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                            <Card.Img style={{height: '170px'}} variant="top" src="images/analytics.jpg"/>
                            <Card.Body>
                                <Card.Title>Аналитика</Card.Title>
                                <Card.Text>Ведите аналитику вашей торговли с поставщиками, формируйте необходимые разнообразные виды отчётов</Card.Text>
                                <Button href='/client/analytics'>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </Container>
        </div>
    );
};

export default ClientHome;
