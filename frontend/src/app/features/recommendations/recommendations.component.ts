import { Component, inject, OnInit } from "@angular/core";
import { AnimeService } from "../../core/services/anime.service";
import { Anime } from "../../core/model/anime";
import { log } from "console";
import { NavigationEnd } from "@angular/router";
import { ActivatedRoute, Router } from "@angular/router";
import { filter } from "rxjs";
@Component({
  selector: "app-recommendations",
  standalone: true,
  imports: [],
  templateUrl: "./recommendations.component.html",
  styleUrl: "./recommendations.component.css",
})
export class RecommendationsComponent {
  animeService = inject(AnimeService);
  router = inject(Router);
  route = inject(ActivatedRoute);
  recommendationAnimeList: Anime[] = [];
  isLoading = true;

  ngOnInit(): void {
    this.animeService.getAnimeRecommendations().subscribe({
      next: (response) => {
        this.recommendationAnimeList = response;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      },
    });
  }

  openDetails(selectedAnime: Anime) {
    this.animeService.selectedAnime = selectedAnime;

    this.router.navigate(["details", selectedAnime.malId]);
  }
}
