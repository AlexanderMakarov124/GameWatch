import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { GameListService } from '../shared/game-list.service';
import { GameList } from '../shared/game-list.model';
import { catchError, tap } from 'rxjs';

@Component({
  selector: 'app-create-game-list',
  templateUrl: './create-game-list.component.html',
  styleUrls: ['./create-game-list.component.css'],
})
export class CreateGameListComponent implements OnInit {
  createGameListForm = this.formBuilder.group({
    name: ['', Validators.required],
  });

  result?: string;

  constructor(private formBuilder: FormBuilder, private gameListService: GameListService) {}

  ngOnInit(): void {}

  create() {
    const gameList: GameList = this.createGameListForm.value as GameList;
    gameList.name = gameList.name.trim();

    this.gameListService
      .createGameList(gameList)
      .pipe(
        tap(() => {
          this.result = `The game list ${gameList.name} was sucessfully created.`;
        }),
        catchError(error => (this.result = `The error was enctountered: ${error}`))
      )
      .subscribe();
  }
}
