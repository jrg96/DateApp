import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};

  @Output() cancelRegister = new EventEmitter();

  constructor(private authService: AuthService, private alertifyService: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    console.log('Register fired');
    this.authService.register(this.model).subscribe(next => {
      this.alertifyService.success('user registered successfully');
    }, error => {
      this.alertifyService.error(error);
    });
  }

  cancel() {
    console.log('Cancel fired');
    this.cancelRegister.emit(false);
  }
}
