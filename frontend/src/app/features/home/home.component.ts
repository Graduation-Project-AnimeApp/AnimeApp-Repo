import { Component, inject, OnInit } from "@angular/core";
import { AnimeService } from "../../core/services/anime.service";
import { Anime } from "../../core/model/anime";
import { log } from "console";

@Component({
  selector: "app-home",
  standalone: true,
  imports: [],
  templateUrl: "./home.component.html",
  styleUrl: "./home.component.css",
})
export class HomeComponent implements OnInit {
  animeService = inject(AnimeService);
  topAnimeList: Anime[] = [];

  ngOnInit(): void {
    this.animeService.getTopAnimeList().subscribe({
      next: (response) => {
        this.topAnimeList = response.data;
      },
    });

    console.log("API RESPONSE IS ", this.topAnimeList);
  }

  openDetails(selectedAnime: Anime) {
    this.animeService.selectedAnime = selectedAnime;
    console.log(
      "SELECTED ANIME CARD INDEX AND TITLE:",
      selectedAnime.mal_id,
      selectedAnime.title
    );
  }
}
