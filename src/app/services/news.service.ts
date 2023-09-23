import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from './base.service';


@Injectable({
  providedIn: 'root'
})
export class NewsService extends BaseService {

  constructor(
    public override http: HttpClient
  ) {
    super(http)
  }

  getNewsByCategory(cat: string){
    if(cat !== ''){
      return this.http.get(this.API_URL + `top-headlines?category=${cat}&country=us&sortBy=popularity&apiKey=` + this.API_KEY);
    }
    else{
      return this.http.get(this.API_URL + 'top-headlines?country=us&sortBy=popularity&apiKey=' + this.API_KEY);
    }
  }
}
