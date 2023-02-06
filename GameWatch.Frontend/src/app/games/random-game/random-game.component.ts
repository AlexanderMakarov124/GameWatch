import { Component, OnInit } from '@angular/core';
import { Game } from '../shared/game.model';
import { GameService } from '../shared/game.service';

@Component({
  selector: 'app-random-game',
  templateUrl: './random-game.component.html',
  styleUrls: ['./random-game.component.css'],
})
export class RandomGameComponent implements OnInit {
  game?: Game;

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.gameService.getAllGames().subscribe(games => this.getRandomGame(games));
  }

  getRandomGame(games: Game[]): void {
    const randomNumber = Math.floor(Math.random() * games.length);

    this.game = games[randomNumber];
  }
}
