import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { GameListService } from '../shared/game-list.service';
import { GameList } from '../shared/game-list.model';

@Component({
  selector: 'app-game-list-detail',
  templateUrl: './game-list-detail.component.html',
  styleUrls: ['./game-list-detail.component.css'],
})
export class GameListDetailComponent implements OnInit {
  @Input() gameList?: GameList;
  @Output() deleted = new EventEmitter<GameList>();

  constructor(private gameListService: GameListService) {}

  ngOnInit(): void {}

  save(): void {
    if (this.gameList) {
      this.gameListService.updateGameList(this.gameList).subscribe();
    }
  }

  delete(): void {
    this.gameListService.deleteGameList(this.gameList?.id as number).subscribe();
    this.deleted.emit(this.gameList);
  }
}
