import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Game } from '../shared/game.model';
import { GameService } from '../shared/game.service';
import { Observable, switchMap, tap } from 'rxjs';
import { GameList } from 'src/app/game-lists/shared/game-list.model';
import { GameListService } from 'src/app/game-lists/shared/game-list.service';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

@Component({
  selector: 'app-game-detail',
  templateUrl: './game-detail.component.html',
  styleUrls: ['./game-detail.component.css'],
})
export class GameDetailComponent implements OnInit {
  game$!: Observable<Game>;
  @Output() deleted = new EventEmitter();
  gameLists: GameList[] = [];

  constructor(  
    private route: ActivatedRoute,
    private router: Router,
    private gameService: GameService, 
    private gameListService: GameListService) {}

  ngOnInit(): void {

    this.game$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.gameService.getGameById(Number(params.get('id'))))
    );

    this.gameListService.getAllGameLists().subscribe(result => this.gameLists = result);
  }

  save(game: Game): void {
    if (this.game$) {
      this.gameService.updateGame(game).subscribe();
    }
  }

  delete(game: Game): void {
    this.gameService
      .deleteGame(game.name as string)
      .pipe(tap(_ => this.router.navigate(['/games'])))
      .subscribe();      
  }
}
