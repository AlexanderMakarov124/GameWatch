import { Component, OnInit } from '@angular/core';
import { Game } from '../shared/game.model';
import { GameService } from '../shared/game.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-random-game',
  template: ''
})
export class RandomGameComponent implements OnInit {
  constructor(private gameService: GameService, private router: Router) {}

  ngOnInit(): void {
    this.gameService.searchGames().subscribe(games => this.getRandomGame(games));
  }

  getRandomGame(games: Game[]): void {
    const randomNumber = Math.floor(Math.random() * games.length);

    const game = games[randomNumber];
    this.router.navigate([`games/${game.id}`])
  }
}
