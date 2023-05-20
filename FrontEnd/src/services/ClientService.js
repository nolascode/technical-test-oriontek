import ApiService from './ApiService';

export async function apiCreateClient(data) {
  return ApiService.fetchData({
    url: '/clients',
    method: 'post',
    data,
  });
}

export async function apiDeleteClient(params) {
  const { clientId } = params;
  return ApiService.fetchData({
    url: `/clients/${clientId}`,
    method: 'DELETE',
  });
}

export async function apiUpdateClient(data) {
  const { clientId } = data;
  return ApiService.fetchData({
    url: `/clients/${clientId}`,
    method: 'patch',
    data,
  });
}

export async function apiGetClientsByCompany(companyId) {
  return ApiService.fetchData({
    url: `/companies/${companyId}/clients`,
    method: 'get',
  });
}
