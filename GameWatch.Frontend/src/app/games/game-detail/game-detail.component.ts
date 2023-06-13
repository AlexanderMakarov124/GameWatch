import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Game } from '../shared/game.model';
import { GameService } from '../shared/game.service';
import { tap } from 'rxjs';
import { GameList } from 'src/app/game-lists/shared/game-list.model';
import { GameListService } from 'src/app/game-lists/shared/game-list.service';

@Component({
  selector: 'app-game-detail',
  templateUrl: './game-detail.component.html',
  styleUrls: ['./game-detail.component.css'],
})
export class GameDetailComponent implements OnInit {
  @Input() game?: Game;
  @Output() deleted = new EventEmitter();
  gameLists: GameList[] = [];

  constructor(private gameService: GameService, private gameListService: GameListService) {}

  ngOnInit(): void {
    this.gameListService.getAllGameLists().subscribe(result => this.gameLists = result);
  }

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
