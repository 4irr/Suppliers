import React from "react";
import { Dropdown, Form, Button } from "react-bootstrap";

const SuppliersFilterOptions = ({sortState, setSortState, setSortOption, filterQuery, setFilterQuery, handleSubmit, handleReset}) => {
    return (
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
                        <Form.Group>
                            <Form.Label>Статус аккаунта</Form.Label>
                            <Form.Select type="text" required value={filterQuery.isEnabled}
                                onChange={(e) => setFilterQuery({...filterQuery, isEnabled: e.target.value})}>
                                <option value='all'>Любой</option>
                                <option value='blocked'>Заблокированный</option>
                                <option value='active'>Активный</option>
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
    );
};

export default SuppliersFilterOptions;