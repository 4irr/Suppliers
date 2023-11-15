import RequireAuth from "../Components/RequireAuth";
import SupplierHome from "../Pages/Suppliers/SupplierHome";
import ProductList from "../Pages/Common/Products/ProductList";
import AddProduct from "../Pages/Suppliers/Products/AddProduct";
import EditProduct from "../Pages/Suppliers/Products/EditProduct";
import ContractsList from "../Pages/Common/Contracts/ContractsList";
import TendersList from "../Pages/Common/Tenders/TendersList";
import RegisterInRender from "../Pages/Suppliers/Tenders/RegisterInTender";
import SaveLicense from "../Pages/Suppliers/License/SaveLicense";
import CreateSingleReport from "../Pages/Common/Reports/CreateSingleReport";
import ShowSingleReport from "../Pages/Common/Reports/ShowSingleReport";

export const supplierRoutes = [
    { path: '/supplier', element: <RequireAuth role="Supplier"><SupplierHome/></RequireAuth> },
    { path: '/supplier/products', element: <RequireAuth role="Supplier"><ProductList role='Supplier'/></RequireAuth> },
    { path: '/supplier/products/add', element: <RequireAuth role="Supplier"><AddProduct/></RequireAuth> },
    { path: '/supplier/products/edit/:id', element: <RequireAuth role="Supplier"><EditProduct/></RequireAuth> },
    { path: '/supplier/orders', element: <RequireAuth role='Supplier'><ContractsList role='Supplier'/></RequireAuth> },
    { path: '/supplier/tenders', element: <RequireAuth role='Supplier'><TendersList role='Supplier'/></RequireAuth> },
    { path: '/supplier/tenders/:id/register', element: <RequireAuth role='Supplier'><RegisterInRender/></RequireAuth> },
    { path: '/supplier/save-license', element: <RequireAuth role='Supplier'><SaveLicense/></RequireAuth> },
    { path: '/supplier/analytics', element: <RequireAuth role='Supplier'><CreateSingleReport role='Supplier'/></RequireAuth> },
    { path: '/supplier/report', element: <RequireAuth role='Supplier'><ShowSingleReport role='Supplier'/></RequireAuth> }
];