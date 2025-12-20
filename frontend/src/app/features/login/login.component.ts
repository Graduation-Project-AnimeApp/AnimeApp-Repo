import { Component, inject } from "@angular/core";
import { AuthService } from "../../core/auth/services/auth.service";
import { FormsModule } from "@angular/forms";
import { Router, RouterLink } from "@angular/router";
@Component({
  selector: "app-login",
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: "./login.component.html",
  styleUrl: "./login.component.css",
})
export class LoginComponent {
  authService = inject(AuthService);
  router = inject(Router);
  email!: string;
  password!: string;
  isLoading = false;

  onSubmit() {
    if (this.email.trim() != "" && this.password.trim() != "") {
      const userData = { email: this.email, password: this.password };
      this.isLoading = true;

      this.authService.login(userData).subscribe({
        next: (response) => {
          this.router.navigate(["/home"]);
          this.isLoading = false;
        },
      });
    }
  }
}
