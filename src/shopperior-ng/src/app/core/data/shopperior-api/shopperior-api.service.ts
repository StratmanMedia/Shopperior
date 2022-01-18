import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Observable, of, throwError } from 'rxjs';
import { concatMap, map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoggingService } from '../../logging/logging.service';
import { ShoppingListModel } from '../list/models/shopping-list-model';
import { ApiResponseModel } from './models/api-response-model';
import { ShoppingListDto } from './models/shopping-list-dto';

@Injectable({
  providedIn: 'root'
})
export class ShopperiorApiService {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'ShopperiorApiService'
  });
  private _url = environment.baseApiUrl;

  constructor(
    private _http: HttpClient,
    private _auth: AuthService) { }

  ShoppingLists = new class {
    constructor(private _super: ShopperiorApiService) { }

    getAll(): Observable<ShoppingListModel[]> {
      return this._super.get<ShoppingListDto[]>('/api/v1/lists').pipe(
        map((res: ShoppingListDto[]) => {
          return res.map((dto: ShoppingListDto) => {
            return <ShoppingListModel>{
              guid: dto.guid,
              name: dto.name
            };
          });
        })
      );
    }

    add(list: ShoppingListModel): Observable<void> {
      return this._super.post('/api/v1/lists', list).pipe(
        map((res: ApiResponseModel<void>) => {
          return;
        })
      );
    }

    delete(guid: string): Observable<void> {
      return this._super.delete(`/api/v1/lists/${guid}`).pipe(
        map((res: ApiResponseModel<null>) => {
          return;
        })
      );
    }
  }(this);

  private get<T>(path: string): Observable<T> {
    const uri = `${this._url}${path}`;
    this._logger.debug(`GET:${uri} Started.`);
    return this.injectAuthHeader(new HttpHeaders()).pipe(
      concatMap(headers => {
        return this._http.get<ApiResponseModel<T>>(uri, {headers});
      }),
      map((res: ApiResponseModel<T>) => {
        this._logger.debug(`GET:${uri} Completed.`);
        if (!res) { throwError(`GET:${uri}: There was no response from the endpoint.`); }
        if (!res.isSuccess) { throwError(res.messages.join()); }
        return res.data;
      })
    );
  }

  private post(path: string, data: any): Observable<any> {
    const uri = `${this._url}${path}`;
    this._logger.debug(`POST:${uri} Started.`);
    return this.injectAuthHeader(new HttpHeaders()).pipe(
      concatMap(headers => {
        return this._http.post(`${uri}`, data, {headers});
      }),
      map((res: ApiResponseModel<null>) => {
        this._logger.debug(`POST:${uri} Completed.`);
        if (!res) { throwError(`POST:${uri} There was no response from the endpoint.`); }
        if (!res.isSuccess) { throwError(res.messages.join()); }
        return res.data;
      })
    );
  }

  private put(path: string, data: any): Observable<any> {
    return this._http.put(`${this._url}${path}`, data);
  }

  private delete(path: string): Observable<any> {
    const uri = `${this._url}${path}`;
    this._logger.debug(`DELETE:${uri} Started.`);
    return this.injectAuthHeader(new HttpHeaders()).pipe(
      concatMap(headers => {
        return this._http.delete(`${this._url}${path}`, {headers});
      }),
      take(1),
      map((res: ApiResponseModel<null>) => {
        this._logger.debug(`DELETE:${uri} Completed.`);
        if (!res) { throwError(`DELETE:${uri} There was no response from the endpoint.`); }
        if (!res.isSuccess) { throwError(res.messages.join()); }
        return res.data;
      })
    );
  }

  private handleError<T>(operation: string, result?: T) {
    return (error: any): Observable<T> => {
      this._logger.error(`ShopperiorApiService.${operation}: An error occurred.`);
      this._logger.error(error);
      return of(result as T);
    };
  }

  private injectAuthHeader(headers: HttpHeaders): Observable<HttpHeaders> {
    return this._auth.getAccessTokenSilently().pipe(
      map((token: string) => {
        const clone = headers.set('Authorization', `Bearer ${token}`);
        return clone;
      })
    );
  }
}
