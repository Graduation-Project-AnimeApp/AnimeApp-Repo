import { Component, inject, OnInit } from "@angular/core";
import { Anime } from "../../core/model/anime";
import { AnimeService } from "../../core/services/anime.service";
import { error } from "console";

@Component({
  selector: "app-profile",
  standalone: true,
  imports: [],
  templateUrl: "./profile.component.html",
  styleUrl: "./profile.component.css",
})
export class ProfileComponent implements OnInit {
  animeService = inject(AnimeService);
  username: string | null = localStorage.getItem("username");
  watchHistory: Anime[] = [];
  isLoadingHistory = true;

  ngOnInit(): void {
    const userIdStr = localStorage.getItem("userId");
    if (!userIdStr) {
      console.error("User ID not found in localStorage!");
      return;
    }

    const userId = Number(userIdStr);
    this.animeService.getUserWatchHistory(userId).subscribe((history) => {
      const animeIds: (string | null)[] = history.map((item) =>
        item.animeApiId != null ? item.animeApiId.toString() : null
      );

      // Call your existing method
      this.animeService.getMultipleAnimeByIds(animeIds).subscribe((animes) => {
        this.watchHistory = animes;
        this.isLoadingHistory = false;
      });
    });
  }
}
