import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { map } from 'rxjs/operators';
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
    private _http: HttpClient) { }

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
  }(this);

  private get<T>(path: string): Observable<T> {
    const uri = `${this._url}${path}`;
    return this._http.get<ApiResponseModel<T>>(uri).pipe(
      map((res: ApiResponseModel<T>) => {
        this._logger.debug(`GET:${uri} Completed.`)
        if (!res) { throwError(`GET:${uri}: There was no response from the endpoint.`); }
        if (!res.isSuccess) { throwError(res.messages.join()); }
        return res.data;
      })
    );
  }

  private post(path: string, data: any): Observable<any> {
    return this._http.post(`${this._url}${path}`, data);
  }

  private put(path: string, data: any): Observable<any> {
    return this._http.put(`${this._url}${path}`, data);
  }

  private delete(path: string): Observable<any> {
    return this._http.delete(`${this._url}${path}`);
  }

  private handleError<T>(operation: string, result?: T) {
    return (error: any): Observable<T> => {
      this._logger.error(`ShopperiorApiService.${operation}: An error occurred.`);
      this._logger.error(error);
      return of(result as T);
    };
  }
}
