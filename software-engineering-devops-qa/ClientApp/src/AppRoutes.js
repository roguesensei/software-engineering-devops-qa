import { FetchData } from './components/FetchData';
import Home from './pages/Home';
import Login from './pages/Login';

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/login',
    element: <Login /> // https://blog.logrocket.com/authentication-react-router-v6/
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
];

export default AppRoutes;
