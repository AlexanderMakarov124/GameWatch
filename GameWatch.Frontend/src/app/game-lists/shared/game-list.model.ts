import { Game } from 'src/app/games/shared/game.model';

export interface GameList {
  id: number;
  name: string;
  games: Game[];
}
