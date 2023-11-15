import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Container, Dropdown, Form, Button } from "react-bootstrap";
import Contract from "./Contract";
import Loader from "../../../Components/Loader/Loader";

const ContractsList = ({role}) => {

    const [contracts, setContracts] = useState([]);
    const [sortedFilteredContracts, setSortedFilteredContracts] = useState([]);
    const [isContentLoading, setIsContentLoading] = useState(true);

    const [sortState, setSortState] = useState('date');
    const [sortOption, setSortOption] = useState('asc')
    const [filterQuery, setFilterQuery] = useState({ product: '', isConfirmed: 'all' }); 

    async function getContracts() {
        setIsContentLoading(true);
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        var url = role === 'Supplier' ? 'https://localhost:7214/api/contracts/user-contracts' : 'https://localhost:7214/api/contracts';
        const result = await fetch(url, options);
        if(result.ok){
            const info = await result.json();
            setContracts(info.contracts);
            setSortedFilteredContracts(info.contracts);
        }
        setIsContentLoading(false);
    }

    const handleSubmit = (e) => {
        e.preventDefault();

        var filteredList = (filterQuery.product !== '')
            ? contracts.filter(item => item.order.batch.product.name.includes(filterQuery.product))
            : contracts;

        if(filterQuery.isConfirmed === 'confirmed')
            filteredList = filteredList.filter(item => item.isConfirmed);
        else if(filterQuery.isConfirmed === 'not-confirmed')
            filteredList = filteredList.filter(item => !item.isConfirmed);

        var sortedList = [];
        switch(sortState) {
            case 'date': {
                sortedList = [...filteredList].sort((a, b) => {
                    if (new Date(a.conclusionDate) > new Date(b.conclusionDate))
                        return 1;
                    else if(new Date(a.conclusionDate) < new Date(b.conclusionDate))
                        return -1;
                    else return 0;
                });
                break;
            }
            case 'quantity': {
                sortedList = [...filteredList].sort((a, b) => a.order.batch.quantity - b.order.batch.quantity);
                break;
            }
            case 'price': {
                sortedList = [...filteredList].sort((a, b) => a.order.orderPrice - b.order.OrderPrice);
                break;
            }
        }

        sortOption === 'desc' && sortedList.reverse();

        setSortedFilteredContracts(sortedList);
        document.body.click();
    };

    const handleReset = () => {
        setSortedFilteredContracts(contracts);
        setFilterQuery({ product: '', isConfirmed: 'all' });
        setSortState('date');
        document.body.click();
    } 

    useEffect(() => {
        getContracts();
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
                <h3 style={{marginBottom: '20px'}}>Список заказов</h3>
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
                                        <option value='date'>По дате</option>
                                        <option value='quantity'>По количеству товара</option>
                                        <option value='price'>По стоимости заказа</option>
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
                                    <Form.Label>Товар</Form.Label>
                                    <Form.Control type='text' value={filterQuery.product}
                                        onChange={(e) => setFilterQuery({...filterQuery, product: e.target.value})}/>
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Статус</Form.Label>
                                    <Form.Select type="text" required value={filterQuery.isConfirmed}
                                        onChange={(e) => setFilterQuery({...filterQuery, isConfirmed: e.target.value})}>
                                        <option value='all'>Любой</option>
                                        <option value='confirmed'>Подтверждённый</option>
                                        <option value='not-confirmed'>Не подтверждённый</option>
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
                {sortedFilteredContracts.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список заказов пуст</h4>}
                <div>
                    {sortedFilteredContracts.map(item => 
                        <Contract key={item.id} item={item} role={role} contracts={contracts} setContracts={setContracts}
                            setSortedFilteredContracts={setSortedFilteredContracts}/>
                    )}
                </div>
            </Container>
            }
        </>
    );
}

export default ContractsList;