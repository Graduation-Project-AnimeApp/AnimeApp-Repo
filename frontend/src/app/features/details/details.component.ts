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
  id = this.route.snapshot.paramMap.get("id");

  ngOnInit() {
    this.animeService.getAnimeById(this.id).subscribe({
      next: (response) => {
        this.selectedAnime = response;
      },
    });
  }

  addToWatchHistory() {
    const requestData = {
      userId: Number(localStorage.getItem("userId")),
      animeApiId: this.id,
    };

    console.log(requestData);

    this.animeService.addToWatchHistory(requestData).subscribe({
      next: (response) => {
        console.log(response);
      },
      error: () => alert("Couldn't proceed with your request!"),
    });
  }
}
