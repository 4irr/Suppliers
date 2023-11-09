import React, { useState } from "react";
import { Alert, Button } from "react-bootstrap";

const Supplier = ({item, suppliers, setSuppliers}) => {

    const [isResultFailed, setIsResultFailed] = useState(false);

    async function loadLicense() {
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
            }
        }
        const result = await fetch(`https://localhost:7214/api/users/${item.id}/load-license`, options);
        if(result.ok){
            setIsResultFailed(false);
            const blob = await result.blob();
            const downloadUrl = window.URL.createObjectURL(blob);
            const link = document.createElement('a');
            link.href = downloadUrl;
            link.download = 'license';
            document.body.appendChild(link);
            link.click();
            link.remove();
        }
        else {
            setIsResultFailed(true);
            console.log(result.status);
        }
    }

    async function confirmLicense() {
        const options = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
            }
        }
        const result = await fetch(`https://localhost:7214/api/users/${item.id}/confirm-license`, options);
        if(result.ok) {
            setSuppliers(suppliers.filter(supplier => supplier.id !== item.id));
        }
        else {
            console.log(result.status);
        }
    }

    const handleLoadLicense = () => {
        loadLicense();
    };

    const handleConfirmLicense = () => {
        confirmLicense();
    };

    return (
        <Alert variant="info">
            <p><b>Имя:</b> {item.firstName}</p>
            <p><b>Фамилия:</b> {item.lastName}</p>
            <p><b>Организация:</b> {item.organization}</p>
            <p><b>Email:</b> {item.email}</p>
            {isResultFailed && <span className="d-block text-danger" style={{marginBottom: '10px'}}>Не удалось загрузить лицензию поставщика!</span>}
            <Button variant="primary" style={{marginRight: '10px'}} onClick={() => handleLoadLicense()}>Загрузить лицензию</Button>
            <Button variant="success" onClick={() => handleConfirmLicense()}>Подтвердить лицензию</Button>
        </Alert>
    );
};

export default Supplier;