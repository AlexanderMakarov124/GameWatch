import { Component, OnInit } from '@angular/core';
import { GameService } from './game.service'
import { Game } from './game';
import { filter, first, map, Observable, Subject, tap, toArray } from 'rxjs';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit {

  // games$: Observable<Game[]> | undefined;
  // games$: Subject<Game[]> | undefined;
  games: Game[] = [];
  selectedGame?: Game;

  constructor(private gameService: GameService) { }

  ngOnInit(): void {
    this.getAllGames();
  }

  onSelect(game: Game): void {
    this.selectedGame = game;
  }

  getAllGames(): void {
    this.gameService.getAllGames().subscribe(games => this.games = games)
    // this.games$ = this.gameService.getAllGames().pipe();
  }

  add(name: string): void {
    name = name.trim();
    if (!name) { return; }

    this.gameService.createGame({ name } as Game)
      .subscribe(game => {
        this.games.push(game);

        this.ngOnInit();
      });
    // this.games$?.pipe()
  }

  delete(game: Game): void {
    this.games = this.games.filter(h => h !== game);
    // this.games$?.pipe(
    //   filter(h => h !== game)
    // )
    this.gameService.deleteGame(game.name).subscribe();
  }

}
