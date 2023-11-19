import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Container } from "react-bootstrap";
import Loader from "../../../Components/Loader/Loader";
import Supplier from "../../Common/Suppliers/Supplier";

const ConfirmRegister = () => {

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
            var suppliers = info.users.filter(item => item.role === 'Supplier' && !item.isRegisterConfirmed);
            setSuppliers(suppliers);
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
                <h2>Подтверждение регистрации поставщиков</h2>
                {suppliers.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список поставщиков для подтверждения регистрации пуст</h4>}
                <div>
                    {suppliers.map(item => 
                        <Supplier key={item.id} item={item} suppliers={suppliers} setSuppliers={setSuppliers} role='Admin'>{item.firstName}</Supplier>
                    )}
                </div>
            </Container>
            }
        </>
    );
};

export default ConfirmRegister;