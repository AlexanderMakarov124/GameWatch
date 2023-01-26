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
   * POST: Creates the game.
   *
   * @param game - Game to create.
   */
  createGame(game: Game): Observable<Game> {
    return this.http
      .post<Game>(this.gamesUrl, game, this.httpOptions)
      .pipe(catchError(this.handleError<Game>('createGame')));
  }

  /**
   * GET games by name.
   *
   * @param name - Name to find.
   * @returns Games.
   */
  getGamesByName(name: string): Observable<Game[]> {
    const url = `${this.gamesUrl}/${name}`;

    return this.http
      .get<Game[]>(url)
      .pipe(catchError(this.handleError<Game[]>(`getGamesByName name=${name}`)));
  }

  /**
   * PUT: Updates the game on the server.
   *
   * @param game - Game to update.
   */
  updateGame(game: Game): Observable<any> {
    return this.http
      .put(this.gamesUrl, game, this.httpOptions)
      .pipe(catchError(this.handleError<any>('updateHero')));
  }

  /**
   *  DELETE: delete the hero from the server
   *
   *  @param name - Name of the game to delete.
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