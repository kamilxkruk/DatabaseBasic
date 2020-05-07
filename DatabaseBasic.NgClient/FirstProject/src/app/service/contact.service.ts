import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  constructor() { }

  TestMethod(){
    return 'Test text from injected service';
  }
}
