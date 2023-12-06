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
    let withCategory = '?';
    if(cat !== ''){
      withCategory = `?category=${cat}&`;
    }

    return this.http.get(this.API_URL + `top-headlines${withCategory}country=us&sortBy=popularity&apiKey=` + this.API_KEY);
  }

  searchNews(search: string){
    return this.http.get(this.API_URL + `/everything?q=${search}&from=2023-12-05&sortBy=popularity&apiKey=` + this.API_KEY);
  }
}
