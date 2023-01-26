import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { GameList } from './game-list.model';

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

  updateGameList(gameList: GameList): Observable<unknown> {
    return this.http.put(this.gameListsUrl, gameList, this.httpOptions);
  }

  deleteGameList(id: number): Observable<GameList> {
    const url = `${this.gameListsUrl}/${id}`;

    return this.http.delete<GameList>(url, this.httpOptions);
  }
}
