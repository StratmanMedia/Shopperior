export const environment = {
  production: true,
  oidcClientSettings: {
    authority: 'https://dev-178567.okta.com/oauth2/default',
    client_id: '0oa1udi7d2ymhwN4R357',
    redirect_uri: 'http://localhost:4200/assets/html/signin-oidc.html',
    scope: 'openid profile',
    response_type: 'id_token token',
    response_mode: '',
    post_logout_redirect_uri: '',
    loadUserInfo: true
  },
  baseApiUrl: 'https://localhost:7080'
};
