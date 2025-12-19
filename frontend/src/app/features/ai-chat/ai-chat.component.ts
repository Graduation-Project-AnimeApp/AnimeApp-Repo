import { Component, inject } from "@angular/core";
import { AnimeService } from "../../core/services/anime.service";
import { FormsModule } from "@angular/forms";
import { AnimeChatRequest } from "../../core/model/anime";

@Component({
  selector: "app-ai-chat",
  standalone: true,
  imports: [FormsModule],
  templateUrl: "./ai-chat.component.html",
  styleUrl: "./ai-chat.component.css",
})
export class AiChatComponent {
  animeService = inject(AnimeService);
  characterName: string = "";
  placeholder: string = "Enter your character name";
  messagesSent: number = 0;
  messages: { text: string; sender: "user" | "ai" }[] = [];
  userMessage = "";

  sendMessage() {
    if (this.userMessage !== "") {
      if (this.messagesSent == 0) {
        this.messagesSent++;
        this.characterName = this.userMessage;
        this.userMessage = "";
      } else {
        this.placeholder = "Enter your message here";
        this.messages.push({ text: this.userMessage, sender: "user" });
        const requestData: AnimeChatRequest = {
          characterName: this.characterName,
          userMessage: this.userMessage,
        };
        this.animeService.getAiChatResponse(requestData).subscribe({
          next: (response) => {
            this.messages.push({
              text: response.characterResponse,
              sender: "ai",
            });
          },
        });
        this.userMessage = "";
      }
    }
  }
}
