import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <Layout>
          <Routes>
            <Route>
              {AppRoutes.map((route, index) => {
                const { element, ...rest } = route;
                return <Route key={index} {...rest} element={element} />;
              })}
            </Route>
          </Routes>
        </Layout>
      </LocalizationProvider>
    );
  }
}
