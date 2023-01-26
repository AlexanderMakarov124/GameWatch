import { Component, Input, OnInit } from '@angular/core';
import { Game } from '../game-lists/game';
import { GameListService } from '../game-lists/game-list.service';
import { GameService } from '../game-lists/game.service';
import { GameList } from '../game-lists/gameList';

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
