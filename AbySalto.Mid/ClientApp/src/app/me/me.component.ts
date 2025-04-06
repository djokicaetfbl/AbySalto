import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { User } from '../_models/user';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-me',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './me.component.html',
  styleUrl: './me.component.css',
})
export class MeComponent implements OnInit {
  private router = inject(Router);
  accountService = inject(AccountService);
  user: any | null = null;

  ngOnInit(): void {
    this.accountService.me().subscribe({
      next: (user) => {
        this.user = user;
        console.log('User data fetched successfully', user);
      },
      error: (error) => {
        console.error('Error fetching user data', error);
        this.router.navigateByUrl('/login');
      },
    });
  }
}
