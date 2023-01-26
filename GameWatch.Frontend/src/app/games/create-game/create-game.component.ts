import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { GameList } from 'src/app/game-lists/shared/game-list.model';
import { GameListService } from 'src/app/game-lists/shared/game-list.service';
import { Game } from '../shared/game.model';
import { GameService } from '../shared/game.service';

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
