import React, { useEffect, useState } from "react";
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import SignoutOidc from '../auth/SignoutOidc';
import SigninOidc from '../auth/SigninOidc';
import Footer from "./Footer";
import Home from "../Pages/Common/Home";
import '../App.css';
import { adminRoutes } from "../Router/AdminRoutes";
import { supplierRoutes } from "../Router/SupplierRoutes";
import { clientRoutes } from "../Router/ClientRoutes";

const AppRouter = () => {

    let routeCounter = 0;

    return (
        <div className="page-container">
            <div className="content-wrap">
                <BrowserRouter>
                    <Routes>
                        <Route path='/' Component={Home}/>
                        <Route path='/signin-oidc' Component={SigninOidc}/>
                        <Route path='/signout-oidc' Component={SignoutOidc}/>
                        {adminRoutes.map(route =>
                            <Route key={routeCounter++} path={route.path} element={route.element}/>
                        )}
                        {supplierRoutes.map(route =>
                            <Route key={routeCounter++} path={route.path} element={route.element}/>
                        )}
                        {clientRoutes.map(route =>
                            <Route key={routeCounter++} path={route.path} element={route.element}/>
                        )}
                        <Route path='*' element={<Home/>}/>
                    </Routes>
                </BrowserRouter>
            </div>
            <Footer/>
        </div>
    );
};

export default AppRouter;