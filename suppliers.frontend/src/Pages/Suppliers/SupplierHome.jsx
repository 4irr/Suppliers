import React from "react";
import { Container, Row, Col, Card, Button } from "react-bootstrap";
import Header from "../../Components/Header";

const SupplierHome = () => {
    return (
        <div>
            <Header role='Supplier'/>
            <Container className='content-container'>
                <h2 style={{textAlign: 'center', marginBottom: '30px'}}>Добро пожаловать</h2>
                <Row style={{borderBottom: '2px solid black', paddingBottom: '60px'}}>
                    <Col md={7}>
                        <img style={{width: '100%'}} src="images/supplier_home_supplier.jpg" alt='supplier.jpg'/>
                    </Col>
                    <Col md={5} style={{display: 'flex', flexDirection: 'column', justifyContent: 'center'}}>
                        <h3>Поставщик</h3>
                        <p>Регистрируйтесь в системе в качестве поставщика продуктов питания, ведите учёт предлагаемых товаров, отслеживайте список поступивших заказов партий товаров, формируйте отчётность по торговле, оформляйте заявки на участие в тендерах и предлагайте собственные условия поставки</p>
                    </Col>
                </Row>
            </Container>
            <Container style={{padding: '0 0 50px 0'}}>
                <h2 style={{textAlign: 'center', marginBottom: '20px'}}>Функционал</h2>
                <Row>
                    <Col className="my-3">
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                            <Card.Img style={{height: '170px'}} variant="top" src="images/supplier_home_food.jpg"/>
                            <Card.Body>
                                <Card.Title>Товары</Card.Title>
                                <Card.Text>Ведите учёт предлагаемых вами видов товаров прямо в системе, выполняйте обновление, фильтрацию и поиск товаров</Card.Text>
                                <Button href='/supplier/products'>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col className="my-3">
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                        <Card.Img style={{height: '170px'}} variant="top" src="images/client_home_orders.jpg"/>
                            <Card.Body>
                                <Card.Title>Заказы</Card.Title>
                                <Card.Text>Просматривайте заказы партий товаров продуктов питания в онлайн режиме и заключайте договора с заказчиком</Card.Text>
                                <Button href='/supplier/orders'>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                    <Col className="my-3">
                        <Card style={{width: '18rem', margin: '0 auto'}}>
                            <Card.Img style={{height: '170px'}} variant="top" src="images/analytics.jpg"/>
                            <Card.Body>
                                <Card.Title>Аналитика</Card.Title>
                                <Card.Text>Ведите аналитику вашей торговли с заказчиком, формируйте необходимые разнообразные виды отчётов</Card.Text>
                                <Button href='/supplier/analytics'>Перейти</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                </Row>
            </Container>
        </div>
    );
};

export default SupplierHome;