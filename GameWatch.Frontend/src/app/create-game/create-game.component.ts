import { Component, OnInit } from '@angular/core';
import { GameService } from '../game-lists/game.service';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Game } from '../game-lists/game';
import { GameList } from '../game-lists/gameList';
import { GameListService } from '../game-lists/game-list.service';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.css'],
})
export class CreateGameComponent implements OnInit {
  //   createGameForm = new FormGroup({
  //    name: new FormControl(''),
  //    genre: new FormControl(''),
  //    description: new FormControl('')
  //   });

  createGameForm = this.formBuilder.group({
    name: ['', Validators.required],
    genre: ['', Validators.required],
    description: [''],
    gameListName: [''],
  });

  gameLists: GameList[] = [];

  constructor(
    private gameService: GameService,
    private gameListService: GameListService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getAllGameLists();
  }

  create(): void {
    const game: Game = this.createGameForm.value as Game;

    game.name = game.name.trim();
    game.genre = game.genre.trim();
    game.description = game.description.trim();
    game.gameListName = game.gameListName.trim();

    this.gameService.createGame(game).subscribe();

    // this.games$?.pipe()
  }

  getAllGameLists(): void {
    this.gameListService.getAllGameLists().subscribe(gameLists => (this.gameLists = gameLists));
  }
}
