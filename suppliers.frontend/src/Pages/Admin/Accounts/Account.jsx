import { Alert, Button } from "react-bootstrap";
import React from "react";
import { useNavigate } from "react-router-dom";

const Account = ({item, suppliers, setSuppliers}) => {

    const router = useNavigate();

    async function blockUser() {
        const options = {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        };
        const result = await fetch(`https://localhost:7214/api/users/${item.id}/block`, options);
        if(result.ok) {
            setSuppliers(suppliers.map(supplier => {
                supplier.id === item.id && (supplier.isEnabled = false);
                return supplier;
            }));
        }
        else {
            console.log(result.status);
        }
    }

    async function unlockUser() {
        const options = {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        };
        const result = await fetch(`https://localhost:7214/api/users/${item.id}/unlock`, options);
        if(result.ok) {
            setSuppliers(suppliers.map(supplier => {
                supplier.id === item.id && (supplier.isEnabled = true);
                return supplier;
            }));
        }
        else {
            console.log(result.status);
        }
    }

    const handleBlock = () => {
        blockUser();
    }

    const handleUnlock = () => {
        unlockUser();
    }

    const handleEdit = () => {
        router('/admin/user-data/edit', { state: { item: item }});
    }

    return (
        <Alert variant={item.isEnabled ? 'primary' : 'danger'} className="my-4">
            {!item.isEnabled && <h4 className="text-danger">Пользователь заблокирован</h4>}
            <p><b>Имя: </b>{item.firstName}</p>
            <p><b>Фамилия: </b>{item.lastName}</p>
            <p><b>Возраст: </b>{item.age}</p>
            <p><b>Email: </b>{item.email}</p>
            <p><b>Организация: </b>{item.organization}</p>
            <p><b>Лицензия: </b>{item.isLicenseLoaded ? 'загружена' : 'не загружена' }</p>
            {item.isLicensed
            ?
                <p>
                    <b>Статус лицензии: </b>
                    <span className="text-success">лицензированный поставщик</span>
                </p> 
            :
                <p>
                    <b>Статус лицензии: </b>
                    <span className="text-danger">лицензия не подтверждена</span>
                </p> 
            }
            <p><b>Статус Email: </b>{item.emailConfirmed ? 'почта подтверждена' : 'почта не подтверждена' }</p>
            <p><b>Регистрация: </b>{item.isRegisterConfirmed ? 'подтверждена' : 'не подтверждена' }</p>
            <div>
                <Button variant="warning" onClick={handleEdit}>Изменить</Button>
                {item.isEnabled
                ?
                    <Button variant="danger" className="mx-3" onClick={handleBlock}>Заблокировать</Button>
                :
                    <Button variant="success" className="mx-3" onClick={handleUnlock}>Разблокировать</Button>
                }
            </div>
        </Alert>
    );
};

export default Account;