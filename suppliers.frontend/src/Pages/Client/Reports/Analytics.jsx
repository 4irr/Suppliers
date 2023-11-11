import React from "react";
import Header from "../../../Components/Header";
import { Container, Button, Card, Row, Col } from "react-bootstrap";

const Analytics = () => {
    return (
        <>
            <Header role='Client'/>
            <Container className='content-container'>
                <Row className="justify-content-md-center">
                    <Col className="my-3" md='5'>
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                            <Card.Img style={{height: '170px'}} variant="top" src="../images/client_analytics_single.jpg"/>
                            <Card.Body>
                                <Card.Title>Отчёт о торговле с поставщиком</Card.Title>
                                <Card.Text>Данный отчёт представляет вид отчёта о торговле с конкретным поставщиком за определённый период времени</Card.Text>
                                <Button href='/client/reports/single'>Сформировать</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col className="my-3" md='5'>
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                            <Card.Img style={{height: '170px'}} variant="top" src="../images/client_analytics_multiple.jpg"/>
                            <Card.Body>
                                <Card.Title>Суммарный отчёт о торговле</Card.Title>
                                <Card.Text>Суммарный отчёт о торговле со всеми поставщиками продуктов питания за выбранный период времени</Card.Text>
                                <Button href='/client/reports/total'>Сформировать</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </Container>
        </>
    );
};

export default Analytics;