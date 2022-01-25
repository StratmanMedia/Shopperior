import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoggingService } from '../../logging/logging.service';
import { LocalDataService } from '../local/local-data.service';
import { ShopperiorApiService } from '../shopperior-api/shopperior-api.service';
import { UserModel } from './models/user-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private _logger = new LoggingService({
    minimumLogLevel: environment.minimumLogLevel,
    callerName: 'UserService'
  });

  constructor(
    private _local: LocalDataService,
    private _api: ShopperiorApiService) { }

  getOneByEmail(email: string): Observable<UserModel> {
    return this._api.Users.getOneByEmail(email);
  }
}
