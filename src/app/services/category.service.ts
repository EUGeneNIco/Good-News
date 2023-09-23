import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private categorySelectedSource = new Subject<string>();

  categorySelected$ = this.categorySelectedSource.asObservable();

  selectCategory(category: string){
    this.categorySelectedSource.next(category);
  }
}
