import { useEffect, useState } from 'react';
import '../../../App.css';
import {  Button, Container, Dropdown, Form } from 'react-bootstrap';
import Header from '../../../Components/Header';
import Supplier from '../../Common/Suppliers/Supplier';
import Loader from '../../../Components/Loader/Loader';
import SuppliersFilterOptions from '../../Common/Suppliers/SuppliersFilterOptions';

const SuppliersList = () => {

    const [suppliers, setSuppliers] = useState([]);
    const [sortedFilteredSuppliers, setSortedFilteredSuppliers] = useState([]);
    const [isContentLoading, setIsContentLoading] = useState(true);

    const [sortState, setSortState] = useState('firstName');
    const [sortOption, setSortOption] = useState('asc')
    const [filterQuery, setFilterQuery] = useState({ organization: '', isLicensed: 'all', isEnabled: 'all' }); 

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

        if(filterQuery.isEnabled === 'blocked')
            filteredList = filteredList.filter(item => !item.isEnabled);
        else if(filterQuery.isEnabled === 'active')
            filteredList = filteredList.filter(item => item.isEnabled);

        var sortedList = (sortOption === 'asc') 
            ? [...filteredList].sort((a, b) => a[sortState].localeCompare(b[sortState]))
            : [...filteredList].sort((a, b) => b[sortState].localeCompare(a[sortState]));
        setSortedFilteredSuppliers(sortedList);
        document.body.click();
    };

    const handleReset = () => {
        setSortedFilteredSuppliers(suppliers);
        setFilterQuery({ organization: '', isLicensed: 'all', isEnabled: 'all' });
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
                <SuppliersFilterOptions sortState={sortState} setSortState={setSortState} setSortOption={setSortOption}
                    filterQuery={filterQuery} setFilterQuery={setFilterQuery} handleSubmit={handleSubmit} handleReset={handleReset}/>
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