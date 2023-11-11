import React, { useState } from "react";
import { Container, Form, Button } from "react-bootstrap";
import Header from "../../../Components/Header";
import { useNavigate } from "react-router-dom";

const CreateTotalReport = () => {

    const [reportDto, setReportDto] = useState({beginning: '', ending: ''});
    const router = useNavigate();
    const [isValidationValied, setIsValidationFailed] = useState(false);

    async function createTotalReport(reportDto) {
        const options = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(reportDto)
        }
        var result = await fetch(`https://localhost:7214/api/reports/total`, options);
        if(result.ok) {
            const info = await result.json();
            router('/client/reports/total/show', { state: { item: info } });
        }
        if(result.status === 400) {
            setIsValidationFailed(true);
        }
        console.log(result.statusText);
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        createTotalReport(reportDto);
    }

    return (
        <>
            <Header role='Client'/>
            <Container className="content-container" style={{height: '70vh', display: 'flex', justifyContent: 'center', alignItems: 'center', flexDirection: 'column'}}>
                <h3 className="text-center">Введите границы периода для формирования отчёта о вашей торговле</h3>
                <div style={{marginTop: '50px'}}>
                    <Form onSubmit={handleSubmit}>
                        <Form.Group className="mb-3">
                            <Form.Label>Дата начала периода</Form.Label>
                            <Form.Control type="date" required value={reportDto.beginning}
                                onChange={(e) => setReportDto({...reportDto, beginning: e.target.value})}/>
                        </Form.Group>
                        <Form.Group className="mb-3">
                            <Form.Label>Дата окончания периода</Form.Label>
                            <Form.Control type="date" required value={reportDto.ending}
                                onChange={(e) => setReportDto({...reportDto, ending: e.target.value})}/>
                        </Form.Group>
                        {isValidationValied && <span className="d-block text-danger">Данные введены неверно!</span>}
                        <Button variant="success" className="my-3 w-100" type="submit">
                            Сформировать отчёт
                        </Button>
                    </Form>
                </div>
            </Container>
        </>
    );
};

export default CreateTotalReport;