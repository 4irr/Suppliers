import { useEffect, useState } from 'react';
import '../../../App.css';
import { Container } from 'react-bootstrap';
import Product from './Product';
import Header from '../../../Components/Header';
import { useParams } from 'react-router-dom';

const SupplierProductList = () => {

    const [products, setProducts] = useState([]);

    const params = useParams();

    async function getProducts() {
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/Products/supplier/${params.id}`, options);
        if(result.ok){
            const info = await result.json();
            setProducts(info.products);
            return info;
        }
        return [];
    }

    useEffect(() => {
        getProducts();
    }, []);
    
    return (
        <>
            <Header role='Client'/>
            <Container className='content-container'>
                <h3>Список товаров</h3>
                {products.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список товаров поставщика пуст</h4>}
                <div>
                    {products.map(item => 
                        <Product key={item.id} item={item} supplierId={params.id}/>
                    )}
                </div>
            </Container>
        </>
    );
}

export default SupplierProductList;