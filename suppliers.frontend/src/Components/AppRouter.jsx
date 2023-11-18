import React from "react";
import { Route, Routes, BrowserRouter } from 'react-router-dom';
import SignoutOidc from '../auth/SignoutOidc';
import SigninOidc from '../auth/SigninOidc';
import Footer from "./Footer";
import Home from "../Pages/Common/Home";
import '../App.css';
import { adminRoutes } from "../Router/AdminRoutes";
import { supplierRoutes } from "../Router/SupplierRoutes";
import { clientRoutes } from "../Router/ClientRoutes";
import Profile from "../Pages/Common/Profile/Profile";
import RequireAuth from "./RequireAuth";
import EditInfo from "../Pages/Common/Profile/EditInfo";
import ChangePassword from "../Pages/Common/Profile/ChangePassword";

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
                        <Route path='/user/profile' element={<RequireAuth><Profile/></RequireAuth>}/>
                        <Route path='/user/profile/edit' element={<RequireAuth><EditInfo/></RequireAuth>}/>
                        <Route path='/user/profile/password' element={<RequireAuth><ChangePassword/></RequireAuth>}/>
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