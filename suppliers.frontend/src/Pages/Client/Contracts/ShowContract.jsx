import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Alert, Container } from "react-bootstrap";
import { useLocation, useParams } from "react-router-dom";

const ShowContract = () => {

    const location = useLocation();
    const params = useParams();

    const [supplier, setSupplier] = useState({});

    async function getSupplier() {
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/users/${params.id}`, options);
        if(result.ok){
            const info = await result.json();
            setSupplier(info);
        }
    }

    useEffect(() => {
        getSupplier();
    }, []);

    return (
        <>
            <Header role={'Client'}/>
            <Container className="content-container" style={{height: '70vh', display: 'flex', justifyContent: 'center', alignItems: 'center', flexDirection: 'column'}}>
                <h2>Договор создан успешно и отправлен поставщику</h2>
                <Alert className="my-4 py-3" variant="primary">
                    <h4 style={{marginBottom: '30px'}}>Id договора: {location.state.contractId}</h4>
                    <p><b>Дата заключения договора:</b> {new Date().toLocaleDateString()}</p>
                    <p><b>Наименование товара: </b> {location.state.batch.productName}</p>
                    <p><b>Количество товара в партии: </b> {location.state.batch.quantity} кг.</p>
                    <p><b>Поставщик:</b> {supplier.firstName + ' ' + supplier.lastName}</p>
                    <p><b>Организация:</b> {supplier.organization}</p>
                    <p><b>Суммарная стоимость заказа:</b> {location.state.price} р.</p>
                </Alert>
            </Container>
        </>
    );
};

export default ShowContract;