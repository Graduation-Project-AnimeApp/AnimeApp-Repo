import { Routes } from "@angular/router";
import path from "path";
import { HomeComponent } from "./features/home/home.component";
import { DetailsComponent } from "./features/details/details.component";
import { AiChatComponent } from "./features/ai-chat/ai-chat.component";
import { LoginComponent } from "./features/login/login.component";
import { PublicComponent } from "./layouts/auth-layout/public.component";
import { AppComponent } from "./app.component";
import { authGuard } from "./core/auth/guards/auth.guard";
import { PrivateComponent } from "./layouts/app-layout/private.component";
import { SignupComponent } from "./features/signup/signup.component";
import { ProfileComponent } from "./features/profile/profile.component";
import { RecommendationsComponent } from "./features/recommendations/recommendations.component";

export const routes: Routes = [
  {
    path: "",
    component: PublicComponent,
    children: [
      { path: "login", component: LoginComponent },
      { path: "signup", component: SignupComponent },
    ],
  },

  {
    path: "",
    component: PrivateComponent,
    canActivate: [authGuard],
    children: [
      { path: "home", component: HomeComponent },
      { path: "recommendations", component: RecommendationsComponent },

      { path: "details/:id", component: DetailsComponent },
      { path: "chat", component: AiChatComponent },
      { path: "profile", component: ProfileComponent },
    ],
  },

  { path: "**", redirectTo: "home" },
];
