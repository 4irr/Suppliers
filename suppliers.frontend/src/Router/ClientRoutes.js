import RequireAuth from "../Components/RequireAuth";
import ClientHome from "../Pages/Client/ClientHome";
import SuppliersList from "../Pages/Client/Suppliers/SuppliersList";
import ProductList from "../Pages/Common/Products/ProductList";
import CreateBatch from "../Pages/Client/Batches/CreateBatch";
import ConfirmContract from "../Pages/Client/Contracts/ConfirmContract";
import ShowContract from "../Pages/Client/Contracts/ShowContract";
import ContractsList from "../Pages/Common/Contracts/ContractsList";
import TenderList from "../Pages/Common/Tenders/TendersList";
import AddTender from "../Pages/Client/Tenders/AddTender";
import EditTender from "../Pages/Client/Tenders/EditTender";
import SuppliersTenderList from "../Pages/Client/Tenders/SuppliersTenderList";
import ConfirmTenderClosing from "../Pages/Client/Tenders/ConfirmTenderClosing";
import Analytics from "../Pages/Client/Reports/Analytics";
import CreateSingleReport from "../Pages/Common/Reports/CreateSingleReport";
import ShowSingleReport from "../Pages/Common/Reports/ShowSingleReport";
import CreateTotalReport from "../Pages/Client/Reports/CreateTotalReport";
import ShowTotalReport from "../Pages/Client/Reports/ShowTotalReport";

export const clientRoutes = [
    { path: '/client', element: <RequireAuth role="Client"><ClientHome/></RequireAuth> },
    { path: '/client/suppliers', element: <RequireAuth role="Client"><SuppliersList/></RequireAuth> },
    { path: '/client/suppliers/:id/products', element: <RequireAuth role="Client"><ProductList role='Client'/></RequireAuth> },
    { path: '/client/suppliers/:id/products/:productId/create-batch', element: <RequireAuth role="Client"><CreateBatch/></RequireAuth> },
    { path: '/client/suppliers/:id/products/:productId/create-batch/confirm', element: <RequireAuth role="Client"><ConfirmContract/></RequireAuth> },
    { path: '/client/suppliers/:id/products/:productId/create-batch/order-confirmed', element: <RequireAuth role="Client"><ShowContract/></RequireAuth> },
    { path: '/client/orders', element: <RequireAuth role='Client'><ContractsList role='Client'/></RequireAuth> },
    { path: '/client/tenders', element: <RequireAuth role='Client'><TenderList role='Client'/></RequireAuth> },
    { path: '/client/tenders/add', element: <RequireAuth role="Client"><AddTender/></RequireAuth> },
    { path: '/client/tenders/edit/:id', element: <RequireAuth role="Client"><EditTender/></RequireAuth> },
    { path: '/client/tenders/:id/suppliers-list', element: <RequireAuth role="Client"><SuppliersTenderList/></RequireAuth> },
    { path: '/client/tenders/:id/executor/:executorId', element: <RequireAuth role="Client"><ConfirmTenderClosing/></RequireAuth> },
    { path: '/client/analytics', element: <RequireAuth role="Client"><Analytics/></RequireAuth> },
    { path: '/client/reports/single', element: <RequireAuth role="Client"><CreateSingleReport role='Client'/></RequireAuth> },
    { path: '/client/reports/single/show', element: <RequireAuth role="Client"><ShowSingleReport role='Client'/></RequireAuth> },
    { path: '/client/reports/total', element: <RequireAuth role="Client"><CreateTotalReport/></RequireAuth> },
    { path: '/client/reports/total/show', element: <RequireAuth role="Client"><ShowTotalReport/></RequireAuth> },
];