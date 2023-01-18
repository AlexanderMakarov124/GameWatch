import { Component, OnInit } from '@angular/core';
import { GameService } from './game.service';
import { Game } from './game';
import { filter, first, map, Observable, Subject, tap, toArray } from 'rxjs';
import { GameList } from './gameList';
import { GameListService } from './game-list.service';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css'],
})
export class GamesComponent implements OnInit {
  // games$: Observable<Game[]> | undefined;
  // games$: Subject<Game[]> | undefined;

  games: Game[] = [];
  gameLists: GameList[] = [];
  selectedGame?: Game;

  constructor(
    private gameService: GameService,
    private gameListService: GameListService
  ) {}

  ngOnInit(): void {
    this.getAllGames();
    this.getAllGameLists();
  }

  onSelect(game: Game): void {
    this.selectedGame = game;
  }

  getAllGames(): void {
    this.gameService.getAllGames().subscribe(games => (this.games = games));
    // this.games$ = this.gameService.getAllGames().pipe();
  }

  getAllGameLists(): void {
    this.gameListService
      .getAllGameLists()
      .subscribe(gameLists => (this.gameLists = gameLists));
  }

  delete(game: Game): void {
    this.games = this.games.filter(h => h !== game);
    // this.games$?.pipe(
    //   filter(h => h !== game)
    // )
    this.gameService.deleteGame(game.name).subscribe();
  }
}
