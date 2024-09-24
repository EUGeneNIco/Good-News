import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SystemService extends BaseService {

  constructor(
    public override http: HttpClient
  ) {
    super(http)
  }

  getDateStatement() {
    return this.http.get(this.API_URL + 'system/getDateStatement');
  }
}

