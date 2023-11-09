import React from "react";
import { Alert, Button } from "react-bootstrap";

const Product = ({item}) => {

    return(
        <Alert variant={item.isLicensed ? 'success' : 'warning'} className="my-4">
            <p><b>Имя:</b> {item.firstName}</p>
            <p><b>Фамилия:</b> {item.lastName}</p>
            <p><b>Организация:</b> {item.organization}</p>
            <p><b>Email:</b> {item.email}</p>
            <p><b>Статус:</b> {item.isLicensed ? 'лицензированный поставщик' : 'лицензия не подтверждена'}</p>
            <Button href={`/client/suppliers/${item.id}/products`}>Предлагаемые товары</Button>
        </Alert>
    );
};

export default Product;