import React, { useEffect, useState } from "react";
import { Alert, Button } from "react-bootstrap";

const Contract = ({item, contracts, setContracts}) => {

    const [supplier, setSupplier] = useState({});

    async function getSupplier() {
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/users/${item.order.supplierId}`, options);
        if(result.ok){
            const info = await result.json();
            setSupplier(info);
        }
    }

    async function submitContract() {
        const options = {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/contracts/${item.id}/confirm`, options);
        if(result.ok){
            setContracts(contracts.map(contract => contract.id === item.id ? {...contract, isConfirmed: true} : contract));
        }
    }

    useEffect(() => {
        getSupplier();
    }, []);

    const handleSubmit = () => {
        submitContract();
    };

    return (
        <Alert variant={(item.isConfirmed) ? 'success' : 'warning'}>
            <h4 style={{marginBottom: '30px'}}>Id договора: {item.id}</h4>
            <p><b>Дата заключения договора:</b> {item.conclusionDate}</p>
            <p><b>Наименование товара: </b> {item.order.batch.product.name}</p>
            <p><b>Количество товара в партии: </b> {item.order.batch.quantity} кг.</p>
            <p><b>Поставщик:</b> {supplier.firstName + ' ' + supplier.lastName}</p>
            <p><b>Организация:</b> {supplier.organization}</p>
            <p><b>Стоимость заказа:</b> {item.order.orderPrice} р.</p>
            <p><b>Статус:</b> {(item.isConfirmed) ? 'Договор подтверджён' : 'Договор в ожидании подтверждения'}</p>
            {!item.isConfirmed && <Button onClick={() => handleSubmit()} style={{marginTop: '10px'}}>Подтвердить</Button>}
        </Alert>
    );
}

export default Contract;