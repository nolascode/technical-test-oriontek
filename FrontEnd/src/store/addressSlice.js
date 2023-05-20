import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { apiGetAddressesByClientId } from '../services/AddressService';

export const getAddressesByClientId = createAsyncThunk(
  'address/getListByClient',
  async (params) => {
    const response = await apiGetAddressesByClientId(params);
    return response.data;
  }
);

export const addressSlice = createSlice({
  name: 'address',
  initialState: {
    addressList: [],
    addressSelectedToWork: null,
    loading: false,
    addressCreateMode: false,
    addressEditMode: false,
  },
  reducers: {
    toogleAddressEditMode: (state) => {
      state.addressEditMode = !state.addressEditMode;
    },
    toogleAddressCreateMode: (state) => {
      state.addressCreateMode = !state.addressCreateMode;
    },
    setAddressSelectedToWork: (state, action) => {
      state.addressSelectedToWork = action.payload;
    },
    setAddressList: (state, action) => {
      state.addressList = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(getAddressesByClientId.pending, (state, action) => {
      state.loading = true;
    });

    builder.addCase(getAddressesByClientId.fulfilled, (state, action) => {
      const { addresses } = action.payload;
      state.addressList = addresses;
    });
  },
});

export const {
  toogleAddressEditMode,
  toogleAddressCreateMode,
  setAddressSelectedToWork,
  setAddressList,
} = addressSlice.actions;
export default addressSlice.reducer;
