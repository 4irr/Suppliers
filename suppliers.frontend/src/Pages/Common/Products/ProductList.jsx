import { useEffect, useState } from 'react';
import '../../../App.css';
import { Button, Container, Dropdown, Form } from 'react-bootstrap';
import Product from './Product';
import Header from '../../../Components/Header';
import Loader from '../../../Components/Loader/Loader';
import { useParams } from 'react-router-dom';

const ProductList = ({role}) => {

    const [products, setProducts] = useState([]);
    const [sortedFilteredProducts, setSortedFilteredProducts] = useState([]);
    const [isContentLoading, setIsContentLoading] = useState(true);

    const [sortState, setSortState] = useState('price');
    const [sortOption, setSortOption] = useState('asc')
    const [filterQuery, setFilterQuery] = useState({ name: '', expirationDate: '' });

    const params = useParams();

    async function getProducts() {
        setIsContentLoading(true);
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        var url = role === 'Supplier' ? 'https://localhost:7214/api/Products' : `https://localhost:7214/api/Products/supplier/${params.id}`;
        const result = await fetch(url, options);
        if(result.ok){
            const info = await result.json();
            setProducts(info.products);
            setSortedFilteredProducts(info.products);
        }
        setIsContentLoading(false);
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        var filteredList = (filterQuery.name !== '')
            ? products.filter(item => item.name.includes(filterQuery.name))
            : products;

        if(filterQuery.expirationDate !== '')
            filteredList = filteredList.filter(item => new Date(item.expirationDate) >= new Date(filterQuery.expirationDate));

        var sortedList = [...filteredList].sort((a, b) => a[sortState] - b[sortState]);
        sortOption === 'desc' && sortedList.reverse();

        setSortedFilteredProducts(sortedList);
        document.body.click();
    };

    const handleReset = () => {
        setSortedFilteredProducts(products);
        setFilterQuery({ name: '', expirationDate: '' });
        setSortState('price');
        document.body.click();
    };

    useEffect(() => {
        getProducts();
    }, []);
    
    return (
        <>
            <Header role={role}/>
            {isContentLoading
            ?
            <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
                <Loader/>
            </Container>
            :
            <Container className='content-container'>
                <div className='items-header'>
                    <h3>Список товаров</h3>
                    {role === 'Supplier' && <Button variant='warning' href='/supplier/products/add'>Добавить</Button>}
                </div>
                <hr></hr>
                <Dropdown style={{textAlign: 'right'}} drop='start'>
                    <Dropdown.Toggle size='sm' variant="outline" id="dropdown-basic">
                        Параметры
                    </Dropdown.Toggle>

                    <Dropdown.Menu>
                        <Dropdown.Header>Сортировка</Dropdown.Header>
                        <Dropdown.ItemText>
                            <Form>
                                <Form.Group className="mb-3">
                                    <Form.Label>Параметр</Form.Label>
                                    <Form.Select type="text" required value={sortState} onChange={(e) => setSortState(e.target.value)}>
                                        <option value='price'>По цене</option>
                                        <option value='quantity'>По количеству</option>
                                    </Form.Select>
                                </Form.Group>
                                <Form.Group>
                                    <Form.Check value='asc' name='option' type='radio' label='По возрастарнию'
                                        onChange={(e) => setSortOption(e.target.value)}/>
                                    <Form.Check value='desc' name='option' type='radio' label='По убыванию'
                                        onChange={(e) => setSortOption(e.target.value)}/>
                                </Form.Group>
                            </Form>
                        </Dropdown.ItemText>
                        <hr></hr>
                        <Dropdown.Header style={{marginTop: '-10px'}}>Фильтры</Dropdown.Header>
                        <Dropdown.ItemText style={{marginBottom: '10px'}}>
                            <Form onSubmit={(e) => handleSubmit(e)}>
                                <Form.Group>
                                    <Form.Label>Название</Form.Label>
                                    <Form.Control type='text' value={filterQuery.name}
                                        onChange={(e) => setFilterQuery({...filterQuery, name: e.target.value})}/>
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Годен до</Form.Label>
                                    <Form.Control type='date' value={filterQuery.expirationDate}
                                        onChange={(e) => setFilterQuery({...filterQuery, expirationDate: e.target.value})}/>
                                </Form.Group>
                                <div style={{marginTop: '20px'}}>
                                    <Button type='submit' variant='outline-primary' style={{marginRight: '10px'}}>Применить</Button>
                                    <Button variant='outline-danger' onClick={handleReset}>Сбросить</Button>
                                </div>
                            </Form>
                        </Dropdown.ItemText>
                    </Dropdown.Menu>
                </Dropdown>
                {sortedFilteredProducts.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список товаров пуст</h4>}
                <div>
                    {sortedFilteredProducts.map(item => 
                        <Product key={item.id} item={item} role={role} products={products} supplierId={params.id} 
                            setProducts={setProducts} setSortedFilteredProducts={setSortedFilteredProducts}/>
                    )}
                </div>
            </Container>
            }
        </>
    );
}

export default ProductList;