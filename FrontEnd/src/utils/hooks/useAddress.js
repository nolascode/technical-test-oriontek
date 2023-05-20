import { useDispatch, useSelector } from 'react-redux';

import {
  apiCreateAddress,
  apiDeleteAddress,
  apiUpdateAddress,
} from '../../services/AddressService';
import {
  getAddressesByClientId,
  setAddressSelectedToWork,
  toogleAddressCreateMode,
  toogleAddressEditMode,
} from '../../store/addressSlice';
import useClient from './useClient';

const useAddress = () => {
  const dispatch = useDispatch();

  const { clientSelectedToWork } = useClient();

  const {
    addressList,
    addressSelectedToWork,
    loading,
    addressCreateMode,
    addressEditMode,
  } = useSelector((state) => state.address);

  const createAddressRequest = async (address) => {
    try {
      await apiCreateAddress(address);
      dispatch(toogleAddressCreateMode());
      dispatch(
        getAddressesByClientId({
          id: address.clientId,
        })
      );
    } catch (error) {
      alert(JSON.stringify(error?.response?.data?.Message, null, 2));
    }
  };

  const updateAddressRequest = async (address) => {
    try {
      await apiUpdateAddress(address);
      dispatch(toogleAddressEditMode());
      dispatch(
        getAddressesByClientId({
          id: address.clientId,
        })
      );
    } catch (error) {
      alert(JSON.stringify(error?.response?.data?.Message, null, 2));
    }
  };

  const deleteAddressRequest = async (address) => {
    try {
      await apiDeleteAddress(address);
      dispatch(
        getAddressesByClientId({
          id: clientSelectedToWork.id,
        })
      );
    } catch (error) {
      alert(JSON.stringify(error?.response?.data?.Message, null, 2));
    }
  };

  const dispatchSetAddressSelectedToWork = (address) => {
    dispatch(setAddressSelectedToWork(address));
  };

  const dispatchToogleAddressCreateMode = () => {
    dispatch(toogleAddressCreateMode());
  };

  const dispatchToogleAddressEditMode = () => {
    dispatch(toogleAddressEditMode());
  };

  return {
    addressList,
    addressSelectedToWork,
    loading,
    addressCreateMode,
    addressEditMode,
    createAddressRequest,
    updateAddressRequest,
    deleteAddressRequest,
    dispatchToogleAddressCreateMode,
    dispatchToogleAddressEditMode,
    dispatchSetAddressSelectedToWork,
  };
};

export default useAddress;
