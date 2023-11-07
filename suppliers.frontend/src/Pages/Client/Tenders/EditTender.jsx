import React, { useEffect, useState } from "react";
import { Container } from "react-bootstrap";
import Header from "../../../Components/Header";
import { Form, Button } from "react-bootstrap";
import { useLocation, useNavigate, useParams } from "react-router-dom";

const AddTender = () => {

    let router = useNavigate();

    const [tender, setTender] = useState({title: '', description: '', beginning: '', ending: ''});
    const [fetchError, setFetchError] = useState(false);
    const params = useParams();

    async function getTender() {
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/tenders/${params.id}`, options);
        if(result.ok){
            const info = await result.json();
            setTender(info);
            return info;
        }
        router('/client/tenders');
        return {};
    }

    async function editTender(tender) {
        const options = {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(tender)
        }
        var result = await fetch(`https://localhost:7214/api/tenders/${tender.id}/edit`, options);
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
        editTender(tender);
    };

    useEffect(() => {
        getTender();
    }, []);

    return (
        <>
            <Header role='Client'/>
            <Container className="content-container">
                <h3>Редактирование тендера</h3>
                <Form className="py-3" onSubmit={handleSubmit}>
                    <Form.Group className="mb-3">
                        <Form.Label>Заголовок тендера</Form.Label>
                        <Form.Control type="text" placeholder="Введите заголовок тендера" required value={tender.title}
                            onChange={(e) => setTender({...tender, title: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Описание тендера</Form.Label>
                        <Form.Control type="text" as="textarea" rows={3} placeholder="Введите описание тендера" required
                        value={tender.description} onChange={(e) => setTender({...tender, description: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Дата начала тендера</Form.Label>
                        <Form.Control type="date" min={0} placeholder="Введите дату начала тендера" required
                        value={tender.beginning} onChange={(e) => setTender({...tender, beginning: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Дата окончания тендера</Form.Label>
                        <Form.Control type="date" placeholder="Введите дату окончания тендера" required value={tender.ending}
                            onChange={(e) => setTender({...tender, ending: e.target.value})}/>
                    </Form.Group>
                    {fetchError && <span className="d-block text-danger">Ошибка! Не удалось сохранить информацию</span>}
                    <Button variant="success" className="my-3" type="submit">
                        Сохранить
                    </Button>
                </Form>
            </Container>
        </>
    );
};

export default AddTender;