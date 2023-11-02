import React from "react";
import { Carousel } from "react-bootstrap";

const Slider = () => {
    return (
        <>
            <Carousel>
                <Carousel.Item style={{width: '100%', background: '#000'}}>
                    <img className="w-100" style={{opacity: '0.6', minHeight: '150px'}} src="images/food.jpg" alt='food.jpg'/>
                    <Carousel.Caption>
                        <h3>Продукты питания</h3>
                        <p>Возможность регистрации в системе для поставщиков продуктов питания и ведение учёта предлагаемых товаров</p>
                    </Carousel.Caption>
                </Carousel.Item>
                <Carousel.Item style={{width: '100%', background: '#000'}}>
                    <img className="d-block w-100" style={{opacity: '0.6', minHeight: '150px'}} src="images/warehouse.jpg" alt='warehouse.jpg'/>
                    <Carousel.Caption>
                        <h3>Заказы</h3>
                        <p>Возможность формирования заказов партий продуктов питания онлайн</p>
                    </Carousel.Caption>
                </Carousel.Item>
                <Carousel.Item style={{width: '100%', background: '#000'}}>
                    <img className="d-block w-100" style={{opacity: '0.6', minHeight: '150px'}} src="images/analytics.jpg" alt='analytics.jpg'/>
                    <Carousel.Caption>
                        <h3>Аналитика</h3>
                        <p>Ведение аналитики и отчётности по торговле для заказчика и поставщиков</p>
                    </Carousel.Caption>
                </Carousel.Item>
            </Carousel>
        </>
    );
}

export default Slider;