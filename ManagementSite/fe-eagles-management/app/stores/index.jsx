import { createStore, applyMiddleware, compose } from 'redux';
import thunkMiddleware from 'redux-thunk';
import reducer from '../reducers/reducer';
import { globalVar } from '../constants/GlobalVariable';

const reduxDevtools = window.devToolsExtension ? window.devToolsExtension() : f => f;

export default function configureStore(initialState) {
  let appStore = globalVar.appStore;
  if (!appStore) {
    appStore = createStore(
      reducer,
      initialState,
      compose(
        applyMiddleware(thunkMiddleware),
        reduxDevtools,
      )
    );
    globalVar.appStore = appStore;
  }
  return appStore;
}
