import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Container, Form, Button } from "react-bootstrap";
import { useLocation, useNavigate, useParams } from "react-router-dom";

const CreateBatch = () => {

    const [batch, setBatch] = useState({quantity: 0});
    const [product, setProduct] = useState({name: ''});
    const [price, setPrice] = useState(0);
    const location = useLocation();
    const router = useNavigate();
    const params = useParams();

    useEffect(() => {
        setProduct(location.state.item);
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();
        const batchToAdd = {
            productId: product.id,
            productName: product.name,
            quantity: batch.quantity
        };
        router(`/client/suppliers/${params.id}/products/${params.productId}/create-batch/confirm`, {state: { batch: batchToAdd, price: price }});
    };

    const handleQuantyityChange = (e) => {
        setBatch({...batch, quantity: e.target.value});
        setPrice(e.target.value * product.price);
    }

    return (
        <>
            <Header role='Client'/>
            <Container className="content-container">
                <h3>Формирование партии товаров</h3>
                <Form className="py-3" onSubmit={handleSubmit}>
                    <Form.Group className="mb-3">
                        <Form.Label>Наименование товара</Form.Label>
                        <Form.Control type="text" required value={product.name} readOnly/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Количество товара (кг.) (max: {product.quantity} кг.)</Form.Label>
                        <Form.Control type="number" min={0} max={product.quantity} placeholder="Введите количество товара" required step={0.1}
                        value={batch.quantity} onChange={(e) => handleQuantyityChange(e)}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Суммарная стоимость заказа (р.)</Form.Label>
                        <Form.Control type="number" readOnly value={price}/>
                    </Form.Group>
                    <Button variant="success" className="my-3" type="submit">
                        Оформить заказ
                    </Button>
                </Form>
            </Container>
        </>
    );
};

export default CreateBatch;