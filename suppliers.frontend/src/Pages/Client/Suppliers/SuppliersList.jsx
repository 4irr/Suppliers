import { useEffect, useState } from 'react';
import '../../../App.css';
import {  Container } from 'react-bootstrap';
import Header from '../../../Components/Header';
import Supplier from './Supplier';

const SuppliersList = () => {

    const [suppliers, setSuppliers] = useState([]);

    async function getSuppliers() {
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/users/suppliers`, options);
        if(result.ok){
            const info = await result.json();
            console.log(info);
            setSuppliers(info.suppliers);
            return info;
        }
        return [];
    }

    useEffect(() => {
        getSuppliers();
    }, []);

    return (
        <>
            <Header role='Client'/>
            <Container className='content-container'>
                <h3>Список поставщиков</h3>
                {suppliers.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список поставщиков пуст</h4>}
                <div>
                    {suppliers.map(item => 
                        <Supplier key={item.id} item={item}>{item.firstName}</Supplier>
                    )}
                </div>
            </Container>
        </>
    );
}

export default SuppliersList;