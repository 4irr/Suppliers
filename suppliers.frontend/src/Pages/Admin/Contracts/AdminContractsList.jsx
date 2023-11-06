import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Container } from "react-bootstrap";
import Contract from "./Contract";

const AdminContractsList = () => {

    const [contracts, setContracts] = useState([]);

    async function getContracts() {
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/contracts`, options);
        if(result.ok){
            const info = await result.json();
            setContracts(info.contracts);
            return info;
        }
        return [];
    }

    useEffect(() => {
        getContracts();
    }, []);

    return (
        <>
            <Header role={'Admin'}/>
            <Container className='content-container'>
                <h3 style={{marginBottom: '20px'}}>Список заказов</h3>
                {contracts.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список заказов пуст</h4>}
                <div>
                    {contracts.map(item => 
                        <Contract key={item.id} item={item} contracts={contracts} setContracts={setContracts}/>
                    )}
                </div>
            </Container>
        </>
    );
}

export default AdminContractsList;