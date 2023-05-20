import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import { apiGetClientsByCompany } from '../services/ClientService';

export const getClientsByCompanyId = createAsyncThunk(
  'cient/getListOfCompanies',
  async (companyId) => {
    const response = await apiGetClientsByCompany(companyId);
    return response.data;
  }
);

export const clientSlice = createSlice({
  name: 'client',
  initialState: {
    clients: [],
    clientSelectedToWork: null,
    loading: false,
    clientCreateMode: false,
    clientEditMode: false,
  },

  reducers: {
    toogleClientEditMode: (state) => {
      state.clientEditMode = !state.clientEditMode;
    },
    toogleClientCreateMode: (state) => {
      state.clientCreateMode = !state.clientCreateMode;
    },
    setClientSelectedToWork: (state, action) => {
      state.clientSelectedToWork = action.payload;
    },
    setClientList: (state, action) => {
      state.clients = action.payload;
    },
    addNewClient: (state, action) => {
      state.clients = [...state.clients, { ...action.payload }];
    },
  },
  extraReducers: (builder) => {
    builder.addCase(getClientsByCompanyId.pending, (state) => {
      state.loading = true;
    });

    builder.addCase(getClientsByCompanyId.fulfilled, (state, action) => {
      const { clients } = action.payload;
      state.loading = false;
      state.clients = clients;
    });
  },
});

export const {
  toogleClientEditMode,
  toogleClientCreateMode,
  setClientSelectedToWork,
  addNewClient,
  setClientList,
} = clientSlice.actions;
export default clientSlice.reducer;
