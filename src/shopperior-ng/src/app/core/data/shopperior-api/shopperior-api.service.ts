import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Observable, of, throwError } from 'rxjs';
import { concatMap, map, take, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoggingService } from '../../logging/logging.service';
import { ListItemModel } from '../list/models/list-tem-model';
import { ShoppingListModel } from '../list/models/shopping-list-model';
import { UserListPermissionModel } from '../list/models/user-list-permission-model';
import { UserModel } from '../user/models/user-model';
import { ApiResponseModel } from './models/api-response-model';
import { ListItemDto } from './models/list-item-dto';
import { ShoppingListDto } from './models/shopping-list-dto';
import { UserDto } from './models/user-dto';
import { UserListPermissionDto } from './models/user-list-permission-dto';

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
            const permissions = dto.permissions.map(p => {
              return <UserListPermissionModel>{
                userGuid: p.user.guid,
                userEmail: p.user.emailAddress,
                shoppingListGuid: p.shoppingListGuid,
                permission: p.permission
              };
            });
            const items = dto.items.map(i => {
              return <ListItemModel>{
                guid: i.guid,
                shoppingListGuid: i.shoppingListGuid,
                name: i.name,
                brand: i.brand,
                comment: i.comment,
                quantity: i.quantity,
                measurement: i.measurement,
                unitPrice: i.unitPrice,
                totalPrice: i.totalPrice,
                isInCart: i.isInCart,
                enteredCartTime: i.enteredCartTime,
                hasPurchased: i.hasPurchased,
                purchasedTime: i.purchasedTime
              };
            });
            return <ShoppingListModel>{
              guid: dto.guid,
              name: dto.name,
              permissions: permissions,
              items: items
            };
          });
        })
      );
    }

    add(list: ShoppingListModel): Observable<void> {
      const data = <ShoppingListDto>{
        name: list.name
      };
      return this._super.post('/api/v1/lists', data).pipe(
        map((res: void) => {
          return;
        })
      );
    }

    update(list: ShoppingListModel): Observable<void> {
      const permissions: UserListPermissionDto[] = [];
      list.permissions.forEach(p => {
        const user = <UserDto>{
          guid: p.userGuid
        };
        const permission = <UserListPermissionDto>{
          user: user,
          shoppingListGuid: p.shoppingListGuid,
          permission: p.permission
        };
        permissions.push(permission);
      });
      const data = <ShoppingListDto>{
        guid: list.guid,
        name: list.name,
        permissions: permissions
      };
      return this._super.put(`/api/v1/lists/${list.guid}`, data).pipe(
        map((res: void) => {
          return;
        })
      );
    }

    delete(guid: string): Observable<void> {
      return this._super.delete(`/api/v1/lists/${guid}`).pipe(
        map((res: void) => {
          return;
        })
      );
    }
  }(this);

  Users = new class {
    constructor(private _super: ShopperiorApiService) { }

    getOneByEmail(email: string): Observable<UserModel> {
      return this._super.get<UserDto>(`/api/v1/users?emailAddress=${email}`).pipe(
        map((res: UserDto) => {
          return <UserModel>{
            guid: res.guid,
            username: res.username,
            firstName: res.firstName,
            lastName: res.lastName,
            email: res.emailAddress
          }
        })
      )
    }
  }(this);

  private get<T>(path: string): Observable<T> {
    const uri = `${this._url}${path}`;
    this._logger.debug(`GET:${uri} Started.`);
    return this.injectAuthHeader(new HttpHeaders()).pipe(
      concatMap(headers => {
        return this._http.get<ApiResponseModel<T>>(uri, {headers});
      }),
      take(1),
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
      take(1),
      map((res: ApiResponseModel<null>) => {
        this._logger.debug(`POST:${uri} Completed.`);
        if (!res) { throwError(`POST:${uri} There was no response from the endpoint.`); }
        if (!res.isSuccess) { throwError(res.messages.join()); }
        return res.data;
      })
    );
  }

  private put(path: string, data: any): Observable<any> {
    const uri = `${this._url}${path}`;
    this._logger.debug(`PUT:${uri} Started.`);
    return this.injectAuthHeader(new HttpHeaders()).pipe(
      concatMap(headers => {
        return this._http.put(`${uri}`, data, {headers});
      }),
      take(1),
      map((res: ApiResponseModel<null>) => {
        this._logger.debug(`PUT:${uri} Completed.`);
        if (!res) { throwError(`PUT:${uri} There was no response from the endpoint.`); }
        if (!res.isSuccess) { throwError(res.messages.join()); }
        return res.data;
      })
    );    
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
