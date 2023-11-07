import React, { useEffect, useState } from "react";
import { Alert, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { loadUser } from "../../../auth/user-service";

const Tender = ({item, role, tenders, setTenders}) => {

    const router = useNavigate();
    const [userId, setUserId] = useState('');

    async function removeTender() {
        const options = {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/tenders/${item.id}/remove`, options);
        if(result.ok) {
            setTenders(tenders.filter(tender => tender.id !== item.id));
        }
    }

    const handleRegister = () => {
        router(`/supplier/tenders/${item.id}/register`, { state: { item: item }});
    };

    const handleRemove = () => {
        removeTender();
    };

    const handleSuppliersListRedirect = () => {
        router(`/client/tenders/${item.id}/suppliers-list`, { state: { tender: item }})
    };

    useEffect(() => {
        loadUser().then(value => {
            if(value!==null)
                setUserId(value.profile.sub);
        });
    }, []);

    return (
        <Alert variant={item.isOpen ? 'info' : 'danger'} className="product">
            <h3 style={{marginBottom: '30px'}}>{item.title}</h3>
            <p><b>Описание тендера:</b> {item.description}</p>
            <p><b>Дата начала тендера:</b> {item.beginning}</p>
            <p><b>Дата окончания тендера:</b> {item.ending}</p>
            <p><b>Статус:</b> {item.isOpen ? 'открыт' : 'закрыт'}</p>
            { role === 'Client' && 
            <div>
                <Button variant="outline-dark" href={`/client/tenders/edit/${item.id}`}>Редактировать</Button>
                <Button variant="outline-dark" style={{marginLeft: '20px'}} onClick={() => handleSuppliersListRedirect()}>Список участников</Button>
            </div> }
            { role === 'Supplier' &&
            ( item.isOpen
            ?
                ( item.tenderUsers.filter(user => user.userId === userId).length !== 0
                ?
                    <span className="text-success">Спасибо за участие в тендере!<br></br>Ваша заявка зафиксирована</span>
                :
                    <Button variant="outline-dark" onClick={() => handleRegister()}>Принять участие</Button>
                )
            :
                ( item.executorId === userId
                ?
                    <span className="text-success">Поздравляем!<br></br>Вы были выбраны исполнителем тендера</span>
                :
                    <span className="text-danger">Вы не были выбраны исполнителем тендера</span>
                )
            )
            }
            { role === 'Admin' && <Button variant="outline-danger" onClick={() => handleRemove()}>Удалить</Button> }
        </Alert>
    );
};

export default Tender;