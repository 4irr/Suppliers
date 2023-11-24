import RequireAuth from "../Components/RequireAuth";
import AdminHome from "../Pages/Admin/AdminHome";
import ContractsList from "../Pages/Common/Contracts/ContractsList";
import Licenses from "../Pages/Admin/Licenses/Licenses";
import TendersList from "../Pages/Common/Tenders/TendersList";
import ConfirmRegister from "../Pages/Admin/Accounts/ConfirmRegister";
import UserData from "../Pages/Admin/Accounts/UserData";
import EditUserData from "../Pages/Admin/Accounts/EditUserData";
import ActivityForm from "../Pages/Admin/Activity/ActivityForm";
import UserActivity from "../Pages/Admin/Activity/UserActivity";

export const adminRoutes = [
    { path: '/admin', element: <RequireAuth role='Admin'><AdminHome/></RequireAuth>},
    { path: '/admin/orders', element: <RequireAuth role="Admin"><ContractsList role='Admin'/></RequireAuth>},
    { path: '/admin/tenders', element: <RequireAuth role="Admin"><TendersList role='Admin'/></RequireAuth>},
    { path: '/admin/licenses', element: <RequireAuth role="Admin"><Licenses/></RequireAuth>},
    { path: '/admin/accounts', element: <RequireAuth role="Admin"><ConfirmRegister/></RequireAuth>},
    { path: '/admin/user-data', element: <RequireAuth role="Admin"><UserData/></RequireAuth>},
    { path: '/admin/user-data/edit', element: <RequireAuth role="Admin"><EditUserData/></RequireAuth>},
    { path: '/admin/user-activity', element: <RequireAuth role="Admin"><ActivityForm/></RequireAuth>},
    { path: '/admin/user-activity/show', element: <RequireAuth role="Admin"><UserActivity/></RequireAuth>}
];