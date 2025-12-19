export interface Anime {
  malId: number;
  url: string;
  images: { jpg: { image_url: string } };
  title: string;
  title_english: string;
  type: string;
  episodes: number;
  status: string;
  synopsis: string;
  airing: boolean;
  score: number;
  rank: number;
  popularity: number;
  year: number;
  genres: [{ name: string }];
}

export interface AnimeDetailsResponse {
  data: Anime;
}

export interface AnimeChatRequest {
  characterName: string;
  userMessage: string;
}

export interface WatchHistoryApiResponse {
  historyId: number;
  animeApiId: number;
}
