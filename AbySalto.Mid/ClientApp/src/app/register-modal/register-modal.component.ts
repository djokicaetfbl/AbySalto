import { Component, inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { passwordMatchValidator } from '../_modules/shared/passwordMatchValidator';
import { BrowserModule } from '@angular/platform-browser';
import { AccountService } from '../_services/account.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register-modal',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register-modal.component.html',
})
export class RegisterModalComponent implements OnInit {
  registerForm!: FormGroup;
  accountService = inject(AccountService);

  constructor(private fb: FormBuilder, public modal: NgbActiveModal) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group(
      {
        username: ['', [Validators.required, Validators.minLength(4)]],
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', [Validators.required]],
        firstName: ['', [Validators.required]],
        lastName: ['', [Validators.required]],
        gender: [''],
      },
      {
        validators: passwordMatchValidator(),
      }
    );
  }

  get formControls() {
    return this.registerForm.controls;
  }

  register() {
    if (this.registerForm.invalid) return;
    const formData = this.registerForm.value;
    this.accountService.register(formData).subscribe({
      next: () => {
        this.modal.close();
      },
      error: (error) => {
        console.error('Registration failed', error);
      },
    });
    this.modal.close(formData);
  }
}
