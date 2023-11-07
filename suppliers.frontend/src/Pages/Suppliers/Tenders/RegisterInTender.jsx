import React, { useEffect, useState } from "react";
import { Container, Form, Button } from "react-bootstrap";
import { useLocation, useNavigate } from "react-router-dom";
import Header from "../../../Components/Header";

const RegisterInRender = () => {

    const [tenderUser, setTenderUser] = useState({tenderId: '', userDescription: ''});
    const location = useLocation();
    const router = useNavigate();

    async function registerInTender(tenderUser) {
        const options = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(tenderUser)
        }
        var result = await fetch(`https://localhost:7214/api/tenders/register`, options);
        if(result.ok) {
            router('/supplier/tenders');
        }
        else {
            console.log(result.status);
        }
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        registerInTender(tenderUser);
    };

    useEffect(() => {
        if(location.state === null)
            router('/supplier/tenders');
        setTenderUser({ ...tenderUser, tenderId: location.state?.item.id });
    }, []);

    return (
        <>
            <Header role={'Supplier'}/>
            <Container className="content-container">
                <h3>Регистрация на участие в тендере</h3>
                <Form className="py-3" onSubmit={handleSubmit}>
                    <Form.Group className="mb-3">
                        <Form.Label>Заголовок тендера</Form.Label>
                        <Form.Control type='text' readOnly value={location.state?.item.title}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Описание тендера</Form.Label>
                        <Form.Control type='text' as="textarea" rows={3} readOnly value={location.state?.item.description}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Условия и предложения по поставке товаров</Form.Label>
                        <Form.Control type="text" as="textarea" rows={3} required value={tenderUser.userDescription}
                            onChange={(e) => setTenderUser({...tenderUser, userDescription: e.target.value})}
                            placeholder="Введите здесь ваши условия и предложения"/>
                    </Form.Group>
                    <Button variant="success" className="my-3" type="submit">
                        Принять участие
                    </Button>
                </Form>
            </Container>
        </>
    );
};

export default RegisterInRender;