import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Container, Form, Button } from "react-bootstrap";
import { useLocation, useNavigate } from "react-router-dom";
import Loader from "../../../Components/Loader/Loader";

const EditInfo = () => {

    const location = useLocation();
    const [user, setUser] = useState({firstName: '', lastName: '', age: '', organization: ''});
    const [isContentLoading, setIsContentLoading] = useState(false);
    const [isFailed, setIsFailed] = useState(false);
    const router = useNavigate();

    async function editUser(user) {
        setIsContentLoading(true);
        const options = {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(user)
        }
        var result = await fetch(`https://localhost:7214/api/users`, options);
        if(result.ok) {
            setIsFailed(false);
            router('/user/profile');
        }
        else {
            setIsFailed(true);
            console.log(result.status);
        }
        setIsContentLoading(false);
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        var newUser = {
            id: user.id,
            firstName: user.firstName,
            lastName: user.lastName,
            age: user.age,
            organization: user.organization
        }
        editUser(newUser);
    };

    useEffect(() => {
        setUser(location.state.item);
    }, []);

    return (
        <>
            <Header role={user?.role}/>
            {isContentLoading
            ?
            <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
                <Loader/>
            </Container>
            :
            <Container className="content-container">
                <h3>Редактирование личной информации</h3>
                <Form className="py-3" onSubmit={handleSubmit}>
                    <Form.Group className="mb-3">
                        <Form.Label>Имя</Form.Label>
                        <Form.Control type="text" placeholder="Введите новое значение" required value={user?.firstName}
                            onChange={(e) => setUser({...user, firstName: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Фамилия</Form.Label>
                        <Form.Control type="text" placeholder="Введите новое значение" required value={user?.lastName}
                            onChange={(e) => setUser({...user, lastName: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Возраст</Form.Label>
                        <Form.Control type="number" min={0} max={120} placeholder="Введите новое значение" required value={user?.age}
                            onChange={(e) => setUser({...user, age: e.target.value})}/>
                    </Form.Group>
                    {user?.role === 'Supplier' && 
                    <Form.Group className="mb-3">
                        <Form.Label>Организация</Form.Label>
                        <Form.Control type="text" placeholder="Введите новое значение" required value={user?.organization}
                            onChange={(e) => setUser({...user, organization: e.target.value})}/>
                    </Form.Group>}
                    {isFailed && <span className='d-block text-danger'>Не удалось выполнить редактирование личных данных</span>}
                    <Button variant="success" className="my-3" type="submit">
                        Сохранить
                    </Button>
                </Form>
            </Container>
            }
        </>
    );
}

export default EditInfo;