import { useEffect, useState } from 'react';
import '../../../App.css';
import { Button, Container } from 'react-bootstrap';
import Product from './Product';
import Header from '../../../Components/Header';
const ProductList = () => {

    const [products, setProducts] = useState([]);

    async function getProducts() {
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
            return info;
        }
        return [];
    }

    useEffect(() => {
        getProducts();
    }, []);

    return (
        <>
            <Header role='Supplier'/>
            <Container className='content-container'>
                <div className='productsHeader'>
                    <h3>Список товаров</h3>
                    <Button variant='warning' href='/products/add'>Добавить</Button>
                </div>
                <div>
                    {products.map(item => 
                        <Product key={item.id} item={item} products={products} setProducts={setProducts}/>
                    )}
                </div>
            </Container>
        </>
    );
}

export default ProductList;