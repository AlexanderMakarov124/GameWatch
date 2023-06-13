import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Game } from '../shared/game.model';
import { GameService } from '../shared/game.service';

@Component({
  selector: 'app-search-games',
  templateUrl: './search-games.component.html',
  styleUrls: ['./search-games.component.css'],
})
export class SearchGamesComponent implements OnInit {
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

    this.gameService.searchGames().subscribe(games => (this.games = games));
    this.filter = name.toLowerCase();
  }
}
