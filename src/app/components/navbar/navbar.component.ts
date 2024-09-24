import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CategoryService } from 'src/app/services/category.service';
import { NewsService } from 'src/app/services/news.service';
import { SystemService } from 'src/app/services/system.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  news:any[] = [];
  formModel: FormGroup;
  dateStatement: string;

  selectedCategory: string = '';

  readonly headlines:string = '';
  readonly sports:string = 'sports';
  readonly technology:string = 'technology';
  readonly business:string = 'business';
  readonly entertainment:string = 'entertainment';
  readonly health:string = 'health';
  readonly science:string = 'science';

  categories: any[] = [
    { name: 'Headlines' , value: '' },
    { name: 'Sports' , value: 'sports' },
    { name: 'Technology' , value: 'technology' },
    { name: 'Business' , value: 'business' },
    { name: 'Entertainment' , value: 'entertainment' },
    { name: 'Health' , value: 'health' },
    { name: 'Science' , value: 'science' },
  ]

  constructor(
    private categoryService: CategoryService,
    private systemService: SystemService,
    private fb: FormBuilder,
  ){

  }

  ngOnInit(): void {
    this.systemService.getDateStatement().subscribe({
      next: (date: any) => {
        this.dateStatement = date.dateStatement;
      },
      error: (err) => {
        console.log("Error: ", err);
      }
    })
    if(!this.formModel){
      this.formModel = this.fb.group({
        searchItem: ['']
      })
    }
  }

  onSelectCategory(cat: string){
    this.selectedCategory = cat;
    this.categoryService.selectCategory(cat);
  }

  search(){
    let searchItem = this.formModel.get('searchItem').value;
    
    if(searchItem !== '') 
      this.categoryService.search(searchItem);
  }

}
