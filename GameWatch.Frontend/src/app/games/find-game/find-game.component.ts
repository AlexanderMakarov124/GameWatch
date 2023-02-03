import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Game } from '../shared/game.model';
import { GameService } from '../shared/game.service';

@Component({
  selector: 'app-find-game',
  templateUrl: './find-game.component.html',
  styleUrls: ['./find-game.component.css'],
})
export class FindGameComponent implements OnInit {
  games: Game[] = [];
  filter?: string;

  findForm = this.formBuilder.group({
    name: ['', Validators.required],
  });

  constructor(private gameService: GameService, private formBuilder: FormBuilder) {}

  ngOnInit(): void {}

  getGamesByName(): void {
    let name: string = this.findForm.value.name as string;
    name = name.trim();

    this.gameService.getAllGames().subscribe(games => (this.games = games));
    this.filter = name;
  }
}
