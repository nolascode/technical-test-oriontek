import ApiService from './ApiService';

export async function apiCreateAddress(data) {
  return ApiService.fetchData({
    url: '/addresses',
    method: 'post',
    data,
  });
}

export async function apiDeleteAddress(params) {
  const { id } = params;
  return ApiService.fetchData({
    url: `/addresses/${id}`,
    method: 'DELETE',
  });
}

export async function apiUpdateAddress(data) {
  const { id } = data;
  return ApiService.fetchData({
    url: `/addresses/${id}`,
    method: 'patch',
    data,
  });
}

export async function apiGetAddressesByClientId(params) {
  const { id } = params;
  return ApiService.fetchData({
    url: `/clients/${id}/addresses`,
    method: 'get',
  });
}
