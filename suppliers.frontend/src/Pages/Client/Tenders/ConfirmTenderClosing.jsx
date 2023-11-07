import React from "react";
import Header from "../../../Components/Header";
import { Button, Container } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";

const ConfirmTenderClosing = () => {

    const router = useNavigate();
    const params = useParams();

    async function confirmClosingTender() {
        const options = {
            method: 'PUT',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        var result = await fetch(`https://localhost:7214/api/tenders/${params.id}/executor/${params.executorId}`, options);
        if(!result.ok) {
            console.log(result.status);
        }
        else {
            router('/client/tenders');
        }
    }

    const handleSubmit = () => {
        confirmClosingTender();
    };

    return (
        <>
            <Header role='Client'/>
            <Container className='content-container' style={{height: '70vh', display: 'flex', justifyContent: 'center', alignItems: 'center', flexDirection: 'column'}}>
                <h2>Подтверждение выбора исполнителя и закрытия тендера</h2>
                <div style={{marginTop: '50px'}}>
                    <Button style={{marginRight: '30px'}} onClick={() => handleSubmit()}>Подтвердить</Button>
                    <Button variant="danger" onClick={() => router('/client/tenders')}>Отмена</Button>
                </div>
            </Container>
        </>
    )
};

export default ConfirmTenderClosing;