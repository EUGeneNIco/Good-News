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
  newsHeadline: any;
  unsubscribe$ = new Subject<void>();
  category: string;
  searchItem: string;

  constructor(
    private newsService: NewsService,
    private categoryService: CategoryService,
  ) {
    this.categoryService.categorySelected$
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(cat => {
        // console.log('Category selected ', cat);
        this.category = cat;
        this.searchNewsByCategory(cat);
      });

    this.categoryService.searchItem$
      .pipe(takeUntil(this.unsubscribe$))
      .subscribe(searchItem => {
        // console.log(searchItem)
        this.searchItem = searchItem;
        this.searchNews();
      });

  }

  ngOnInit(): void {
    this.searchNewsByCategory('');
  }

  ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }


  searchNewsByCategory(category: string) {
    this.resetNewsDashboard();
    this.searchItem = '';

    this.newsService.getNewsByCategory(category).subscribe({
      next: (news: any) => {
        
        // this.newsData = this.prepareData(news.articles);
        
        let newsItems = this.prepareData(news.articles);
        this.newsHeadline = newsItems[0];
        this.newsData = newsItems.splice(1);
        
        // console.log(this.newsHeadline);
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    })
  }

  searchNews() {
    this.resetNewsDashboard();
    this.category = '';
    
    this.newsService.searchNews(this.searchItem).subscribe({
      next: (news: any) => {
        // console.log("news by search: ", news, this.searchItem);

        this.newsData = this.prepareData(news.articles);
      },
      error: (e) => {
        console.log('Error: ', e);
      }
    })
  }

  prepareData(data: any) {
    return data.filter(news => {
      return news.urlToImage != null;
    }).map((newsData: any) => {
      return {
        title: newsData.title.split(' - ')[0],
        content: newsData.content ? newsData.content.split(' [+')[0] : '',
        source: newsData.source,
        author: newsData.author,
        // content: newsData.content,
        description: newsData.description,
        publishedAt: newsData.publishedAt,
        url: newsData.url,
        urlToImage: newsData.urlToImage,
      }
    });
  }

  resetNewsDashboard() {
    this.newsData = [];
    this.newsHeadline = '';
  }
}
