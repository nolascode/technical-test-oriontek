import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';
import {
  apiCreateCompany,
  apiGetCompanyList,
} from '../services/CompanyService';

export const getCompaniesListAsync = createAsyncThunk(
  'company/getListOfCompanies',
  async () => {
    const response = await apiGetCompanyList();
    return response.data;
  }
);

export const createCompaniesAsync = createAsyncThunk(
  'company/getListOfCompanies',
  async (params) => {
    const response = await apiCreateCompany(params);
    return response.data;
  }
);

export const companySlice = createSlice({
  name: 'company',
  initialState: {
    companies: [],
    companySelectedToWork: null,
    loading: false,
    creatingMode: false,
    editMode: false,
  },
  reducers: {
    setCompanySelectedToWork: (state, action) => {
      state.companySelectedToWork = action.payload;
    },
    toogleEditingMode: (state) => {
      state.editMode = !state.editMode;
    },
    toogleCreatingMode: (state) => {
      state.creatingMode = !state.creatingMode;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(getCompaniesListAsync.pending, (state) => {
      state.loading = true;
    });
    builder.addCase(getCompaniesListAsync.fulfilled, (state, action) => {
      state.loading = false;
      state.companies = action.payload;
    });
  },
});

export const {
  setCompanySelectedToWork,
  toogleCreatingMode,
  toogleEditingMode,
} = companySlice.actions;

export default companySlice.reducer;
