import React, { useState } from "react";
import { Container } from "react-bootstrap";
import Header from "../../../Components/Header";
import { Form, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const AddProduct = () => {

    let router = useNavigate();

    const [newProduct, setNewProduct] = useState({ name: '', price: 0, quantity: 0, expirationDate: ''});

    async function addProduct(product) {
        const options = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(product)
        }
        var result = await fetch(`https://localhost:7214/api/Products`, options);
        if(result.ok) {
            router('/');
        }
        else {
            console.log(result.status);
        }
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        const product = {
            name: newProduct.name,
            price: newProduct.price,
            quantity: newProduct.quantity,
            expirationDate: newProduct.expirationDate
        };
        addProduct(product);
    };

    return (
        <>
            <Header role='Supplier'/>
            <Container className="content-container">
                <h3>Добавление товара</h3>
                <Form className="py-3" onSubmit={handleSubmit}>
                    <Form.Group className="mb-3">
                        <Form.Label>Наименование товара</Form.Label>
                        <Form.Control type="text" placeholder="Введите наименование товара" required value={newProduct.name}
                            onChange={(e) => setNewProduct({...newProduct, name: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Стоимость (рублей) за кг. товара</Form.Label>
                        <Form.Control type="number" min={0} placeholder="Введите стоимость товара" required step={0.1}
                        value={newProduct.price} onChange={(e) => setNewProduct({...newProduct, price: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Количество товара (кг.)</Form.Label>
                        <Form.Control type="number" min={0} placeholder="Введите количество товара" required step={0.1}
                        value={newProduct.quantity} onChange={(e) => setNewProduct({...newProduct, quantity: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Срок годности товара</Form.Label>
                        <Form.Control type="date" placeholder="Введите срок годности товара" required value={newProduct.expirationDate}
                            onChange={(e) => setNewProduct({...newProduct, expirationDate: e.target.value})}/>
                    </Form.Group>
                    <Button variant="success" className="my-3" type="submit">
                        Добавить
                    </Button>
                </Form>
            </Container>
        </>
    );
};

export default AddProduct;