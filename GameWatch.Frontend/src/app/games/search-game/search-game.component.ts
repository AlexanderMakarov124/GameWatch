import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Game } from '../shared/game.model';
import { GameService } from '../shared/game.service';

@Component({
  selector: 'app-search-game',
  templateUrl: './search-game.component.html',
  styleUrls: ['./search-game.component.css'],
})
export class SearchGameComponent implements OnInit {
  games: Game[] = [];
  filter?: string;

  searchForm = this.formBuilder.group({
    name: ['', Validators.required],
  });

  constructor(private gameService: GameService, private formBuilder: FormBuilder) {}

  ngOnInit(): void {}

  searchGamesByName(): void {
    let name: string = this.searchForm.value.name as string;
    name = name.trim();

    this.gameService.searchGames(name).subscribe(games => (this.games = games));
    this.filter = name;
  }
}
