import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { BankComponent } from './bank/bank.component';
import { BankAccountComponent } from './bank-account/bank-account.component';

@NgModule({
  declarations: [
    AppComponent,
    BankComponent,
    BankAccountComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }