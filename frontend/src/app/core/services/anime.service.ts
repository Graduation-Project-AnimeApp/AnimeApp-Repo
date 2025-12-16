import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Anime, AnimeApiResponse } from "../model/anime";

@Injectable({
  providedIn: "root",
})
export class AnimeService {
  http = inject(HttpClient);
  baseUrl = "https://api.jikan.moe/v4/";
  selectedAnime!: Anime;

  getTopAnimeList(): Observable<AnimeApiResponse> {
    return this.http.get<AnimeApiResponse>(`${this.baseUrl}top/anime?limit=20`);
  }
}
