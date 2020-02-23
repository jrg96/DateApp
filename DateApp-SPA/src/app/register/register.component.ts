import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};

  @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  register() {
    console.log('Register fired');
  }

  cancel() {
    console.log('Cancel fired');
    this.cancelRegister.emit(false);
  }
}
