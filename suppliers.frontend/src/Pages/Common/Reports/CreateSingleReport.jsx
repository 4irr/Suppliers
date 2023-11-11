import React, { useEffect, useState } from "react";
import { Container, Form, Button } from "react-bootstrap";
import Header from "../../../Components/Header";
import { useNavigate } from "react-router-dom";
import { loadUser } from "../../../auth/user-service";
import Loader from "../../../Components/Loader/Loader";

const CreateSingleReport = ({role}) => {

    const [reportDto, setReportDto] = useState({supplierId: '', beginning: '', ending: ''});
    const router = useNavigate();
    const [isValidationValied, setIsValidationFailed] = useState(false);
    const [suppliers, setSuppliers] = useState([]);
    const [isContentLoading, setIsContentLoading] = useState(true);

    async function getSuppliers() {
        setIsContentLoading(true);
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/users/suppliers`, options);
        if(result.ok){
            const info = await result.json();
            setSuppliers(info.suppliers);
        }
        setIsContentLoading(false);
    }

    async function createSingleReport(reportDto) {
        const options = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token'),
                "Content-Type": "application/json"
            },
            body: JSON.stringify(reportDto)
        }
        var result = await fetch(`https://localhost:7214/api/reports/single`, options);
        if(result.ok) {
            const info = await result.json();
            let route = role === 'Supplier' ? '/supplier/report' : '/client/reports/single/show';
            router(route, { state: { item: info } });
        }
        if(result.status === 400) {
            setIsValidationFailed(true);
        }
        console.log(result.statusText);
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        const report = {
            supplierId: reportDto.supplierId === '' ? suppliers[0].id : reportDto.supplierId,
            beginning: reportDto.beginning,
            ending: reportDto.ending
        };
        createSingleReport(report);
    }

    useEffect(() => {
        if(role === 'Supplier') {
            loadUser().then(value => {
                setReportDto({...reportDto, supplierId: value.profile.sub})
                setIsContentLoading(false);
            });
        }
        else {
            getSuppliers();
        }
    }, []);

    return (
        <>
            <Header role={role}/>
            {isContentLoading
            ?
            <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
                <Loader/>
            </Container>
            :
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
                        {role === 'Client' &&
                        <Form.Group className="mb-3">
                        <Form.Label>Поставщик</Form.Label>
                            <Form.Select type="text" required
                                onChange={(e) => setReportDto({...reportDto,supplierId: e.target.value})}>
                                    {suppliers.map(item => <option key={item.id} value={item.id}>{item.organization}</option>)}
                                </Form.Select>
                        </Form.Group>}
                        {isValidationValied && <span className="d-block text-danger">Данные введены неверно!</span>}
                        <Button variant="success" className="my-3 w-100" type="submit">
                            Сформировать отчёт
                        </Button>
                    </Form>
                </div>
            </Container>
            }
        </>
    );
};

export default CreateSingleReport;