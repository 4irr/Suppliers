import { useEffect, useState } from 'react';
import '../../../App.css';
import {  Button, Container, Dropdown, Form } from 'react-bootstrap';
import Header from '../../../Components/Header';
import Supplier from '../../Common/Suppliers/Supplier';
import Loader from '../../../Components/Loader/Loader';

const SuppliersList = () => {

    const [suppliers, setSuppliers] = useState([]);
    const [sortedFilteredSuppliers, setSortedFilteredSuppliers] = useState([]);
    const [isContentLoading, setIsContentLoading] = useState(true);

    const [sortState, setSortState] = useState('firstName');
    const [sortOption, setSortOption] = useState('asc')
    const [filterQuery, setFilterQuery] = useState({ organization: '', isLicensed: 'all' }); 

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
            var suppliers = info.users.filter(item => item.role === 'Supplier');
            setSuppliers(suppliers);
            setSortedFilteredSuppliers(suppliers);
        }
        setIsContentLoading(false);
    }

    const handleSubmit = (e) => {        
        e.preventDefault();
        
        var filteredList = (filterQuery.organization !== '')
            ? suppliers.filter(item => item.organization.includes(filterQuery.organization))
            : suppliers;

        if(filterQuery.isLicensed === 'licensed')
            filteredList = filteredList.filter(item => item.isLicensed);
        else if(filterQuery.isLicensed === 'not-licensed')
            filteredList = filteredList.filter(item => !item.isLicensed);

        var sortedList = (sortOption === 'asc') 
            ? [...filteredList].sort((a, b) => a[sortState].localeCompare(b[sortState]))
            : [...filteredList].sort((a, b) => b[sortState].localeCompare(a[sortState]));
        setSortedFilteredSuppliers(sortedList);
        document.body.click();
    };

    const handleReset = () => {
        setSortedFilteredSuppliers(suppliers);
        setFilterQuery({ organization: '', isLicensed: 'all' });
        setSortState('firstName');
        document.body.click();
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
                                        <option value='firstName'>По имени</option>
                                        <option value='organization'>По организации</option>
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
                                    <Form.Label>Организация</Form.Label>
                                    <Form.Control type='text' value={filterQuery.organization}
                                        onChange={(e) => setFilterQuery({...filterQuery, organization: e.target.value})}/>
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Статус</Form.Label>
                                    <Form.Select type="text" required value={filterQuery.isLicensed}
                                        onChange={(e) => setFilterQuery({...filterQuery, isLicensed: e.target.value})}>
                                        <option value='all'>Любой</option>
                                        <option value='licensed'>Лицензированный</option>
                                        <option value='not-licensed'>Не лицензированный</option>
                                    </Form.Select>
                                </Form.Group>
                                <div style={{marginTop: '20px'}}>
                                    <Button type='submit' variant='outline-primary' style={{marginRight: '10px'}}>Применить</Button>
                                    <Button variant='outline-danger' onClick={handleReset}>Сбросить</Button>
                                </div>
                            </Form>
                        </Dropdown.ItemText>
                    </Dropdown.Menu>
                </Dropdown>
                {sortedFilteredSuppliers.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список поставщиков пуст</h4>}
                <div>
                    {sortedFilteredSuppliers.map(item => 
                        <Supplier key={item.id} item={item} role='Client'>{item.firstName}</Supplier>
                    )}
                </div>
            </Container>
            }
        </>
    );
}

export default SuppliersList;