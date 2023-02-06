import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Game } from './shared/game.model';

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
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @Input() filter?: string;

  displayedColumns = ['id', 'name', 'genre', 'createdAt'];

  constructor() {}

  ngOnChanges(): void {
    this.dataSource = new MatTableDataSource(this.games);
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    if (this.filter != undefined) {
      this.dataSource.filter = this.filter;
    }
  }

  ngOnInit(): void {}

  onSelect(game: Game): void {
    this.selectedGame = game;
  }
}
