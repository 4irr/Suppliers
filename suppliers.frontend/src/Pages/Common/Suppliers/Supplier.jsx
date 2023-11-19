import React, { useState } from "react";
import { Alert, Button } from "react-bootstrap";

const Supplier = ({item, suppliers, setSuppliers, role}) => {

    const [isFailed, setIsFailed] = useState(false);

    let alertVariant;

    if(role === 'Client') {
        alertVariant = item.isLicensed ? 'success' : 'warning';
    }
    else {
        alertVariant = 'primary';
    }

    async function confirmRegister() {
        const options = {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/users/${item.id}/register/confirm`, options);
        if(result.ok){
            setSuppliers(suppliers.filter(supplier => supplier.id !== item.id));
            setIsFailed(false);
        }
        else {
            setIsFailed(true);
            console.log(result.status);
        }
    }

    const handleConfirmRegister = () => {
        confirmRegister();
    };

    return(
        <Alert variant={alertVariant} className="my-4">
            <p><b>Имя:</b> {item.firstName}</p>
            <p><b>Фамилия:</b> {item.lastName}</p>
            <p><b>Организация:</b> {item.organization}</p>
            <p><b>Email:</b> {item.email}</p>
            {role === 'Client'
            ?
            <div>
                <p><b>Статус:</b> {item.isLicensed ? 'лицензированный поставщик' : 'лицензия не подтверждена'}</p>
                <Button href={`/client/suppliers/${item.id}/products`}>Предлагаемые товары</Button>
            </div>
            :
            <div>
                {item.emailConfirmed
                ?
                <span className="d-block text-success">Почта подтверждена</span>
                :
                <span className="d-block text-danger">Почта не подтверждена</span>
                }
                {isFailed && <span className="d-block text-danger" style={{marginTop: '20px'}}>Не удалось подтвердить регистрацию</span>}
                <Button variant="outline-dark" style={{marginTop: '20px'}} onClick={handleConfirmRegister}>Подтвердить регистрацию</Button>
            </div>
            }
        </Alert>
    );
};

export default Supplier;