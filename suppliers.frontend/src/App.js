import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import AuthProvider from './auth/auth-provider';
import userManager from './auth/user-service';
import AppRouter from './Components/AppRouter';

function App() {
  return (
    <div className="App">
      <AuthProvider userManager={userManager}>
        <AppRouter/>
      </AuthProvider>
    </div>
  );
}

export default App;
