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

  getNewsByCategory(cat: string) {
    if (typeof cat === "string" && cat.length === 0) cat = 'headlines';
    return this.http.get(this.API_URL + 'news/getNews/' + cat);
  }

  searchNews(search: string) {
    return this.http.get(this.NEWS_API_URL + `/everything?q=${search}&from=2023-12-05&sortBy=popularity&apiKey=` + this.NEWS_API_KEY);
  }
}
