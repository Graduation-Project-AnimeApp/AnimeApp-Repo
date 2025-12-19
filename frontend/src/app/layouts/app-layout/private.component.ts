import { Component } from "@angular/core";
import { NavbarComponent } from "../../shared/navbar/navbar.component";
import { RouterOutlet } from "@angular/router";
@Component({
  selector: "app-private",
  standalone: true,
  imports: [NavbarComponent, RouterOutlet],
  templateUrl: "./private.component.html",
  styleUrl: "./private.component.css",
})
export class PrivateComponent {}
