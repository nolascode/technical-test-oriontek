import ApiService from './ApiService';

export async function apiGetCompanyList() {
  return ApiService.fetchData({
    url: '/companies',
    method: 'get',
  });
}

export async function apiCreateCompany(data) {
  return ApiService.fetchData({
    url: '/companies',
    method: 'post',
    data,
  });
}

export async function apiUpdateCompany(data) {
  const { companyId } = data;
  return ApiService.fetchData({
    url: `/companies/${companyId}`,
    method: 'patch',
    data,
  });
}

export async function apiDeleteCompany(params) {
  const { companyId } = params;
  return ApiService.fetchData({
    url: `/companies/${companyId}`,
    method: 'delete',
  });
}

