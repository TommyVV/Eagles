import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import configureStore from './stores';
import RouteMap from './routes/routeMap.jsx';

// import './constants/interceptors';

const store = configureStore();

const root = document.getElementById('root')

ReactDOM.render(
  <Provider store={store}>
    <RouteMap />
  </Provider>,
  root
);
