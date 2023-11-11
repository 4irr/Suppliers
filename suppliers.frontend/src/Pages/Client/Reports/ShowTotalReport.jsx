import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { useLocation } from "react-router-dom";
import { Container } from "react-bootstrap";

const ShowTotalReport = () => {

    const [report, setReport] = useState(null);
    const location = useLocation();

    useEffect(() => {
        setReport(location.state.item);
    }, []);

    return (
        <>
            <Header role='Client'/>
            <Container className='content-container'>
                <h3>Отчёт за период с {report?.beginning} по {report?.ending}</h3>
                <div style={{padding: '10px 20px', border: '2px solid black', marginTop: '20px'}}>
                    <h3>Отчёт о торговле со всеми контрагентами</h3>
                    <p className="contract-row">Количество контрагентов: {report?.suppliersCount}</p>
                    <p className="contract-row">Список контрагентов:</p>
                    {report?.suppliers.map(item =>
                        <div className="contract-subrow">
                            <h5 style={{marginTop: '10px'}}>Организация: {item.organization}</h5>
                            <p>Количество заказов: {item.contractsCount}</p>
                            <p>Суммарная стоимость заказов: {item.contractsTotalCost} р.</p>
                        </div>
                    )}
                    <p className="contract-row"><b>Итого суммарная стоимость заказов составила:</b> {report?.totalCost} р.</p>
                </div>
            </Container>
        </>
    );
};

export default ShowTotalReport;