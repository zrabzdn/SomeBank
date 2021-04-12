import {Component, OnInit} from '@angular/core';
import { Bank } from '../interfaces/bank';
import { BankAccount } from '../interfaces/bank-account';
import { BankAccountService } from '../services/bank-account.service';
import { BankService } from '../services/bank.service';

@Component({
    selector: 'app-bank-account',
    templateUrl: './bank-account.component.html',
    styleUrls: ['./bank-account.component.css']
})

export class BankAccountComponent implements OnInit{

    isNewRecord: boolean = false;
    enableEditIndex: number = null;
    statusMessage: string;
    editedBankAccount: BankAccount;

    bankAccounts: Array<BankAccount>;
    banks: Array<Bank>;

    constructor(private service: BankAccountService, private bankService: BankService) {
        this.bankAccounts = new Array<BankAccount>();
    }
  
    ngOnInit() {
        this.loadBankAccounts();   
        this.bankService.getAll()
            .subscribe(res => this.banks = res as []);    
    } 

    private loadBankAccounts() {
        this.service.getAll().subscribe((data: BankAccount[]) => {
                this.bankAccounts = data; 
            });
    }
  
    add() {    
        if(this.isNewRecord)
            return;
        this.editedBankAccount = {
            bankAccountID: 0, 
            accountHolder: '',
            accountNumber:'',
            bankID: 0,
            bankCode:''
         };   
        this.bankAccounts.push(this.editedBankAccount);               
        this.enableEditIndex = this.bankAccounts.length - 1;
        this.isNewRecord = true;
   }
  
    save(bankAccount: BankAccount) {
        if (this.isNewRecord) {            
            this.service.create(bankAccount).subscribe(data => {
                this.statusMessage = 'Bank account was successfully add',
                this.loadBankAccounts();
            });
            this.isNewRecord = false;
            this.enableEditIndex = null;
        } else {
            this.service.update(bankAccount).subscribe(data => {
                this.statusMessage = 'Bank account was successfully update',
                this.loadBankAccounts();
            });
            this.enableEditIndex = null;
        }
    }

    remove(id) {
        this.service.delete(id).subscribe(data => {
            this.statusMessage = 'Bank account was successfully delete',
            this.loadBankAccounts();
        });
    }
  
    cancel() {
       if (this.isNewRecord) {
            this.bankAccounts.pop();
            this.isNewRecord = false;
        }
        this.enableEditIndex = null;
    }

    switchEditMode(i, bankAccount: BankAccount) {        
        this.enableEditIndex = i;
      }
}
