import { secrets } from '../../../../../shopperior-ng-secrets';

export const environment = {
  production: false,
  minimumLogLevel: 'DEBUG',
  oidc: {
    authority: secrets.auth0Domain,
    audience: secrets.auth0Audience,
    client_id: secrets.auth0ClientId,
    redirect_uri: 'http://localhost:8080/app/dashboard',
    scope: 'openid profile',
    response_type: 'id_token token',
    response_mode: '',
    post_logout_redirect_uri: '',
    loadUserInfo: true
  },
  baseApiUrl: 'https://localhost:7080'
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
