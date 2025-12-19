import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable, forkJoin } from "rxjs";
import {
  Anime,
  AnimeDetailsResponse,
  WatchHistoryApiResponse,
} from "../model/anime";

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

  getAnimeRecommendations(): Observable<Anime[]> {
    return this.http.get<Anime[]>(`${this.baseUrl}api/Anime`);
  }

  getAnimeById(id: string | null): Observable<Anime> {
    return this.http.get<Anime>(`${this.baseUrl}api/Anime/${id}`);
  }
  getMultipleAnimeByIds(ids: (string | null)[]): Observable<Anime[]> {
    const requests: Observable<Anime>[] = ids.map((id) =>
      this.getAnimeById(id)
    );
    return forkJoin(requests);
  }
  getAiChatResponse(requestData: any): Observable<any> {
    return this.http.post(`${this.baseUrl}api/Chat/message`, requestData);
  }

  addToWatchHistory(requestData: any): Observable<any> {
    return this.http.post(`${this.baseUrl}api/WatchHistory/add`, requestData);
  }
  getUserWatchHistory(userId: number): Observable<WatchHistoryApiResponse[]> {
    return this.http.get<WatchHistoryApiResponse[]>(
      `${this.baseUrl}api/WatchHistory/user/${userId}`
    );
  }
}
