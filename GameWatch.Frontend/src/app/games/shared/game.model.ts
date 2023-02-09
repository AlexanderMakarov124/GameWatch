export interface Game {
  id: number;
  name: string;
  genre: string;
  description: string;
  createdAt: Date;
  gameListId: number;
  gameListName: string;
  storeLink: string;
  downloadLink: string;
}
