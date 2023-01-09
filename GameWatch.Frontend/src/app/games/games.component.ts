import { Component, OnInit } from '@angular/core';
import { GameService } from './game.service'
import { Game } from './game';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css']
})
export class GamesComponent implements OnInit {

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
    this.gameService.getAllGames().subscribe(games => this.games = games);
  }

  add(name: string): void {
    name = name.trim();
    if (!name) { return; }
    this.gameService.createGame({ name } as Game)
      .subscribe(game => {
        this.games.push(game);
      });
  }

  delete(game: Game): void {
    this.games = this.games.filter(h => h !== game);
    this.gameService.deleteGame(game.name).subscribe();
  }

}
