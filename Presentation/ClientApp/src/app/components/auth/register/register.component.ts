import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.sass']
})
export class RegisterComponent {

  registrationForm: FormGroup = this.fb.group({
    fullName: ['', [Validators.required, Validators.minLength(8)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(32),
      Validators.pattern(/[A-Z]+/),
      Validators.pattern(/[a-z]+/), 
      Validators.pattern(/[0-9]+/),
      Validators.pattern(/[\!\?\*\.]+/) 
    ]],
    confirmPassword: ['', [Validators.required, this.passwordMatchValidator]]
  }, {
    validators: this.passwordMatchValidator
  });

  constructor(private fb: FormBuilder, private authService: AuthService) { }

  ngOnInit(): void { }

  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;

    return password === confirmPassword ? null : { mismatch: true };
  }

  get fullName() {
    return this.registrationForm.get('fullName');
  }

  get email() {
    return this.registrationForm.get('email');
  }

  get password() {
    return this.registrationForm.get('password');
  }

  get confirmPassword() {
    return this.registrationForm.get('confirmPassword');
  }

  onSubmit() {
    if (this.registrationForm.valid) {
      this.authService.register(
        this.fullName?.value, 
        this.email?.value, 
        this.password?.value);
    }
  }

}