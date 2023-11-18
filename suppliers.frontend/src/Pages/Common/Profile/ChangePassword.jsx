import React, { useEffect, useState } from "react";
import { Container, Form, Button } from "react-bootstrap";
import Header from "../../../Components/Header";
import { useLocation, useNavigate } from "react-router-dom";

const ChangePassword = () => {

    const location = useLocation();
    const [user, setUser] = useState();
    const [isContentLoading, setIsContentLoading] = useState(false);
    const [fail, setFail] = useState({ isFailed: '', message: ''});
    const [passwordDto, setPasswordDto] = useState({id: '', oldPassword: '', newPassword: ''});
    const router = useNavigate();

    async function changePassword() {
        setIsContentLoading(true);
        const options = {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(passwordDto)
        }
        var result = await fetch(`https://localhost:7214/api/users/password`, options);
        if(result.ok) {
            setFail({...fail, isFailed: false});
            router('/user/profile');
        }
        else {
            setFail({isFailed: true, message: (await result.json()).error});
            console.log(result.status);
        }
        setIsContentLoading(false);
    }

    useEffect(() => {
        setUser(location.state.item);
        setPasswordDto({...passwordDto, id: location.state.item.id});
    }, []);

    const handleSubmit = (e) => {
        e.preventDefault();
        changePassword();
    };

    return (
        <>
            <Header role={user?.role}/>
            <Container className="content-container">
                <h3>Изменение пароля</h3>
                <Form className="py-3" onSubmit={handleSubmit}>
                    <Form.Group className="mb-3">
                        <Form.Label>Старый пароль</Form.Label>
                        <Form.Control type="password" placeholder="Введите старый пароль" required value={passwordDto.oldPassword}
                            onChange={(e) => setPasswordDto({...passwordDto, oldPassword: e.target.value})}/>
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Новый пароль</Form.Label>
                        <Form.Control type="password" placeholder="Введите новый пароль" required value={passwordDto.newPassword}
                            onChange={(e) => setPasswordDto({...passwordDto, newPassword: e.target.value})}/>
                    </Form.Group>
                    {fail.isFailed && <span className='d-block text-danger'>{fail.message}</span>}
                    <Button variant="success" className="my-3" type="submit">
                        Сохранить
                    </Button>
                </Form>
            </Container>
        </>
    );
};

export default ChangePassword;