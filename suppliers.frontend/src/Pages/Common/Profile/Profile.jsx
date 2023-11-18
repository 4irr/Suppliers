import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Container, Row, Col, Button, Alert } from "react-bootstrap";
import { loadUser } from "../../../auth/user-service";
import Loader from "../../../Components/Loader/Loader";
import { useNavigate } from "react-router-dom";

const Profile = () => {

    const [user, setUser] = useState(null);
    const [isContentLoading, setIsContentLoading] = useState(true);
    const router = useNavigate();

    async function getUser() {
        setIsContentLoading(true);
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        var userId = (await loadUser()).profile.sub;
        const result = await fetch(`https://localhost:7214/api/users/${userId}`, options);
        if(result.ok){
            const info = await result.json();
            setUser(info);
        }
        setIsContentLoading(false);
    }

    const handleChangePassword = () => {
        router('/user/profile/password', { state: { item: user }});
    };

    const handleEdit = () => {
        router('/user/profile/edit', { state: { item: user }});
    };

    useEffect(() => {
        getUser();
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
                <Row style={{height: '100%'}}>
                    <Col className='my-4' md={3} style={{borderRight: '1px solid black', borderBottom: '1px solid black'}}>
                        {user.role === 'Supplier' && <h3 style={{textAlign: 'center'}}>Поставщик</h3>}
                        {user.role === 'Admin' && <h3 style={{textAlign: 'center'}}>Администратор</h3>}
                        {user.role === 'Client' && <h3 style={{textAlign: 'center'}}>Заказчик</h3>}
                        <div className="my-5">
                            <Button variant="outline-primary" className="w-100" onClick={handleEdit}>Редактировать личную информацию</Button>
                            <Button variant="outline-primary" className="w-100 my-3" onClick={handleChangePassword}>Изменить пароль</Button>
                            {user.role === 'Supplier' && <Button variant="outline-primary" className="w-100" href='/supplier/save-license'>Загрузить лицензию</Button>}
                        </div>
                        {user.role === 'Supplier' && <p><b>Статус: </b> 
                            {user.isLicensed
                            ?
                            <span className="text-success">Лицензированный поставщик</span>
                            :
                            <span className="text-warning">Лицензия не подтверждена</span>
                            }
                        </p>}
                    </Col>
                    <Col md={9} className="text-center">
                        <h2 className="text-center">Данные профиля</h2>
                        <div style={{display: 'inline-block', textAlign: 'left', marginTop: '40px'}}>
                            <Alert className="p-4">
                                <p className="user-info-item"><i className='bx bx-user-circle mx-1'></i><b>Имя пользователя:</b>{user.firstName}</p>
                                <p className="user-info-item"><i className='bx bx-user-circle mx-1'></i><b>Фамилия пользователя:</b>{user.lastName}</p>
                                <p className="user-info-item"><i className='bx bx-user-circle mx-1'></i><b>Возраст:</b>{user.age}</p>
                                <p className="user-info-item"><i className='bx bx-envelope mx-1'></i><b>Email:</b>{user.email}</p>
                                {user.role === 'Supplier' && <p className="user-info-item"><i className='bx bx-briefcase mx-1'></i><b>Организация:</b>{user.organization}</p>}
                            </Alert>
                        </div>
                    </Col>
                </Row>
            </Container>
            }
        </>
    );
};

export default Profile;