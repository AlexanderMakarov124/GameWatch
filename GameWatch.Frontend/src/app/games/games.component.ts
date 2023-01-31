import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Game } from './shared/game.model';
import { GameService } from './shared/game.service';

@Component({
  selector: 'app-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.css'],
})
export class GamesComponent implements OnInit {
  @Input() games?: Game[];
  selectedGame?: Game;
  dataSource!: MatTableDataSource<Game>;
  @ViewChild(MatSort) sort!: MatSort;

  displayedColumns = ['id', 'name', 'genre', 'createdAt'];

  constructor(private gameService: GameService) {}

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource(this.games);
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  onSelect(game: Game): void {
    this.selectedGame = game;
  }

  deleteGame(game: Game): void {
    this.games = this.games?.filter(g => g !== game);
    // this.games$?.pipe(
    //   filter(h => h !== game)
    // )
    this.gameService.deleteGame(game.name).subscribe();
  }
}
