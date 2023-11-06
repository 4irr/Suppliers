import React from "react";
import Header from "../../../Components/Header";
import { Button, Container } from "react-bootstrap";
import { useLocation, useNavigate, useParams } from "react-router-dom";

const ConfirmContract = () => {

    const router = useNavigate();
    const location = useLocation();
    const params = useParams();

    async function addBatch(batch) {
        const options = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(batch)
        }
        var result = await fetch(`https://localhost:7214/api/batches`, options);
        if(!result.ok) {
            console.log(result.status); 
        }
        else {
            return await result.json();
        }
    }

    async function addOrder(order) {
        const options = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(order)
        }
        var result = await fetch(`https://localhost:7214/api/orders`, options);
        if(!result.ok) {
            console.log(result.status); 
        }
        else {
            return await result.json();
        }
    }

    async function addContract(contract) {
        const options = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(contract)
        }
        var result = await fetch(`https://localhost:7214/api/contracts`, options);
        if(!result.ok) {
            console.log(result.status);
        }
        else {
            return await result.json();
        }
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        var batchId = await addBatch({productId: location.state.batch.productId, quantity: location.state.batch.quantity});
        var orderId = await addOrder({batchId: batchId, supplierId: params.id});
        var contractId = await addContract({ orderId: orderId });
        router(`/client/suppliers/${params.id}/products/${params.productId}/create-batch/order-confirmed`, { state: { contractId: contractId, batch: location.state.batch, supplierId: params.id, price: location.state.price }});
    };

    return (
        <div>
            <Header role='Client'/>
            <Container className="content-container" style={{height: '70vh', display: 'flex', justifyContent: 'center', alignItems: 'center', flexDirection: 'column'}}>
                <h2>Подтверджение заключения договора</h2>
                <div style={{marginTop: '50px'}}>
                    <Button style={{marginRight: '30px'}} onClick={(e) => handleSubmit(e)}>Подвердить</Button>
                    <Button variant="danger" onClick={() => router('/client/suppliers')}>Отмена</Button>
                </div>
            </Container>
        </div>
    );
};

export default ConfirmContract;