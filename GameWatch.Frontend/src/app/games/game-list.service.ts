import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GameList } from './gameList';

@Injectable({
  providedIn: 'root',
})
export class GameListService {
  private gameListsUrl = 'api/gameLists';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private http: HttpClient) {}

  getAllGameLists(): Observable<GameList[]> {
    return this.http.get<GameList[]>(this.gameListsUrl);
  }

  createGameList(gameList: GameList): Observable<GameList> {
    return this.http.post<GameList>(this.gameListsUrl, gameList, this.httpOptions);
  }
}
