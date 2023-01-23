import { Component, Input, OnInit } from '@angular/core';
import { Game } from '../games/game';
import { GameListService } from '../games/game-list.service';
import { GameService } from '../games/game.service';
import { GameList } from '../games/gameList';

@Component({
  selector: 'app-game-list-detail',
  templateUrl: './game-list-detail.component.html',
  styleUrls: ['./game-list-detail.component.css'],
})
export class GameListDetailComponent implements OnInit {
  @Input() gameList?: GameList;

  selectedGame?: Game;

  constructor(private gameService: GameService, private gameListService: GameListService) {}

  ngOnInit(): void {}

  onSelect(game: Game): void {
    this.selectedGame = game;
  }

  save(): void {
    if (this.gameList) {
      this.gameListService.updateGameList(this.gameList).subscribe();
    }
  }

  deleteGame(game: Game): void {
    // this.gameList?.games = this.gameList?.games.filter(h => h !== game);
    // this.games$?.pipe(
    //   filter(h => h !== game)
    // )
    this.gameService.deleteGame(game.name).subscribe();
  }
}
