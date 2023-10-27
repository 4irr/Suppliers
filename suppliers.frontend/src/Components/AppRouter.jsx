import React from "react";
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import SignoutOidc from '../auth/SignoutOidc';
import SigninOidc from '../auth/SigninOidc';
import ProductList from "../Pages/Suppliers/Products/ProductList";
import RequireAuth from "./RequireAuth";
import Footer from "./Footer";
import '../App.css';
import Home from "../Pages/Common/Home";
import AdminHome from "../Pages/Admin/AdminHome";
import AddProduct from "../Pages/Suppliers/Products/AddProduct";
import EditProduct from "../Pages/Suppliers/Products/EditProduct";

const AppRouter = () => {
    return (
        <div className="page-container">
            <div className="content-wrap">
                <BrowserRouter>
                    <Routes>
                        <Route path='/' Component={Home}/>
                        <Route path='/signin-oidc' Component={SigninOidc}/>
                        <Route path='/signout-oidc' Component={SignoutOidc}/>
                        <Route path='/admin' element={<RequireAuth role="Admin"><AdminHome/></RequireAuth>}/>
                        <Route path='/supplier' element={<RequireAuth role="Supplier"><ProductList/></RequireAuth>}/>
                        <Route path='/products' element={<RequireAuth role="Supplier"><ProductList/></RequireAuth>}/>
                        <Route path='/products/add' element={<RequireAuth role="Supplier"><AddProduct/></RequireAuth>}/>
                        <Route path='/products/edit/:id' element={<RequireAuth role="Supplier"><EditProduct/></RequireAuth>}/>
                        <Route path='*' element={<Home/>}/>
                    </Routes>
                </BrowserRouter>
            </div>
            <Footer/>
        </div>
    );
};

export default AppRouter;