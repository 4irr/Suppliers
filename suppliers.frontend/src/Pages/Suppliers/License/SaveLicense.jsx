import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Container, Form, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { loadUser } from "../../../auth/user-service";
import Loader from "../../../Components/Loader/Loader";

const SaveLicense = () => {

    const router = useNavigate();
    const [file, setFile] = useState();
    const [supplier, setSupplier] = useState({});
    const [isContentLoading, setIsContentLoading] = useState(true);
    const [isResultFailded, setIsResultFailed] = useState(false);

    async function getSupplier() {
        setIsContentLoading(true);
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        let user = await loadUser();
        const result = await fetch(`https://localhost:7214/api/users/${user.profile.sub}`, options);
        if(result.ok){
            const info = await result.json();
            setSupplier(info);
        }
        setIsContentLoading(false);
    }

    async function sendLicense(formData) {
        const options = {
            method: 'POST',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            },
            body: formData
        }
        var result = await fetch(`https://localhost:7214/api/users/save-license`, options);
        if(result.ok) {
            setIsResultFailed(false);
            router('/supplier');
        }
        else {
            console.log(result.status);
            setIsResultFailed(true);
        }
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        const formData = new FormData();
        formData.append('formFile', file);
        sendLicense(formData);
    };

    useEffect(() => {
        getSupplier();
    }, []);

    return (
        <>
            <Header role='Supplier'/>
            {isContentLoading 
            ?
            <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
                <Loader/>
            </Container>
            :
            <Container className='content-container'>
                <h3>Загрузка лицензии поставщика</h3>
                { supplier.isLicenseLoaded
                ?
                <span className="text-success">Ваша лицензия уже была загружена</span>
                :
                <Form className="py-3" onSubmit={handleSubmit} encType="multipart/form-data">
                    <Form.Group className="mb-3">
                        <Form.Label>Выберите лицензию из списка файлов</Form.Label>
                        <Form.Control type="file" placeholder="Введите наименование товара" onChange={(e) => setFile(e.target.files[0])} required/>
                    </Form.Group>
                    {isResultFailded && <span className="d-block text-danger">Ошибка!<br></br>Не удалось сохранить лицензию</span>}
                    <Button variant="success" className="my-3" type="submit">
                        Загрузить
                    </Button>
                </Form>}
            </Container>
            }
        </>
    );
};

export default SaveLicense;