import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { GameListService } from '../shared/game-list.service';
import { GameList } from '../shared/game-list.model';
import { tap } from 'rxjs';

@Component({
  selector: 'app-game-list-detail',
  templateUrl: './game-list-detail.component.html',
  styleUrls: ['./game-list-detail.component.css'],
})
export class GameListDetailComponent implements OnInit {
  @Input() gameList?: GameList;

  constructor(
    private gameListService: GameListService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.pipe(
      map((params: ParamMap) => {
        const id: number | null = Number(params.get('id'));
        this.gameListService.getGameListById(id).subscribe(gameList => (this.gameList = gameList));
      })
    ).subscribe();
  }

  save(): void {
    if (this.gameList) {
      this.gameListService.updateGameList(this.gameList).subscribe();
    }
  }

  delete(): void {
    this.gameListService
      .deleteGameList(this.gameList?.id as number)
      .pipe(tap(_ => this.router.navigateByUrl('')))
      .subscribe();
  }
}
