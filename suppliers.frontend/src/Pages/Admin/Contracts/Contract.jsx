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

    async function deleteContract() {
        const options = {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/contracts/${item.id}/remove`, options);
        if(!result.ok) {
            console.log(result.status);
        }
    }

    async function deleteOrder() {
        const options = {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/orders/${item.order.id}/remove`, options);
        if(!result.ok) {
            console.log(result.status);
        }
    }

    async function deleteBatch() {
        const options = {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/batches/${item.order.batch.id}/remove`, options);
        if(!result.ok) {
            console.log(result.status);
        }
    }

    useEffect(() => {
        getSupplier();
    }, []);

    const handleSubmit = async () => {
        await deleteContract();
        await deleteOrder();
        await deleteBatch();
        setContracts(contracts.filter(contract => contract.id !== item.id));
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
            <Button variant="danger" onClick={() => handleSubmit()} style={{marginTop: '10px'}}>Удалить</Button>
        </Alert>
    );
}

export default Contract;