import React, { useEffect, useState } from "react";
import { Alert, Container } from "react-bootstrap";

const Contract = ({item}) => {

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

    useEffect(() => {
        getSupplier();
    }, []);

    return (
        <>
            {!isContentLoading &&
            <Alert variant={(item.isConfirmed) ? 'success' : 'warning'}>
                <h4 style={{marginBottom: '30px'}}>Id договора: {item.id}</h4>
                <p><b>Дата заключения договора:</b> {item.conclusionDate}</p>
                <p><b>Наименование товара: </b> {item.order.batch.product.name}</p>
                <p><b>Количество товара в партии: </b> {item.order.batch.quantity} кг.</p>
                <p><b>Поставщик:</b> {supplier.firstName + ' ' + supplier.lastName}</p>
                <p><b>Организация:</b> {supplier.organization}</p>
                <p><b>Стоимость заказа:</b> {item.order.orderPrice} р.</p>
                <p><b>Статус:</b> {(item.isConfirmed) ? 'Договор подтверджён поставщиком' : 'Договор не подтверждён поставщиком'}</p>
            </Alert>
            }
        </>
    );
}

export default Contract;