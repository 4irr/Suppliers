import { useEffect, useState } from 'react';
import '../../../App.css';
import { Button, Container } from 'react-bootstrap';
import Product from './Product';
import Header from '../../../Components/Header';
import Loader from '../../../Components/Loader/Loader';
const ProductList = () => {

    const [products, setProducts] = useState([]);
    const [isContentLoading, setIsContentLoading] = useState(true);

    async function getProducts() {
        setIsContentLoading(true);
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/Products`, options);
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
            <Header role='Supplier'/>
            {isContentLoading
            ?
            <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
                <Loader/>
            </Container>
            :
            <Container className='content-container'>
                <div className='items-header'>
                    <h3>Список товаров</h3>
                    <Button variant='warning' href='/supplier/products/add'>Добавить</Button>
                </div>
                {products.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список ваших товаров пуст</h4>}
                <div>
                    {products.map(item => 
                        <Product key={item.id} item={item} products={products} setProducts={setProducts}/>
                    )}
                </div>
            </Container>
            }
        </>
    );
}

export default ProductList;