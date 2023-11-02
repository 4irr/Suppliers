import React from "react";
import { Alert, Button } from "react-bootstrap";

const Product = ({item, products, setProducts}) => {

    async function removeProduct() {
        const options = {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/Products/${item.id}`, options);
        if(result.ok) {
            setProducts(products.filter(product => product.id !== item.id));
        }
    }

    const handleRemove = () => {
        removeProduct();
    };

    return(
        <Alert variant="primary" className="product">
            <p><b>Название товара:</b> {item.name}</p>
            <p><b>Цена за кг. товара:</b> {item.price} р.</p>
            <p><b>Количество:</b> {item.quantity} кг.</p>
            <p><b>Товар годен до:</b> {item.expirationDate}</p>
            <Button variant="success" style={{marginRight: '20px'}} href={`/supplier/products/edit/${item.id}`}>Изменить</Button>
            <Button variant="danger" onClick={handleRemove}>Удалить</Button>
        </Alert>
    );
};

export default Product;