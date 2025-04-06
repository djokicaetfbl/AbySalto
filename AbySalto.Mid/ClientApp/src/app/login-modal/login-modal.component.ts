import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-modal',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login-modal.component.html',
})
export class LoginModalComponent {
  model: any = {};
  accountService = inject(AccountService);
  activeModal = inject(NgbActiveModal);
  router = inject(Router);

  login() {
    this.accountService.login(this.model).subscribe({
      next: () => {
        this.activeModal.close();
        this.router.navigateByUrl('/products');
      },
    });
  }
}
