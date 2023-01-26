import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Game } from '../game-lists/game';
import { GameService } from '../game-lists/game.service';

@Component({
  selector: 'app-find-game',
  templateUrl: './find-game.component.html',
  styleUrls: ['./find-game.component.css'],
})
export class FindGameComponent implements OnInit {
  games: Game[] = [];

  findForm = this.formBuilder.group({
    name: ['', Validators.required]
  });

  constructor(private gameService: GameService, private formBuilder: FormBuilder) {}

  ngOnInit(): void {}

  getGamesByName(): void {
    let name: string = this.findForm.value.name as string;
    name = name.trim();

    this.gameService.getGamesByName(name).subscribe(games => (this.games = games));
  }
}
