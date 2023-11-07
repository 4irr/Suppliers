import RequireAuth from "../Components/RequireAuth";
import SupplierHome from "../Pages/Suppliers/SupplierHome";
import ProductList from "../Pages/Suppliers/Products/ProductList";
import AddProduct from "../Pages/Suppliers/Products/AddProduct";
import EditProduct from "../Pages/Suppliers/Products/EditProduct";
import SupplierContractsList from "../Pages/Suppliers/Contracts/SupplierContractList";
import TendersList from "../Pages/Common/Tenders/TendersList";
import RegisterInRender from "../Pages/Suppliers/Tenders/RegisterInTender";

export const supplierRoutes = [
    { path: '/supplier', element: <RequireAuth role="Supplier"><SupplierHome/></RequireAuth> },
    { path: '/supplier/products', element: <RequireAuth role="Supplier"><ProductList/></RequireAuth> },
    { path: '/supplier/products/add', element: <RequireAuth role="Supplier"><AddProduct/></RequireAuth> },
    { path: '/supplier/products/edit/:id', element: <RequireAuth role="Supplier"><EditProduct/></RequireAuth> },
    { path: '/supplier/orders', element: <RequireAuth role='Supplier'><SupplierContractsList/></RequireAuth> },
    { path: '/supplier/tenders', element: <RequireAuth role='Supplier'><TendersList role='Supplier'/></RequireAuth> },
    { path: '/supplier/tenders/:id/register', element: <RequireAuth role='Supplier'><RegisterInRender/></RequireAuth> }
];