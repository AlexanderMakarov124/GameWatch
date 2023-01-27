import { Component, OnInit } from '@angular/core';
import { GameListService } from './shared/game-list.service';
import { GameList } from './shared/game-list.model';

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

  constructor(private gameListService: GameListService) {}

  ngOnInit(): void {
    this.gameListService.getAllGameLists().subscribe(gameLists => (this.gameLists = gameLists));
  }

  onSelect(gameList: GameList): void {
    this.selectedGameList = gameList;
  }

  deleteGameList(gameList: GameList): void {
    this.gameLists = this.gameLists.filter(h => h !== gameList);
    // this.games$?.pipe(
    //   filter(h => h !== game)
    // )
    this.gameListService.deleteGameList(gameList.id).subscribe();
  }
}
