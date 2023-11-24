import React, { useState } from "react";
import Header from "../../../Components/Header";
import { Container, Form, Button } from "react-bootstrap";
import { useLocation, useNavigate } from "react-router-dom";

const EditUserData = () => {

    const location = useLocation();
    const router = useNavigate();   

    const [user, setUser] = useState(location.state.item);
    const [isFailed, setIsFailed] = useState(false);

    async function editUserData(user) {
        const options = {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                'Content-type': 'application/json'
            },
            body: JSON.stringify(user)
        };
        const result = await fetch('https://localhost:7214/api/users', options);
        if(result.ok) {
            setIsFailed(false);
            router('/admin/user-data');
        }
        else {
            setIsFailed(true);
            console.log(result.status);
        }
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        const newUser = {
            id: user.id,
            firstName: user.firstName,
            lastName: user.lastName,
            age: user.age,
            organization: user.organization
        }
        editUserData(newUser);
    }

    return (
        <>
            <Header role='Admin'/>
            <Container className="content-container">
                <h2>Редактирование информации о пользователе</h2>
                <Form className="py-3" onSubmit={handleSubmit}>
                    <Form.Group className="mb-3">
                        <Form.Label>Имя</Form.Label>
                        <Form.Control type="text" placeholder="Введите имя пользователя" required value={user.firstName}
                            onChange={(e) => setUser({...user, firstName: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Фамилия</Form.Label>
                        <Form.Control type="text" placeholder="Введите фамилию пользователя" required value={user.lastName}
                            onChange={(e) => setUser({...user, lastName: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Возраст</Form.Label>
                        <Form.Control type="number" min={0} max={120} placeholder="Введите возраст пользователя" required value={user.age}
                            onChange={(e) => setUser({...user, age: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Организация</Form.Label>
                        <Form.Control type="text" placeholder="Введите наименование организации" required value={user.organization}
                            onChange={(e) => setUser({...user, organization: e.target.value})}/>
                    </Form.Group>
                    {isFailed && <span className='d-block text-danger'>Не удалось выполнить редактирование данных пользователя</span>}
                    <Button variant="success" className="my-3" type="submit">
                        Сохранить
                    </Button>
                </Form>
            </Container>
        </>
    );
};

export default EditUserData;