import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { GameListService } from '../game-lists/game-list.service';
import { GameList } from '../game-lists/gameList';

@Component({
  selector: 'app-create-game-list',
  templateUrl: './create-game-list.component.html',
  styleUrls: ['./create-game-list.component.css'],
})
export class CreateGameListComponent implements OnInit {
  createGameListForm = this.formBuilder.group({
    name: ['', Validators.required],
  });

  constructor(private formBuilder: FormBuilder, private gameListService: GameListService) {}

  ngOnInit(): void {}

  create() {
    const gameList: GameList = this.createGameListForm.value as GameList;
    gameList.name = gameList.name.trim();

    this.gameListService.createGameList(gameList).subscribe();
  }
}
