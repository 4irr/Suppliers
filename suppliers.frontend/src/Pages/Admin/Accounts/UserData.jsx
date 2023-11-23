import React from "react";
import Header from "../../../Components/Header";
import { Container, Dropdown, Form, Button } from "react-bootstrap";
import { useState, useEffect } from "react";
import Loader from "../../../Components/Loader/Loader";
import Account from "./Account";
import SuppliersFilterOptions from "../../Common/Suppliers/SuppliersFilterOptions";

const UserData = () => {

    const [isContentLoading, setIsContentLoading] = useState(true);
    const [suppliers, setSuppliers] = useState([]);
    const [sortedFilteredSuppliers, setSortedFilteredSuppliers] = useState([]);

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
        };
        const result = await fetch('https://localhost:7214/api/users', options);
        if(result.ok) {
            const info = await result.json();
            const suppliers = info.users.filter(user => user.role === 'Supplier');
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
    };

    useEffect(() => {
        getSuppliers();
    }, []);

    return (
        <>
            <Header role='Admin'/>
            {isContentLoading
            ?
            <Container style={{display: "flex", justifyContent: "center", alignItems: "center", minHeight: "70vh"}}>
                <Loader/>
            </Container>
            :
            <Container className="content-container">
                <h2>Работа с данными пользователей</h2>
                <hr></hr>
                <SuppliersFilterOptions sortState={sortState} setSortState={setSortState} setSortOption={setSortOption}
                    filterQuery={filterQuery} setFilterQuery={setFilterQuery} handleSubmit={handleSubmit} handleReset={handleReset}/>
                {sortedFilteredSuppliers.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список пользователей пуст</h4>}
                {sortedFilteredSuppliers.map(item => 
                    <Account key={item.id} item={item} suppliers={suppliers} setSuppliers={setSuppliers}/>
                )}
            </Container>
            }
        </>
    );
};

export default UserData;