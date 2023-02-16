import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
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

  createGameList(gameList: GameList): Observable<GameList> {
    return this.http
      .post<GameList>(this.gameListsUrl, gameList, this.httpOptions)
      .pipe(catchError(this.handleError<GameList>('createGameList')));
  }

  getAllGameLists(): Observable<GameList[]> {
    return this.http
      .get<GameList[]>(this.gameListsUrl)
      .pipe(catchError(this.handleError<GameList[]>('getAllGameLists')));
  }

  getGameListById(id: number): Observable<GameList> {
    const url = `${this.gameListsUrl}/${id}`;

    return this.http.get<GameList>(url).pipe(catchError(this.handleError<GameList>('getGameListById')));
  }

  updateGameList(gameList: GameList): Observable<unknown> {
    return this.http
      .put(this.gameListsUrl, gameList, this.httpOptions)
      .pipe(catchError(this.handleError<GameList>('updateGameList')));
  }

  deleteGameList(id: number): Observable<GameList> {
    const url = `${this.gameListsUrl}/${id}`;

    return this.http
      .delete<GameList>(url, this.httpOptions)
      .pipe(catchError(this.handleError<GameList>('deleteGameList')));
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - Name of the operation that failed
   * @param result - Optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
