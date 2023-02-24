import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};
  message = "";

  constructor(private accountSetting: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register() {
  this.accountSetting.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: error => {
        this.toastr.error(error.error, "Error");
        console.log(error);
        
      }
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
