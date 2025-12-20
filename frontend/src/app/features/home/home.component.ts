import { Component, inject, OnInit } from "@angular/core";
import { AnimeService } from "../../core/services/anime.service";
import { Anime } from "../../core/model/anime";
import { log } from "console";
import { NavigationEnd } from "@angular/router";
import { ActivatedRoute, Router } from "@angular/router";
import { filter } from "rxjs";

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
  route = inject(ActivatedRoute);
  topAnimeList: Anime[] = [];
  isLoading = true;

  ngOnInit(): void {
    this.animeService.getLatestAnimeList().subscribe({
      next: (response) => {
        this.topAnimeList = response;
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
