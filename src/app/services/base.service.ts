import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NEWS_API_KEY } from '../globals/newsapp';

@Injectable({
  providedIn: 'root'
})
export class BaseService {
  NEWS_API_URL: string;
  NEWS_API_KEY: string;

  API_URL: string;

  constructor(
    public http: HttpClient
  ) {
    this.NEWS_API_URL = 'https://newsapi.org/v2/';
    this.NEWS_API_KEY = NEWS_API_KEY;
    this.API_URL = 'https://localhost:7134/api/';
  }
}
