import { Routes } from "@angular/router";
import path from "path";
import { HomeComponent } from "./features/home/home.component";
import { DetailsComponent } from "./features/details/details.component";
import { AiChatComponent } from "./features/ai-chat/ai-chat.component";

export const routes: Routes = [
  {
    path: "",
    component: HomeComponent,
  },

  { path: "details/:id", component: DetailsComponent },
  { path: "chat", component: AiChatComponent },
];
