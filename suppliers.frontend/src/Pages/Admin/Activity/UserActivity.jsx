import React, { useState } from "react";
import Header from "../../../Components/Header";
import { Container } from "react-bootstrap";
import { useLocation } from "react-router-dom";

const UserActivity = () => {

    const location = useLocation();
    const [activity, setActivity] = useState(location.state.item);

    return (
        <>
            <Header role='Admin'/>
            <Container className='content-container'>
                <h3>Отчёт об активности поставщика за период с {activity?.beginning} по {activity?.ending}</h3>
                <div style={{padding: '10px 20px', border: '2px solid black', marginTop: '20px'}}>
                    <h4>Поставщик: {activity?.supplier}</h4>
                    <p className="contract-row">Общее количество товаров: {activity?.products.length}</p>
                    <p className="contract-row">Список товаров:</p>
                    {activity?.products.map(item =>
                        <div className="contract-subrow">
                            <h5 style={{marginTop: '10px'}}>Товар: {item.name}</h5>
                            <p>Предоставляемое количество товара: {item.quantity} кг.</p>
                            <p>Цена за кг. товара: {item.price} р.</p>
                            <p>Товар годен до: {new Date(item.expirationDate).toLocaleDateString('ru-RU', { day: 'numeric', month: 'long', year: 'numeric' })}</p>
                        </div>
                    )}
                    <p className="contract-row">Количество заключённых договоров: {activity?.contracts.length}</p>
                    <p className="contract-row">Список заказов:</p>
                    {activity?.contracts.map(item =>
                        <div className="contract-subrow">
                            <h5 style={{marginTop: '10px'}}>Заказ от: {item.conclusionDate}</h5>
                            <p>Наименование товара: {item.product.name}</p>
                            <p>Цена за кг. товара: {item.product.price} р.</p>
                            <p>Количество кг. товара в заказе: {item.quantity} кг.</p>
                            <p>Суммарная стоимость заказа: {item.orderPrice} р.</p>
                        </div>
                    )}
                    <p className="contract-row">Количество участий в тендерах: {activity?.tenders.length}</p>
                    <p className="contract-row">Список тендеров:</p>
                    {activity?.tenders.map(item =>
                        <div className="contract-subrow">
                            <h5 style={{marginTop: '10px'}}>{item.tender.title}</h5>
                            <p>Описание тендера: {item.tender.description}</p>
                        </div>
                    )}
                </div>
            </Container>
        </>
    );
}

export default UserActivity;