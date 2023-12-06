import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CategoryService } from 'src/app/services/category.service';
import { NewsService } from 'src/app/services/news.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  news:any[] = [];
  formModel: FormGroup;

  constructor(
    private categoryService: CategoryService,
    private fb: FormBuilder,
  ){

  }

  ngOnInit(): void {
    if(!this.formModel){
      this.formModel = this.fb.group({
        searchItem: ['']
      })
    }
  }

  onSelectCategory(cat: string){
    this.selectByCategory(cat);
  }

  selectByCategory(cat: string){
    this.categoryService.selectCategory(cat);
  }

  search(){
    let searchItem = this.formModel.get('searchItem').value;
    
    if(searchItem !== '') 
      this.categoryService.search(searchItem);
  }

}
