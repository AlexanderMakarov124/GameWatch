import { Component, Input, OnInit } from '@angular/core';
import { Game } from '../game-lists/game';
import { GameService } from '../game-lists/game.service';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css'],
})
export class GamesComponent implements OnInit {
  @Input() games?: Game[];
  selectedGame?: Game;

  constructor(private gameService: GameService) {}

  ngOnInit(): void {}

  onSelect(game: Game): void {
    this.selectedGame = game;
  }

  deleteGame(game: Game): void {
    // this.gameList?.games = this.gameList?.games.filter(h => h !== game);
    // this.games$?.pipe(
    //   filter(h => h !== game)
    // )
    this.gameService.deleteGame(game.name).subscribe();
  }
}
