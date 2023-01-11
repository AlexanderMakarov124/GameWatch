import { Component, OnInit } from '@angular/core';
import { GameService } from './game.service';
import { Game } from './game';
import { filter, first, map, Observable, Subject, tap, toArray } from 'rxjs';
import {
  FormControl,
  FormGroup,
  FormBuilder,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css'],
})
export class GamesComponent implements OnInit {
  // games$: Observable<Game[]> | undefined;
  // games$: Subject<Game[]> | undefined;
  games: Game[] = [];
  selectedGame?: Game;

  //   createGameForm = new FormGroup({
  //    name: new FormControl(''),
  //    genre: new FormControl(''),
  //    description: new FormControl('')
  //   });

  createGameForm = this.formBuilder.group({
    name: ['', Validators.required],
    genre: ['', Validators.required],
    description: [''],
  });

  constructor(
    private gameService: GameService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getAllGames();
  }

  onSelect(game: Game): void {
    this.selectedGame = game;
  }

  getAllGames(): void {
    this.gameService.getAllGames().subscribe(games => (this.games = games));
    // this.games$ = this.gameService.getAllGames().pipe();
  }

  create(): void {
    const game: Game = this.createGameForm.value as Game;

    game.name = game.name.trim();
    game.genre = game.genre.trim();
    game.description = game.description.trim();
    game.createdAt = new Date();

    this.gameService
      .createGame(game)
      .subscribe(game => {
        this.games.push(game);

        this.ngOnInit();
      });
    // this.games$?.pipe()
  }

  delete(game: Game): void {
    this.games = this.games.filter(h => h !== game);
    // this.games$?.pipe(
    //   filter(h => h !== game)
    // )
    this.gameService.deleteGame(game.name).subscribe();
  }
}
