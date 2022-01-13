export const environment = {
  production: true,
  minimumLogLevel: 'INFO',
  oidc: {
    authority: '__IDENTITY_UTHORITY__',
    client_id: '__IDENTITY_CLIENT_ID__',
    redirect_uri: '__APP_URL__/app/dashboard',
    scope: 'openid profile',
    response_type: 'id_token token',
    response_mode: '',
    post_logout_redirect_uri: '',
    loadUserInfo: true
  },
  baseApiUrl: '__SHOPPERIOR_API_BASE_URL__'
};
