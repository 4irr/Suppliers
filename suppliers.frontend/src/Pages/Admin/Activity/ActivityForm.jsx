import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Container, Form, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import Loader from "../../../Components/Loader/Loader";

const ActivityForm = () => {

    const [activityDto, setActivityDto] = useState({supplierId: '', beginning: '', ending: ''});
    const [isValidationValied, setIsValidationFailed] = useState(false);
    const [suppliers, setSuppliers] = useState([]);
    const [isContentLoading, setIsContentLoading] = useState(true);
    const router = useNavigate();

    async function getSuppliers() {
        setIsContentLoading(true);
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/users`, options);
        if(result.ok){
            const info = await result.json();
            setSuppliers(info.users.filter(item => item.role === 'Supplier'));
        }
        setIsContentLoading(false);
    }

    async function calculateActivity(activityDto) {
        const options = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(activityDto)
        }
        var result = await fetch(`https://localhost:7214/api/users/activity`, options);
        if(result.ok) {
            const info = await result.json();
            console.log(info);
            router('/admin/user-activity/show', { state: { item: info } });
        }
        if(result.status === 400) {
            setIsValidationFailed(true);
        }
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        const activityForm = {
            supplierId: activityDto.supplierId === '' ? suppliers[0].id : activityDto.supplierId,
            beginning: activityDto.beginning,
            ending: activityDto.ending
        };
        calculateActivity(activityForm);
    };

    useEffect(() => {
        getSuppliers();
    }, []);

    return (
        <>
            <Header role='Admin'/>
            {isContentLoading
            ?
            <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
                <Loader/>
            </Container>
            :
            <Container className='content-container' style={{height: '70vh', display: 'flex', justifyContent: 'center', alignItems: 'center', flexDirection: 'column'}}>
                <h3 className="text-center">Введите границы периода для формирования статистики об активности пользователя</h3>
                <div style={{marginTop: '50px'}}>
                    <Form onSubmit={handleSubmit}>
                        <Form.Group className="mb-3">
                            <Form.Label>Дата начала периода</Form.Label>
                            <Form.Control type="date" required value={activityDto.beginning}
                                onChange={(e) => setActivityDto({...activityDto, beginning: e.target.value})}/>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Дата окончания периода</Form.Label>
                            <Form.Control type="date" required value={activityDto.ending}
                                onChange={(e) => setActivityDto({...activityDto, ending: e.target.value})}/>
                        </Form.Group>
                        <Form.Group className="mb-3">
                        <Form.Label>Пользователь</Form.Label>
                            <Form.Select type="text" required
                                onChange={(e) => setActivityDto({...activityDto, supplierId: e.target.value})}>
                                    {suppliers.map(item => <option key={item.id} value={item.id}>{item.organization}</option>)}
                                </Form.Select>
                        </Form.Group>
                        {isValidationValied && <span className="d-block text-danger">Данные введены неверно!</span>}
                        <Button variant="success" className="my-3 w-100" type="submit">
                            Рассчитать активность
                        </Button>
                    </Form>
                </div>
            </Container>
            }
        </>
    );
};

export default ActivityForm;