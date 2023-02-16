import { Genre } from './genre.model';

export interface Game {
  id: number;
  name: string;
  genres: Genre[];
  description: string;
  createdAt: Date;
  gameListId: number;
  gameListName: string;
  storeLink: string;
  downloadLink: string;
  coverUrl: string;
  summary: string;
  releaseDate: Date;
}
