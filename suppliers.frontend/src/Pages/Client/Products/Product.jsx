import React from "react";
import { Alert, Button } from "react-bootstrap";

const Product = ({item, products, setProducts}) => {
    return(
        <Alert variant="primary" className="product">
            <p><b>Название товара:</b> {item.name}</p>
            <p><b>Цена за кг. товара:</b> {item.price} р.</p>
            <p><b>Количество:</b> {item.quantity} кг.</p>
            <p><b>Товар годен до:</b> {item.expirationDate}</p>
            <Button variant="success">Оформить заказ</Button>
        </Alert>
    );  
};

export default Product;