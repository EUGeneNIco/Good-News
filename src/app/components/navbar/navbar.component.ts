import { Component, OnInit } from '@angular/core';
import { CategoryService } from 'src/app/services/category.service';
import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  news:any[] = [];

  constructor(
    private categoryService: CategoryService,
  ){

  }

  ngOnInit(): void {

  }

  onSelectCategory(cat: string){
    this.selectByCategory(cat);
  }

  selectByCategory(cat: string){
    this.categoryService.selectCategory(cat);
  }

}
