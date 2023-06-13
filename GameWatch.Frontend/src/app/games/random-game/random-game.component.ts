import { Component, OnInit } from '@angular/core';
import { GameListService } from 'src/app/game-lists/shared/game-list.service';
import { Game } from '../shared/game.model';
import { GameService } from '../shared/game.service';

@Component({
  selector: 'app-random-game',
  templateUrl: './random-game.component.html',
  styleUrls: ['./random-game.component.css'],
})
export class RandomGameComponent implements OnInit {
  game?: Game;

  constructor(private gameService: GameService, private gameListService: GameListService) {}

  ngOnInit(): void {
    this.gameService.searchGames().subscribe(games => this.getRandomGame(games));
  }

  getRandomGame(games: Game[]): void {
    const randomNumber = Math.floor(Math.random() * games.length);

    this.game = games[randomNumber];
    this.gameListService
      .getGameListById(this.game.gameListId)
      .subscribe(gameList => (this.game!.gameListName = gameList.name));
  }
}
