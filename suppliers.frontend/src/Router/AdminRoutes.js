import RequireAuth from "../Components/RequireAuth";
import AdminHome from "../Pages/Admin/AdminHome";
import AdminContractsList from "../Pages/Admin/Contracts/AdminContractsList";
import Licenses from "../Pages/Admin/Licenses/Licenses";
import TendersList from "../Pages/Common/Tenders/TendersList";

export const adminRoutes = [
    { path: '/admin', element: <RequireAuth role='Admin'><AdminHome/></RequireAuth>},
    { path: '/admin/orders', element: <RequireAuth role="Admin"><AdminContractsList/></RequireAuth>},
    { path: '/admin/tenders', element: <RequireAuth role="Admin"><TendersList role='Admin'/></RequireAuth>},
    { path: '/admin/licenses', element: <RequireAuth role="Admin"><Licenses/></RequireAuth>}
];