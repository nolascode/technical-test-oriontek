import { useDispatch, useSelector } from 'react-redux';
import {
  apiCreateClient,
  apiDeleteClient,
  apiUpdateClient,
} from '../../services/ClientService';
import {
  setClientList,
  setClientSelectedToWork,
  toogleClientEditMode,
  toogleClientCreateMode,
  getClientsByCompanyId,
} from '../../store/clientSlice';
import useCompany from './useCompany';
import { getAddressesByClientId } from '../../store/addressSlice';

const useClient = () => {
  const dispatch = useDispatch();
  const { clients, clientEditMode, clientCreateMode, clientSelectedToWork } =
    useSelector((state) => state.client);
  const { companySelectedToWork } = useCompany();

  const createClientRequest = async (client) => {
    try {
      await apiCreateClient(client);
      dispatch(toogleClientCreateMode());
      dispatch(getClientsByCompanyId(companySelectedToWork.id));
    } catch (error) {
      alert(JSON.stringify(error?.response?.data?.Message, null, 2));
    }
  };

  const deleteClientRequest = async (payload) => {
    try {
      await apiDeleteClient(payload);
      dispatch(getClientsByCompanyId(companySelectedToWork.id));
      alert('Client deleted successfully');
    } catch (error) {
      alert(JSON.stringify(error?.response?.data?.Message, null, 2));
    }
  };

  const updateClientRequest = async (payload) => {
    try {
      await apiUpdateClient(payload);
      dispatch(toogleClientEditMode());
      alert('Client created successfully');
      dispatch(getClientsByCompanyId(companySelectedToWork.id));
    } catch (error) {
      alert(JSON.stringify(error?.response?.data?.Message, null, 2));
    }
  };

  const dispatchSetClientList = (payload) => {
    dispatch(setClientList(payload));
  };

  const dispatchSetClientSelectedToWork = (payload) => {
    dispatch(setClientSelectedToWork(payload));
    dispatch(getAddressesByClientId(payload));
  };

  const dispatchToogleClientEditMode = () => {
    dispatch(toogleClientEditMode());
  };

  const dispatchToogleClientCreateMode = () => {
    dispatch(toogleClientCreateMode());
  };

  return {
    clients,
    clientEditMode,
    clientCreateMode,
    clientSelectedToWork,
    dispatchToogleClientEditMode,
    dispatchToogleClientCreateMode,
    dispatchSetClientSelectedToWork,
    createClientRequest,
    deleteClientRequest,
    updateClientRequest,
    dispatchSetClientList,
  };
};

export default useClient;
