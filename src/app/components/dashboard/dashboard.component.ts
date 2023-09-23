import { Component, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { CategoryService } from 'src/app/services/category.service';
import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  newsData: any[] = [];
  unsubscribe$ = new Subject<void>();
  category: string;

  constructor(
    private newsService: NewsService,
    private categoryService: CategoryService,
  ){
    this.categoryService.categorySelected$
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(cat => {
        // console.log('Category selected ', cat);
        this.category = cat;
        this.searchNewsByCategory(cat);
      });

  }

  ngOnInit(): void {
    this.searchNewsByCategory('');
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  
  searchNewsByCategory(category: string){
    this.newsService.getNewsByCategory(category).subscribe({
      next: (news: any) => {
        // console.log("News: ", news);
        this.newsData = news.articles.filter(news => {
          return news.urlToImage != null;
        });
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    })
  }
}
