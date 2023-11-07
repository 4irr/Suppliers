import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Container } from "react-bootstrap";
import { useLocation } from "react-router-dom";
import TenderSupplier from "./TenderSupplier";

const SuppliersTenderList = () => {
    
    const location = useLocation();
    const [tenderUsers, setTenderUsers] = useState([]);

    useEffect(() => {
        setTenderUsers(location.state.tender.tenderUsers);
    }, []);

    return (
        <>
            <Header role='Client'/>
            <Container className='content-container'>
                <h3>Список участников тендера</h3>
                {tenderUsers.map(item =>
                    <TenderSupplier key={item.userId} userId={item.userId} userDescription={item.userDescription} tender={location.state.tender}/>
                )}
            </Container>
        </>
    );
};

export default SuppliersTenderList;