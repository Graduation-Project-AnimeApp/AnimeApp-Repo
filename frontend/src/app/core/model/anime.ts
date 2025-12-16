export interface Anime {
  mal_id: number;
  url: string;
  images: { jpg: { image_url: string } };
  title: string;
  title_english: string;
  type: string;
  episodes: number;
  status: string;
  airing: boolean;
  score: number;
  rank: number;
  popularity: number;
  year: number;
}

export interface AnimeApiResponse {
  data: Anime[];
}
