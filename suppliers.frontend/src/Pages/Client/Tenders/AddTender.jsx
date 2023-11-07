import React, { useState } from "react";
import { Container } from "react-bootstrap";
import Header from "../../../Components/Header";
import { Form, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const AddTender = () => {

    let router = useNavigate();

    const [newTender, setNewTender] = useState({ title: '', description: '', beginning: '', ending: ''});
    const [fetchError, setFetchError] = useState(false);

    async function addTender(tender) {
        const options = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(tender)
        }
        var result = await fetch(`https://localhost:7214/api/tenders`, options);
        if(result.ok) {
            router('/client/tenders');
        }
        else {
            console.log(result.status);
            setFetchError(true);
        }
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        const tender = {
            title: newTender.title,
            description: newTender.description,
            beginning: newTender.beginning,
            ending: newTender.ending
        };
        addTender(tender);
    };

    return (
        <>
            <Header role='Client'/>
            <Container className="content-container">
                <h3>Публикация тендера</h3>
                <Form className="py-3" onSubmit={handleSubmit}>
                    <Form.Group className="mb-3">
                        <Form.Label>Заголовок тендера</Form.Label>
                        <Form.Control type="text" placeholder="Введите заголовок тендера" required value={newTender.title}
                            onChange={(e) => setNewTender({...newTender, title: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Описание тендера</Form.Label>
                        <Form.Control type="text" as="textarea" rows={3} placeholder="Введите описание тендера" required
                        value={newTender.description} onChange={(e) => setNewTender({...newTender, description: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Дата начала тендера</Form.Label>
                        <Form.Control type="date" min={0} placeholder="Введите дату начала тендера" required
                        value={newTender.beginning} onChange={(e) => setNewTender({...newTender, beginning: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Дата окончания тендера</Form.Label>
                        <Form.Control type="date" placeholder="Введите дату окончания тендера" required value={newTender.ending}
                            onChange={(e) => setNewTender({...newTender, ending: e.target.value})}/>
                    </Form.Group>
                    {fetchError && <span className="d-block text-danger">Ошибка! Не удалось сохранить информацию</span>}
                    <Button variant="success" className="my-3" type="submit">
                        Опубликовать
                    </Button>
                </Form>
            </Container>
        </>
    );
};

export default AddTender;