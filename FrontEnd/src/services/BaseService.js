import axios from 'axios';
import appConfig from '../configs/app.config';

const BaseService = axios.create({
  timeout: 60000,
  baseURL: appConfig.apiPrefix,
  headers: {
    Accept: 'application/json',
  },
});

export default BaseService;
