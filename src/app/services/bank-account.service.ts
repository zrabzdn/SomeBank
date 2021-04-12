import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'; 
import { BankAccount } from '../interfaces/bank-account';
import { environment } from 'src/environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})

export class BankAccountService {

    httpOptions = {
        headers: new HttpHeaders({
            'Content-Type': 'application/json'
        })
    }

  constructor(private http: HttpClient){ }  

    getAll(): Observable<BankAccount[]> {
        return this.http.get<BankAccount[]>(environment.apiBaseURI + '/BankAccount/')
        .pipe(
        catchError(this.errorHandler)
        )
    }
 
    create(bankAccount: BankAccount){
        return this.http.post(environment.apiBaseURI + '/BankAccount/', JSON.stringify(bankAccount), this.httpOptions); 
    }

    update(bankAccount: BankAccount) {      
        return this.http.put(environment.apiBaseURI + '/BankAccount/' + bankAccount.bankAccountID, JSON.stringify(bankAccount), this.httpOptions);
    }

    delete(id: number){
        return this.http.delete(environment.apiBaseURI + '/BankAccount/' + id);
    }

    errorHandler(error) {
        let errorMessage = '';
        if(error.error instanceof ErrorEvent) {
            errorMessage = error.error.message;
        } else {
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        return throwError(errorMessage);
    }
}