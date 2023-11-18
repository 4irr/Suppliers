import { useNavigate } from "react-router-dom";
import { loadUser, signinRedirect } from "../auth/user-service";

const RequireAuth = ({role, children}) => {

    var router = useNavigate();

    if(!sessionStorage.getItem('oidc.user:https://localhost:7073:suppliers-web-api')) {
        signinRedirect();
    }
    else {
        loadUser().then(value => {
            if(value.profile.role !== role && role !== undefined)
                router('/');
        });
    }
    return children;
};

export default RequireAuth;