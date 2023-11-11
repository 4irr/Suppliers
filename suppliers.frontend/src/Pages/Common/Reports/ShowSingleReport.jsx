import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { useLocation } from "react-router-dom";
import { Container } from "react-bootstrap";

const ShowSingleReport = ({role}) => {

    const [report, setReport] = useState(null);
    const location = useLocation();

    useEffect(() => {
        setReport(location.state.item);
    }, []);

    return (
        <>
            <Header role={role}/>
            <Container className='content-container'>
                <h3>Отчёт о торговле за период с {report?.beginning} по {report?.ending}</h3>
                <div style={{padding: '10px 20px', border: '2px solid black', marginTop: '20px'}}>
                    <h4>Поставщик: {report?.supplier}</h4>
                    <p className="contract-row">Количество заключённых договоров: {report?.contractsCount}</p>
                    <p className="contract-row">Список заказов:</p>
                    {report?.contracts.map(item =>
                        <div className="contract-subrow">
                            <h5 style={{marginTop: '10px'}}>Заказ от: {item.conclusionDate}</h5>
                            <p>Наименование товара: {item.product.name}</p>
                            <p>Цена за кг. товара: {item.product.price} р.</p>
                            <p>Количество кг. товара в заказе: {item.quantity} кг.</p>
                            <p>Суммарная стоимость заказа: {item.orderPrice} р.</p>
                        </div>
                    )}
                    <p className="contract-row"><b>Итого суммарная стоимость заказов составила:</b> {report?.totalCost} р.</p>
                </div>
            </Container>
        </>
    );
};

export default ShowSingleReport;