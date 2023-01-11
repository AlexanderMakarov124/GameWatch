import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, Observable, of } from 'rxjs';

import { Game } from './game';

@Injectable({
  providedIn: 'root',
})
export class GameService {
  private gamesUrl = 'api/games';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  constructor(private http: HttpClient) {}

  /**
   * GET all games from server.
   */
  getAllGames(): Observable<Game[]> {
    return this.http
      .get<Game[]>(this.gamesUrl)
      .pipe(catchError(this.handleError<Game[]>('getAllGames', [])));
  }

  /**
   * GET a game by name.
   *
   * @param name Game name.
   * @returns Game.
   */
  getGameByName(name: string): Observable<Game> {
    const gameUrl = `${this.gamesUrl}/${name}`;

    return this.http
      .get<Game>(gameUrl)
      .pipe(catchError(this.handleError<Game>(`getGameByName name=${name}`)));
  }

  /**
   * POST: Creates the game.
   *
   * @param game Game to create.
   */
  createGame(game: Game): Observable<Game> {
    return this.http
      .post<Game>(this.gamesUrl, game, this.httpOptions)
      .pipe(catchError(this.handleError<Game>('createGame')));
  }

  /**
   * PUT: Updates the game on the server.
   *
   * @param game Game to update.
   */
  updateGame(game: Game): Observable<any> {
    return this.http
      .put(this.gamesUrl, game, this.httpOptions)
      .pipe(catchError(this.handleError<any>('updateHero')));
  }

  /**
   *  DELETE: delete the hero from the server
   *
   *  @param name Name of the game to delete.
   */
  deleteGame(name: string): Observable<Game> {
    const url = `${this.gamesUrl}/${name}`;

    return this.http
      .delete<Game>(url, this.httpOptions)
      .pipe(catchError(this.handleError<Game>('deleteHero')));
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
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
