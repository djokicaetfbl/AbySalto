import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { FormsModule } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoginModalComponent } from '../login-modal/login-modal.component';
import { RegisterModalComponent } from '../register-modal/register-modal.component';

@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, FormsModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
})
export class NavComponent {
  private router = inject(Router);
  accountService = inject(AccountService);
  private modalService = inject(NgbModal);

  model: any = {};

  openLoginModal() {
    this.modalService.open(LoginModalComponent, { centered: true });
  }

  openRegisterModal() {
    this.modalService.open(RegisterModalComponent, { centered: true });
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: (_) => {
        this.router.navigateByUrl('/products');
      },
      error: (error) => console.log(error),
    });
  }

  me() {
    this.router.navigateByUrl('/me');
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
