import React, { useEffect, useState } from "react";
import Header from "../../../Components/Header";
import { Container, Button } from "react-bootstrap";
import Tender from "./Tender";
import Loader from "../../../Components/Loader/Loader";

const TendersList = ({role}) => {

    const [tenders, setTenders] = useState([]);
    const [isContentLoading, setIsContentLoading] = useState(true);

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
        }
        setIsContentLoading(false);
    }

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
                {tenders.length === 0 && <h4 style={{marginTop: '150px', textAlign: 'center'}}>Список тендеров пуст</h4>}
                <div>
                    {tenders.map(item => 
                        <Tender key={item.id} item={item} role={role} tenders={tenders} setTenders={setTenders}/>
                    )}
                </div>
            </Container>
            }
        </>
    );
};

export default TendersList;