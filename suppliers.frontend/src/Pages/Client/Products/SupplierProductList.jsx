import { useEffect, useState } from 'react';
import '../../../App.css';
import { Container } from 'react-bootstrap';
import Product from './Product';
import Header from '../../../Components/Header';
import { useParams } from 'react-router-dom';
import Loader from '../../../Components/Loader/Loader';

const SupplierProductList = () => {

    const [products, setProducts] = useState([]);
    const [isContentLoading, setIsContentLoading] = useState(true);

    const params = useParams();

    async function getProducts() {
        setIsContentLoading(true);
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
        }
        setIsContentLoading(false);
    }

    useEffect(() => {
        getProducts();
    }, []);
    
    return (
        <>
            <Header role={'Client'}/>
            {isContentLoading
            ?
            <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
                <Loader/>
            </Container>
            :
            <Container className='content-container'>
                <h3>Список товаров</h3>
                {products.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список товаров поставщика пуст</h4>}
                <div>
                    {products.map(item => 
                        <Product key={item.id} item={item} supplierId={params.id}/>
                    )}
                </div>
            </Container>
            }
        </>
    );
}

export default SupplierProductList;