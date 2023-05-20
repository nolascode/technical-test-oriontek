import { useDispatch, useSelector } from 'react-redux';
import {
  getCompaniesListAsync,
  setCompanySelectedToWork,
  toogleCreatingMode,
  toogleEditingMode,
} from '../../store/companySlice';
import {
  apiCreateCompany,
  apiDeleteCompany,
  apiUpdateCompany,
} from '../../services/CompanyService';
import { getClientsByCompanyId } from '../../store/clientSlice';
import {
  setAddressList,
  setAddressSelectedToWork,
} from '../../store/addressSlice';

const useCompany = () => {
  const dispatch = useDispatch();

  const { companies, companySelectedToWork, editMode, creatingMode } =
    useSelector((state) => state.company);

  const dispatchGetCompaniesListAsync = async () => {
    dispatch(getCompaniesListAsync());
  };

  const dispatchSetCompanySelectedToWork = (company) => {
    dispatch(setCompanySelectedToWork(company));
    if (company !== null) {
      dispatch(getClientsByCompanyId(company?.id));
    }

    dispatch(setAddressSelectedToWork(null));
    dispatch(setAddressList([]));
  };

  const dispatchToogleCreatingMode = () => {
    dispatch(toogleCreatingMode());
  };

  const dispatchToogleEditingMode = () => {
    dispatch(toogleEditingMode());
  };

  const createCompanyRequest = async (payload) => {
    try {
      await apiCreateCompany(payload);
      alert('Company created successfully');
      dispatch(getCompaniesListAsync());
    } catch (error) {
      alert(JSON.stringify(error?.response?.data?.Message, null, 2));
    }
  };
  const deleteCompanyRequest = async (payload) => {
    try {
      await apiDeleteCompany({
        companyId: payload.companyId,
      });
      dispatch(getCompaniesListAsync());
    } catch (error) {
      alert(JSON.stringify(error?.response?.data?.Message, null, 2));
    }
  };

  const updateCompanyApiRequest = async (payload) => {
    try {
      await apiUpdateCompany(payload);
      dispatch(getCompaniesListAsync());
    } catch (error) {
      alert(JSON.stringify(error?.response?.data?.Message, null, 2));
    }
  };

  return {
    companies,
    companySelectedToWork,
    creatingMode,
    dispatchSetCompanySelectedToWork,
    dispatchToogleCreatingMode,
    editMode,
    dispatchToogleEditingMode,
    createCompanyRequest,
    deleteCompanyRequest,
    updateCompanyApiRequest,
    dispatchGetCompaniesListAsync,
  };
};

export default useCompany;
