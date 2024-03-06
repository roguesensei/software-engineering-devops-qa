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
    element: <Login />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
];

export default AppRoutes;
