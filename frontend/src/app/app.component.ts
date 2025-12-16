import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { HomeComponent } from "./features/home/home.component";
import { NavbarComponent } from "./shared/navbar/navbar.component";

@Component({
  selector: "app-root",
  standalone: true,
  imports: [RouterOutlet, HomeComponent, NavbarComponent],
  templateUrl: "./app.component.html",
  styleUrl: "./app.component.css",
})
export class AppComponent {
  title = "my-anime";
}
