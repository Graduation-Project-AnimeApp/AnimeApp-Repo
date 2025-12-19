import { Component, inject, OnInit } from "@angular/core";
import { AnimeService } from "../../core/services/anime.service";
import { Anime } from "../../core/model/anime";
import { log } from "console";
import { Router } from "@angular/router";

@Component({
  selector: "app-home",
  standalone: true,
  imports: [],
  templateUrl: "./home.component.html",
  styleUrl: "./home.component.css",
})
export class HomeComponent implements OnInit {
  animeService = inject(AnimeService);
  router = inject(Router);
  topAnimeList: Anime[] = [];

  ngOnInit(): void {
    this.animeService.getLatestAnimeList().subscribe({
      next: (response) => {
        this.topAnimeList = response;
      },
    });

    console.log("API RESPONSE IS ", this.topAnimeList);
  }

  openDetails(selectedAnime: Anime) {
    this.animeService.selectedAnime = selectedAnime;
    console.log(
      "SELECTED ANIME CARD INDEX AND TITLE:",
      selectedAnime.malId,
      selectedAnime.title
    );

    this.router.navigate(["details", selectedAnime.malId]);
  }
}
