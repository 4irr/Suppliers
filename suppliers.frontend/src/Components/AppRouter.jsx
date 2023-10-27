import React from "react";
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import SignoutOidc from '../auth/SignoutOidc';
import SigninOidc from '../auth/SigninOidc';
import ProductList from "../Products/ProductList";
import RequireAuth from "./RequireAuth";
import Footer from "./Footer";
import '../App.css';
import Home from "./Home";

const AppRouter = () => {
    return (
        <div className="page-container">
            <div className="content-wrap">
                <BrowserRouter>
                    <Routes>
                        <Route path='/' Component={Home}/>
                        <Route path='/admin' element={<RequireAuth role="Admin"><ProductList/></RequireAuth>}/>
                        <Route path='/products' element={<RequireAuth role="Admin"><ProductList/></RequireAuth>}/>
                        <Route path='/signin-oidc' Component={SigninOidc}/>
                        <Route path='/signout-oidc' Component={SignoutOidc}/>
                        <Route path='*' element={<Home/>}/>
                    </Routes>
                </BrowserRouter>
            </div>
            <Footer/>
        </div>
    );
};

export default AppRouter;