import React, { useState } from "react";
import { Alert, Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const Product = ({item, role, supplierId, products, setProducts, setSortedFilteredProducts}) => {

    const [isFailed, setIsFailed] = useState(false);
    const [isRemovingImpossible, setIsRemovingImpossible] = useState(false);
    const router = useNavigate();

    async function getContracts() {
        const options = {
            method: 'GET',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }

        const result = await fetch('https://localhost:7214/api/contracts/user-contracts', options);
        if(result.ok){
            const info = await result.json();
            return info.contracts;
        }
        return null;
    }

    async function removeProduct() {
        const options = {
            method: 'DELETE',
            headers: {
                Authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }
        const result = await fetch(`https://localhost:7214/api/Products/${item.id}`, options);
        if(result.ok) {
            var newProducts = products.filter(product => product.id !== item.id);
            setSortedFilteredProducts(newProducts);
            setProducts(newProducts);
        }
    }

    const handleRemove = async () => {
        getContracts().then(value => {
            if(value === null)
                return;
            if(value.filter(contract => contract.order.batch.product.id === item.id).length > 0) {
                setIsRemovingImpossible(true);
                return;
            }
            removeProduct();
        });
    };

    function handleSubmit() {
        if(item.quantity === 0) {
            setIsFailed(true);
            return;
        }
        router(`/client/suppliers/${supplierId}/products/${item.id}/create-batch`, {state: { item: item }});
    }

    const dateOptions = {
        day: 'numeric',
        month: 'long',
        year: 'numeric'
    }

    return(
        <Alert variant="primary" className="product">
            <p><b>Название товара:</b> {item.name}</p>
            <p><b>Цена за кг. товара:</b> {item.price} р.</p>
            <p><b>Количество:</b> {item.quantity} кг.</p>
            <p><b>Товар годен до:</b> {new Date(item.expirationDate).toLocaleDateString('ru-RU', dateOptions)}</p>
            {role === 'Supplier' 
            ?
            <div>
                {isRemovingImpossible && 
                    <span className="d-block text-danger" style={{marginBottom: '20px'}}>
                        Невозможно удалить информацию о товаре, для которого существует активный заказ
                    </span>}
                <Button variant="success" style={{marginRight: '20px'}} href={`/supplier/products/edit/${item.id}`}>Изменить</Button>
                <Button variant="danger" onClick={handleRemove}>Удалить</Button>
            </div>
            :
            <div>
                {isFailed && 
                <span className="d-block text-danger" style={{marginBottom: '20px'}}>
                    Вы не можете оформить заказ с данным поставщиков, так как количество предлагаемого товара равно нулю
                </span>
                }
                <Button variant="success" onClick={() => handleSubmit()}>Оформить заказ</Button>
            </div>
            }
        </Alert>
    );
};

export default Product;