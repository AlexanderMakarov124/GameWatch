import { Game } from './game';

export interface GameList {
  id: number;
  name: string;
  games: Game[];
}
