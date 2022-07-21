import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Observable, of, throwError } from 'rxjs';
import { concatMap, map, take, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoggingService } from '../../logging/logging.service';
import { CategoryModel } from '../category/models/category-model';
import { ListItemModel } from '../list/models/list-tem-model';
import { ShoppingListModel } from '../list/models/shopping-list-model';
import { UserListPermissionModel } from '../list/models/user-list-permission-model';
import { UserModel } from '../user/models/user-model';
import { ApiResponseModel } from './models/api-response-model';
import { CategoryDto } from './models/category-dto';
import { ListItemDto } from './models/list-item-dto';
import { ShoppingListDto } from './models/shopping-list-dto';
import { UserDto } from './models/user-dto';
import { UserListPermissionDto } from './models/user-list-permission-dto';
import { retryWithBackoff } from './retry-with-backoff';

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
  
  Categories = new class {
    constructor(private _super: ShopperiorApiService) { }

    add(category: CategoryModel): Observable<void> {
      this._super._logger.debug('Adding category: ' + JSON.stringify(category));
      const data = <CategoryDto>{
        shoppingListGuid: category.shoppingListGuid,
        name: category.name
      };
      return this._super.post('/api/v1/categories', data);
    }
  }(this);

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
            const categories = dto.categories.map(c => {
              return <CategoryModel>{
                guid: c.guid,
                shoppingListGuid: c.shoppingListGuid,
                name: c.name
              };
            });
            const items = dto.items.map(i => {
              return <ListItemModel>{
                guid: i.guid,
                shoppingListGuid: i.shoppingListGuid,
                categoryGuid: i.categoryGuid,
                name: i.name,
                brand: i.brand,
                comment: i.comment,
                quantity: i.quantity,
                measurement: i.measurement,
                unitPrice: i.unitPrice,
                totalPrice: i.totalPrice,
                isInCart: i.isInCart,
                hasPurchased: i.hasPurchased,
              };
            });
            return <ShoppingListModel>{
              guid: dto.guid,
              name: dto.name,
              permissions: permissions,
              categories: categories,
              items: items
            };
          });
        })
      );
    }

    add(list: ShoppingListModel): Observable<string> {
      const data = <ShoppingListDto>{
        name: list.name
      };
      return this._super.post('/api/v1/lists', data).pipe(
        map((res: string) => {
          return res;
        })
      );
    }

    update(list: ShoppingListModel): Observable<void> {
      this._super._logger.debug('Updating shopping list: ' + JSON.stringify(list));
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
      const categories: CategoryDto[] = list.categories.map(c => {
        return <CategoryDto>{
          guid: c.guid,
          shoppingListGuid: c.shoppingListGuid,
          name: c.name
        };
      });
      const data = <ShoppingListDto>{
        guid: list.guid,
        name: list.name,
        permissions: permissions,
        categories: categories
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

    addItem(item: ListItemModel) {
      this._super._logger.debug(`Posting item.`);
      const dto = <ListItemDto>{
        shoppingListGuid: item.shoppingListGuid,
        categoryGuid: item.categoryGuid,
        name: item.name,
        brand: item.brand,
        comment: item.comment,
        quantity: item.quantity,
        measurement: item.measurement,
        unitPrice: item.unitPrice,
        totalPrice: item.totalPrice,
        isInCart: item.isInCart
      };
      return this._super.post(`/api/v1/lists/${item.shoppingListGuid}/items`, dto).pipe(
        map((res: void) => {
          return;
        })
      );
    }

    updateItem(item: ListItemModel) {
      this._super._logger.debug(`Putting item.`);
      const dto = <ListItemDto>{
        guid: item.guid,
        shoppingListGuid: item.shoppingListGuid,
        categoryGuid: item.categoryGuid,
        name: item.name,
        brand: item.brand,
        comment: item.comment,
        quantity: item.quantity,
        measurement: item.measurement,
        unitPrice: item.unitPrice,
        totalPrice: item.totalPrice,
        isInCart: item.isInCart,
        hasPurchased: item.hasPurchased
      };
      return this._super.put(`/api/v1/lists/${item.shoppingListGuid}/items/${item.guid}`, dto).pipe(
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
    return this.injectAuthHeader(new HttpHeaders()).pipe(
      concatMap(headers => {
        this._logger.debug(`GET:${uri} Started.`);
        return this._http.get<ApiResponseModel<T>>(uri, {headers});
      }),
      // retryWithBackoff(5000),
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
    this._logger.debug('POST data: ' + JSON.stringify(data));
    const uri = `${this._url}${path}`;
    return this.injectAuthHeader(new HttpHeaders()).pipe(
      concatMap(headers => {
        this._logger.debug(`POST:${uri} Started.`);
        return this._http.post(`${uri}`, data, {headers});
      }),
      retryWithBackoff(5000),
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
    this._logger.debug('PUT data: ' + JSON.stringify(data));
    const uri = `${this._url}${path}`;
    return this.injectAuthHeader(new HttpHeaders()).pipe(
      concatMap(headers => {
        this._logger.debug(`PUT:${uri} Started.`);
        return this._http.put(`${uri}`, data, {headers});
      }),
      retryWithBackoff(5000),
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
    return this.injectAuthHeader(new HttpHeaders()).pipe(
      concatMap(headers => {
        this._logger.debug(`DELETE:${uri} Started.`);
        return this._http.delete(`${this._url}${path}`, {headers});
      }),
      retryWithBackoff(5000),
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
