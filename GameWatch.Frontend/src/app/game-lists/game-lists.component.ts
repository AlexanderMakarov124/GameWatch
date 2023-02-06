import { Component, OnInit } from '@angular/core';
import { GameListService } from './shared/game-list.service';
import { GameList } from './shared/game-list.model';
import { tap } from 'rxjs';

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
    const PLANNED = 'Planned';

    this.gameListService
      .getAllGameLists()
      .pipe(tap(gameLists => (this.selectedGameList = gameLists.find(gl => gl.name == PLANNED))))
      .subscribe(gameLists => (this.gameLists = gameLists));
  }

  onSelect(gameList: GameList): void {
    this.selectedGameList = gameList;
  }
}
