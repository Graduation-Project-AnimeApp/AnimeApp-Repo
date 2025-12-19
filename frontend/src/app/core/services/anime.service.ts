import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Anime, AnimeDetailsResponse } from "../model/anime";

@Injectable({
  providedIn: "root",
})
export class AnimeService {
  http = inject(HttpClient);
  baseUrl = "https://localhost:7059/";
  selectedAnime!: Anime;

  getLatestAnimeList(): Observable<Anime[]> {
    return this.http.get<Anime[]>(`${this.baseUrl}api/Anime/latest`);
  }

  getAnimeById(id: string | null): Observable<Anime> {
    return this.http.get<Anime>(`${this.baseUrl}api/Anime/${id}`);
  }

  getAiChatResponse(requestData: any): Observable<any> {
    return this.http.post(`${this.baseUrl}api/Chat/message`, requestData);
  }
}
