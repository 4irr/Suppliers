import { Alert, Button } from "react-bootstrap";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

const TenderSupplier = ({userId, userDescription, tender}) => {

    const [user, setUser] = useState({});
    const [isContentLoading, setIsContentLoading] = useState(true);
    const params = useParams();

    async function getSupplier() {
        setIsContentLoading(true);
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/users/${userId}`, options);
        if(result.ok){
            const info = await result.json();
            setUser(info);
        }
        setIsContentLoading(false);
    }

    useEffect(() => {
        getSupplier();
    }, []);

    return(
        <>
            {!isContentLoading &&
            <Alert className='my-4'>
                <p><b>Имя:</b> {user.firstName}</p>
                <p><b>Фамилия:</b> {user.lastName}</p>
                <p><b>Организация:</b> {user.organization}</p>
                <p><b>Email:</b> {user.email}</p>
                <p style={{textAlign: 'justify'}}><b>Описание отклика на учатие в тендере:</b> {userDescription}</p>
                {tender.isOpen && <Button variant="success" href={`/client/tenders/${params.id}/executor/${userId}`}>Выбрать исполнителя</Button> }
            </Alert>
            }
        </>
    );
}

export default TenderSupplier;