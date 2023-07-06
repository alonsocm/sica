import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loading = false;
  login: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.login = this.fb.group({
      usuario: ['', [Validators.required, Validators.maxLength(15)]],
      password: ['', [Validators.required]],
    });
  }

  ingresar() {
    this.loading = true;
    const val = this.login.value;
    this.authService.login(val.usuario, val.password).subscribe({
      next: (response) => {
        this.authService.setSession(response);
        this.router.navigate(['/home']);
      },
      error: (reponse) => {
        this.errorMessage = reponse.error.Message;
        this.loading = false;
      },
    });
  }

  ngOnInit(): void {
    if(this.authService.isLoggedIn()){
      this.router.navigate(['/home']);
    }
  }
}
