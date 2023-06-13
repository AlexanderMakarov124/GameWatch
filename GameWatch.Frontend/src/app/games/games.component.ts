import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
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
  @Output() deleted = new EventEmitter();

  displayedColumns = ['name', 'genres', 'createdAt'];

  constructor() {}

  ngOnChanges(): void {
    const NAME_LENGTH = 25;
    this.games?.map(g => {
      if (g.name.length > NAME_LENGTH) {
        g.name = g.name.slice(0, NAME_LENGTH) + '...';
      }
      return g;
    });
    this.dataSource = new MatTableDataSource(this.games);
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    if (this.filter != undefined) {
      this.dataSource.filterPredicate = function(record, filter){
        const searchResult = record.genres.find(genre => genre.name.trim().toLowerCase().includes(filter)) != undefined
        || record.name.trim().toLowerCase().includes(filter)
        || record.createdAt.toString().includes(filter);
        return searchResult
      }
      this.dataSource.filter = this.filter.trim().toLowerCase();
    }
    
    this.selectedGame = undefined;
  }
  
  ngOnInit(): void {
    this.ngOnChanges()
  }

  onSelect(game: Game): void {
    this.selectedGame = game;
  }
}
