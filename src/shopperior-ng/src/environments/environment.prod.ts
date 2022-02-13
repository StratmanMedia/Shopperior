export const environment = {
  production: true,
  minimumLogLevel: 'INFO',
  oidc: {
    authority: '__IDENTITY_AUTHORITY__',
    audience: '__IDENTITY_AUDIENCE__',
    client_id: '__IDENTITY_CLIENT_ID__',
    redirect_uri: 'https://shopperior.com/app/dashboard',
    scope: 'openid profile',
    response_type: 'id_token token',
    response_mode: '',
    post_logout_redirect_uri: '',
    loadUserInfo: true
  },
  baseApiUrl: 'https://shopperiorapi-tst.azurewebsites.net'
};
