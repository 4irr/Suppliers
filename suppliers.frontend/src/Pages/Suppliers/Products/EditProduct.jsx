import React, { useState, useEffect } from "react";
import { Container, Form, Button } from "react-bootstrap";
import Header from "../../../Components/Header";
import { useNavigate, useParams } from "react-router-dom";

const EditProduct = () => {

    const router = useNavigate();

    const params = useParams();

    const [product, setProduct] = useState({});

    async function getProduct() {
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/Products/${params.id}`, options);
        if(result.ok){
            const info = await result.json();
            setProduct(info);
            return info;
        }
        router('/supplier/products');
        return {};
    }

    async function editProduct(product) {
        const options = {
            method: 'PUT',
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

    useEffect(() => {
        getProduct();
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();
        const newProduct = {
            id: params.id,
            name: product.name,
            price: product.price,
            quantity: product.quantity,
            expirationDate: product.expirationDate
        };
        editProduct(newProduct);
    };

    return (
        <>
            <Header role={'Supplier'}/>
            <Container className="content-container">
            <h3>Добавление товара</h3>
                <Form className="py-3" onSubmit={handleSubmit}>
                    <Form.Group className="mb-3">
                        <Form.Label>Наименование товара</Form.Label>
                        <Form.Control type="text" placeholder="Введите наименование товара" required value={product.name}
                            onChange={(e) => setProduct({...product, name: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Стоимость (рублей) за кг. товара</Form.Label>
                        <Form.Control type="number" min={0} placeholder="Введите стоимость товара" required step={0.1}
                        value={product.price} onChange={(e) => setProduct({...product, price: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Количество товара (кг.)</Form.Label>
                        <Form.Control type="number" min={0} placeholder="Введите количество товара" required step={0.1}
                        value={product.quantity} onChange={(e) => setProduct({...product, quantity: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Срок годности товара</Form.Label>
                        <Form.Control type="date" placeholder="Введите срок годности товара" required value={product.expirationDate}
                            onChange={(e) => setProduct({...product, expirationDate: e.target.value})}/>
                    </Form.Group>
                    <Button variant="success" className="my-3" type="submit">
                        Сохранить
                    </Button>
                </Form>
            </Container>
        </>
    );
};

export default EditProduct;