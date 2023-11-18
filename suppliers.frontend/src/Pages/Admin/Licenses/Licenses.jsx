import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import Supplier from "./Supplier";
import { Container } from "react-bootstrap";
import Loader from "../../../Components/Loader/Loader";

const Licenses = () => {

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
        const result = await fetch(`https://localhost:7214/api/users`, options);
        if(result.ok){
            const info = await result.json();
            setSuppliers(info.users.filter(item => item.isLicenseLoaded && !item.isLicensed && item.role === 'Supplier'));
        }
        setIsContentLoading(false);
    }

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
            <Container className='content-container'>
                <h3>Список поставщикок, предоставивших лицензии</h3>
                {suppliers.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список поставщиков, загрузивших лицензии пуст</h4>}
                {suppliers.map(item =>
                    <Supplier key={item.id} item={item} suppliers={suppliers} setSuppliers={setSuppliers}/>
                )}
            </Container>
            }
        </>
    );
};

export default Licenses;