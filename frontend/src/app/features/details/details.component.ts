import { Component, inject, OnInit } from "@angular/core";
import { AnimeService } from "../../core/services/anime.service";
import { Anime } from "../../core/model/anime";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-details",
  standalone: true,
  imports: [],
  templateUrl: "./details.component.html",
  styleUrl: "./details.component.css",
})
export class DetailsComponent implements OnInit {
  animeService = inject(AnimeService);
  route = inject(ActivatedRoute);
  selectedAnime!: Anime;

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get("id");
    this.animeService.getAnimeById(id).subscribe({
      next: (response) => {
        this.selectedAnime = response.data;
      },
    });
  }
}
