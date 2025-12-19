import { Component, inject } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { Router, RouterLink } from "@angular/router";
import { AuthService } from "../../core/auth/services/auth.service";

@Component({
  selector: "app-signup",
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: "./signup.component.html",
  styleUrl: "./signup.component.css",
})
export class SignupComponent {
  authService = inject(AuthService);
  router = inject(Router);
  username: string = "";
  email: string = "";
  password: string = "";
  confirmPassword: string = "";

  onSubmit() {
    if (
      this.username.trim() == "" ||
      this.email.trim() == "" ||
      this.password.trim() == ""
    ) {
      alert("Please fill all fields");
      return;
    } else if (this.password !== this.confirmPassword) {
      alert("Passwords do not match!");
      return;
    } else {
      const userData = {
        username: this.username,
        email: this.email,
        password: this.password,
        confirmPassword: this.confirmPassword,
      };

      this.authService.signup(userData).subscribe({
        next: () => {
          this.router.navigate(["/home"]);
        },
      });
    }
  }
}
