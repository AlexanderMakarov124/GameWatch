import { Component, Input, OnInit } from '@angular/core';
import { GameListService } from '../shared/game-list.service';
import { GameList } from '../shared/game-list.model';
import { GameService } from 'src/app/games/shared/game.service';

@Component({
  selector: 'app-game-list-detail',
  templateUrl: './game-list-detail.component.html',
  styleUrls: ['./game-list-detail.component.css'],
})
export class GameListDetailComponent implements OnInit {
  @Input() gameList?: GameList;

  constructor(private gameService: GameService, private gameListService: GameListService) {}

  ngOnInit(): void {}

  save(): void {
    if (this.gameList) {
      this.gameListService.updateGameList(this.gameList).subscribe();
    }
  }
}
