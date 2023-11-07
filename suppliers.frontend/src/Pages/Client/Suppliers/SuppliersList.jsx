import { useEffect, useState } from 'react';
import '../../../App.css';
import {  Container } from 'react-bootstrap';
import Header from '../../../Components/Header';
import Supplier from './Supplier';
import Loader from '../../../Components/Loader/Loader';

const SuppliersList = () => {

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

    useEffect(() => {
        getSuppliers();
    }, []);

    return (
        <>
            <Header role='Client'/>
            {isContentLoading
            ?
            <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
                <Loader/>
            </Container>
            :
            <Container className='content-container'>
                <h3>Список поставщиков</h3>
                {suppliers.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список поставщиков пуст</h4>}
                <div>
                    {suppliers.map(item => 
                        <Supplier key={item.id} item={item}>{item.firstName}</Supplier>
                    )}
                </div>
            </Container>
            }
        </>
    );
}

export default SuppliersList;