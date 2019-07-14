import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Register } from '../../models/register.model';
import { RegisterService } from '../../services/register.service'
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  CreateAccountForm: FormGroup;
  registerInfo: Register = new Register();
  isAcceptTerms = false;
  isCreated = false;
  isFailure = false;
  public loading = false;
  constructor(private frmBuilder: FormBuilder, private registerService: RegisterService) { }

  ngOnInit() {
    this.CreateAccountForm = this.frmBuilder.group({
      fullname: [this.registerInfo.fullname ="Nguyen Van Tam", Validators.compose([Validators.required])],
      email: [this.registerInfo.email ="tamnv1008@gmail.com", Validators.compose([Validators.required])],
      password: [this.registerInfo.password="Tam1234567@123", Validators.compose([Validators.required])],
      confirmPass: ['Tam1234567@123', Validators.compose([Validators.required])]
    },{validators:this.checkPasswords});
  }
  checkPasswords(group: FormGroup) { // here we have the 'passwords' group
    let pass = group.controls.password.value;
    let confirmPass = group.controls.confirmPass.value;

    return pass === confirmPass ? null : { notSame: true }
  }
  onCreateAccount() {
    this.loading = true;
    this.registerService.register(this.registerInfo).subscribe(
      success => {
        this.loading=false;
        this.isCreated = true;
        this.isFailure = false;
      }, 
      error => {
        this.loading=false
        this.isCreated = true;
        this.isFailure = true;
      });
  }
}
