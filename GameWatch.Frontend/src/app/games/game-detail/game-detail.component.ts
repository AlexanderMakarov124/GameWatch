import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Game } from '../shared/game.model';
import { GameService } from '../shared/game.service';
import { tap } from 'rxjs';

@Component({
  selector: 'app-game-detail',
  templateUrl: './game-detail.component.html',
  styleUrls: ['./game-detail.component.css'],
})
export class GameDetailComponent implements OnInit {
  @Input() game?: Game;
  @Output() deleted = new EventEmitter();

  constructor(private gameService: GameService) {}

  ngOnInit(): void {}

  save(): void {
    if (this.game) {
      this.gameService.updateGame(this.game).subscribe();
    }
  }

  delete(): void {
    this.gameService
      .deleteGame(this.game?.name as string)
      .pipe(tap(() => this.deleted.emit()))
      .subscribe();
  }
}
