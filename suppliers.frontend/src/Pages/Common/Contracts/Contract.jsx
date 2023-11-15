import React, { useEffect, useState } from "react";
import { Alert, Button } from "react-bootstrap";

const Contract = ({item, role, contracts, setContracts, setSortedFilteredContracts}) => {

    const [supplier, setSupplier] = useState({});
    const [isContentLoading, setIsContentLoading] = useState(true);

    async function getSupplier() {
        setIsContentLoading(true);
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
        setIsContentLoading(false);
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

    async function submitContract() {
        const options = {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/contracts/${item.id}/confirm`, options);
        if(result.ok){
            var newContracts = contracts.map(contract => contract.id === item.id ? {...contract, isConfirmed: true} : contract);
            setContracts(newContracts);
            setSortedFilteredContracts(newContracts);
        }
    }

    useEffect(() => {
        getSupplier();
    }, []);

    const handleRemove = async () => {
        await deleteContract();
        await deleteOrder();
        await deleteBatch();
        var newContracts = contracts.filter(contract => contract.id !== item.id);
        setContracts(newContracts);
        setSortedFilteredContracts(newContracts);
    };
    
    const dateOptions = {
        day: 'numeric',
        month: 'long',
        year: 'numeric'
    }

    const handleSubmit = () => {
        submitContract();
    };

    useEffect(() => {
        getSupplier();
    }, []);

    return (
        <>
            {!isContentLoading &&
            <Alert variant={(item.isConfirmed) ? 'success' : 'warning'} className="my-4">
                <h4 style={{marginBottom: '30px'}}>Id договора: {item.id}</h4>
                <p><b>Дата заключения договора:</b> {new Date(item.conclusionDate).toLocaleDateString('ru-RU', dateOptions)}</p>
                <p><b>Наименование товара: </b> {item.order.batch.product.name}</p>
                <p><b>Количество товара в партии: </b> {item.order.batch.quantity} кг.</p>
                <p><b>Поставщик:</b> {supplier.firstName + ' ' + supplier.lastName}</p>
                <p><b>Организация:</b> {supplier.organization}</p>
                <p><b>Стоимость заказа:</b> {item.order.orderPrice} р.</p>
                <p><b>Статус:</b> {(item.isConfirmed) ? 'Договор подтверджён поставщиком' : 'Договор не подтверждён поставщиком'}</p>
                {role === 'Admin' && <Button variant="danger" onClick={() => handleRemove()} style={{marginTop: '10px'}}>Удалить</Button>}
                {(role === 'Supplier' && !item.isConfirmed) && <Button onClick={() => handleSubmit()} style={{marginTop: '10px'}}>Подтвердить</Button>}
            </Alert>
            }
        </>
    );
}

export default Contract;