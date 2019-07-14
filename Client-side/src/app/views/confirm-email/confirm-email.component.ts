import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import {RegisterService} from '../../services/register.service';
import {ConfirmEmail} from '../../models/confirm.email.model';
@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent implements OnInit {
  public loading = false;
  public isConfirmed = false;
  constructor(private registerService : RegisterService) { }

  async ngOnInit() {
    var model = await this.getParams();
    this.loading = true;
    this.registerService.confirmEmail(model).subscribe(
      success=>{this.loading=false; this.isConfirmed=true},error=>console.log(error));
  }
  getParams() : ConfirmEmail {
    var model : ConfirmEmail = new ConfirmEmail();
    var urlOrigin = window.location.href;
    var regexp = new RegExp(/[^&?]*?=[^&?]*/g);
    var matches:any;
    var values = [];
    while (matches = regexp.exec(urlOrigin)) {
      values.push(matches[0]);
    }
    var regexUserId = new RegExp(/(?<=userId=).*$/);
    var regexCtoken = new RegExp(/(?<=ctoken=).*$/);
    model.userId = regexUserId.exec(values[0]).toString();
    model.ctoken = regexCtoken.exec(values[1]).toString();
    return model;
  }
}
