import { Component, OnInit } from '@angular/core';
import { GameService } from './game.service';
import { Game } from './game';
import { filter, first, map, Observable, Subject, tap, toArray } from 'rxjs';
import { GameList } from './gameList';
import { GameListService } from './game-list.service';

@Component({
  selector: 'app-game-lists',
  templateUrl: './game-lists.component.html',
  styleUrls: ['./game-lists.component.css'],
})
export class GameListsComponent implements OnInit {
  // games$: Observable<Game[]> | undefined;
  // games$: Subject<Game[]> | undefined;

  gameLists: GameList[] = [];
  selectedGameList?: GameList;

  constructor(private gameService: GameService, private gameListService: GameListService) {}

  ngOnInit(): void {
    this.getAllGameLists();
  }

  onSelect(gameList: GameList): void {
    this.selectedGameList = gameList;
  }

  getAllGameLists(): void {
    this.gameListService.getAllGameLists().subscribe(gameLists => (this.gameLists = gameLists));
  }

  deleteGameList(gameList: GameList): void {
    this.gameLists = this.gameLists.filter(h => h !== gameList);
    // this.games$?.pipe(
    //   filter(h => h !== game)
    // )
    this.gameListService.deleteGameList(gameList.id).subscribe();
  }
}
