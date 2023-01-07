import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Game } from './game';

@Injectable({
    providedIn: 'root'
})
export class GamesService {

    private gamesUrl = 'api/games';

    httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };

    constructor(private http: HttpClient) { }

    /** GET all games from server */
    getAllGames(): Observable<Game[]> {
        return this.http.get<Game[]>(this.gamesUrl);
    }
}