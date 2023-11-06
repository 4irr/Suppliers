import React from "react";
import { Alert, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const Product = ({item, supplierId}) => {

    const router = useNavigate();

    function handleSubmit() {
        router(`/client/suppliers/${supplierId}/products/${item.id}/create-batch`, {state: { item: item }});
    }

    return(
        <Alert variant="primary" className="product">
            <p><b>Название товара:</b> {item.name}</p>
            <p><b>Цена за кг. товара:</b> {item.price} р.</p>
            <p><b>Количество:</b> {item.quantity} кг.</p>
            <p><b>Товар годен до:</b> {item.expirationDate}</p>
            <Button variant="success" onClick={() => handleSubmit()}>Оформить заказ</Button>
        </Alert>
    );  
};

export default Product;