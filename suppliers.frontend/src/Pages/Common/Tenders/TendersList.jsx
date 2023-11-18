import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Container, Button, Dropdown, Form } from "react-bootstrap";
import Tender from "./Tender";
import Loader from "../../../Components/Loader/Loader";

const TendersList = ({role}) => {

    const [tenders, setTenders] = useState([]);
    const [sortedFilteredtenders, setSortedFilteredTenders] = useState([]);
    const [isContentLoading, setIsContentLoading] = useState(true);

    const [sortState, setSortState] = useState('description');
    const [sortOption, setSortOption] = useState('asc')
    const [filterQuery, setFilterQuery] = useState({ beginning: '', ending: '', isClosed: 'all' });

    async function getTenders() {
        setIsContentLoading(true);
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/tenders`, options);
        if(result.ok){
            const info = await result.json();
            setTenders(info.tenders);
            setSortedFilteredTenders(info.tenders);
        }
        setIsContentLoading(false);
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        var filteredList = tenders;

        filteredList = filterQuery.beginning !== '' 
            ? filteredList.filter(item => new Date(item.beginning) >= new Date(filterQuery.beginning))
            : filteredList;
        filteredList = filterQuery.ending !== ''
            ? filteredList.filter(item => new Date(item.ending) <= new Date(filterQuery.ending))
            : filteredList;

        if(filterQuery.isClosed === 'closed')
            filteredList = filteredList.filter(item => !item.isOpen);
        else if(filterQuery.isClosed === 'open')
            filteredList = filteredList.filter(item => item.isOpen);

        var sortedList = [];
        switch(sortState) {
            case 'description': {
                sortedList = [...filteredList].sort((a, b) => a.description.localeCompare(b.description));
                break;
            }
            case 'date': {
                sortedList = [...filteredList].sort((a, b) => {
                    if (new Date(a.beginning) > new Date(b.beginning))
                        return 1;
                    else if(new Date(a.beginning) < new Date(b.beginning))
                        return -1;
                    else return 0;
                });
                break;
            }
        }

        sortOption === 'desc' && sortedList.reverse();

        setSortedFilteredTenders(sortedList);
        document.body.click();
    };

    const handleReset = () => {
        setSortedFilteredTenders(tenders);
        setFilterQuery({ beginning: '', ending: '', isClosed: 'all' });
        setSortState('description');
        document.body.click();
    };

    useEffect(() => {
        getTenders();
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
            <Container className="content-container">
                {role ==='Client'
                ?
                <div className='items-header'>
                    <h3>Список опубликованных тендеров</h3>
                    <Button variant='warning' href='/client/tenders/add'>Добавить</Button>
                </div>
                :
                <h3>Список опубликованных тендеров</h3>
                }
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
                                        <option value='description'>По описанию</option>
                                        <option value='date'>По дате начала</option>
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
                                    <Form.Label>Дата начала</Form.Label>
                                    <Form.Control type='date' value={filterQuery.beginning}
                                        onChange={(e) => setFilterQuery({...filterQuery, beginning: e.target.value})}/>
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Дата окончания</Form.Label>
                                    <Form.Control type='date' value={filterQuery.ending}
                                        onChange={(e) => setFilterQuery({...filterQuery, ending: e.target.value})}/>
                                </Form.Group>
                                <Form.Group>
                                    <Form.Label>Статус</Form.Label>
                                    <Form.Select type="text" required value={filterQuery.isClosed}
                                        onChange={(e) => setFilterQuery({...filterQuery, isClosed: e.target.value})}>
                                        <option value='all'>Любой</option>
                                        <option value='open'>Открыт</option>
                                        <option value='closed'>Закрыт</option>
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
                {tenders.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список тендеров пуст</h4>}
                <div>
                    {sortedFilteredtenders.map(item => 
                        <Tender key={item.id} item={item} role={role} tenders={tenders} setTenders={setTenders} setSortedFilteredTenders={setSortedFilteredTenders}/>
                    )}
                </div>
            </Container>
            }
        </>
    );
};

export default TendersList;