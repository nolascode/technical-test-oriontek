import { combineReducers } from 'redux';

import company from './companySlice';
import client from './clientSlice';
import address from './addressSlice';

const rootReducer = (asyncReducers) => (state, action) => {
  const combinedReducer = combineReducers({
    company,
    client,
    address,
    ...asyncReducers,
  });
  return combinedReducer(state, action);
};

export default rootReducer;
