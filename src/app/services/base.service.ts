import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NEWS_API_KEY } from '../globals/newsapp';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  API_URL: string;
  API_KEY: string;

  constructor(
    public http: HttpClient
  ) {
    this.API_URL = 'https://newsapi.org/v2/';
    this.API_KEY = NEWS_API_KEY;
  }
}
