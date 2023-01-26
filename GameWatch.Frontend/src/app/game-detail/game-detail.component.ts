import { Component, Input, OnInit } from '@angular/core';
import { Game } from '../game-lists/game';
import { GameService } from '../game-lists/game.service';

@Component({
  selector: 'app-game-detail',
  templateUrl: './game-detail.component.html',
  styleUrls: ['./game-detail.component.css'],
})
export class GameDetailComponent implements OnInit {
  @Input() game?: Game;

  constructor(private gameService: GameService) {}

  ngOnInit(): void {}

  save(): void {
    if (this.game) {
      this.gameService.updateGame(this.game).subscribe();
    }
  }
}
