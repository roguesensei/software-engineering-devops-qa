import Course from './pages/Course';
import Home from './pages/Home';
import Login from './pages/Login';
import User from './pages/User';

const AppRoutes = [
  {
    path: '*',
    index: true,
    element: <Home />
  },
  {
    path: '/login',
    element: <Login />
  },
  {
    path: '/courses',
    element: <Course />
  },
  {
    path: '/users',
    element: <User />
  }
];

export default AppRoutes;
