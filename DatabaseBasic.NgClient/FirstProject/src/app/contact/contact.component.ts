import { Component, OnInit } from '@angular/core';
import { ContactService } from 'src/app/service/contact.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  imie = 'Kamil';

  text = 'XxxxxxxX';

  imgPath = '/assets/pobrane.jpg';

  isImageVisible = true;

  myArray = [1, 2, 3, 4, 5];

  constructor(private contactService: ContactService) { }

  ngOnInit() {
  }

  toggleImage() {
    this.isImageVisible = !this.isImageVisible;
  }

  twoWayBindingTextShow() {
    alert(this.text);
  }

  printFromInjectedService() {
    alert(this.contactService.TestMethod());
  }

}
