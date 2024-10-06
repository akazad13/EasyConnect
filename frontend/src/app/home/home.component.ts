import { NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: ``,
  imports: [NgIf, RegisterComponent],
  standalone: true,
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor() {}

  ngOnInit() {}

  enterRegisterMode() {
    this.registerMode = true;
  }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }
}