import React from "react";
import { Container, Row, Col, Card, Button } from "react-bootstrap";
import Header from "../../Components/Header";
import { loadUser, signinRedirect } from "../../auth/user-service";
import { useNavigate } from "react-router-dom";
import Slider from "../../Components/Slider";
import { useEffect } from "react";
import { useState } from "react";
import Loader from "../../Components/Loader/Loader";

const Home = () => {

    var router = useNavigate();

    if(localStorage.getItem('isAuthenticated') === 'true' && !sessionStorage.getItem('oidc.user:https://localhost:7073:suppliers-web-api'))
        signinRedirect();

    loadUser().then(value => {
        if(value===null)
            return;
        else {
            switch(value.profile.role) {
                case 'Admin': {
                    router('/admin');
                    break;
                }
                case 'Supplier': {
                    router('/supplier');
                    break;
                }
                case 'Client': {
                    router('/client');
                    break;
                }
                default: {
                    signinRedirect();
                    break;
                }
            }
        }
    });

    return (            
        <div>
            <Header/>
            <Slider/>
            <Container style={{marginTop: '2rem', marginBottom: '2rem'}}>
                <Row className="justifyCenter">
                    <Col className="my-3">
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                            <Card.Img style={{height: '170px'}} variant="top" src="images/customer.jpg"/>
                            <Card.Body>
                                <Card.Title>Заказчик</Card.Title>
                                <Card.Text>Выполняйте поиск поставщиков в качестве заказчика, оформляйте заказы партий товаров в онлайн режиме, заключайте договора с поставщиками, публикуйте тендеры на закупку товаров и ведите собственную аналитику торговли</Card.Text>
                                <Button onClick={() => signinRedirect() }>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col className="my-3">
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                        <Card.Img style={{height: '170px'}} variant="top" src="images/supplier.jpg"/>
                            <Card.Body>
                                <Card.Title>Поставщики</Card.Title>
                                <Card.Text>Регистрируйтесь в системе в качестве поставщика продуктов питания, ведите учёт предлагаемых товаров, отслеживайте список поступивших заказов партий товаров, формируйте отчётность по торговле</Card.Text>
                                <Button onClick={() => signinRedirect() }>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col className="my-3">
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                            <Card.Img style={{height: '170px'}} variant="top" src="images/admin.jpg"/>
                            <Card.Body>
                                <Card.Title>Администрирование</Card.Title>
                                <Card.Text>Ведите администрирование системы, выполняйте работу с пользовательскими данными и правами доступа, выполняйте расчёт активности пользователей в системе и подтвержение лицензий поставщиков продуктов питания</Card.Text>
                                <Button onClick={() => signinRedirect() }>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </Container>
        </div>
    );
};

export default Home;