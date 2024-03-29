import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon'; 

import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module';
import { CreateGameListComponent } from './game-lists/create-game-list/create-game-list.component';
import { GameListDetailComponent } from './game-lists/game-list-detail/game-list-detail.component';
import { GameListsComponent } from './game-lists/game-lists.component';
import { CreateGameComponent } from './games/create-game/create-game.component';
import { SearchGamesComponent } from './games/search-games/search-games.component';
import { GameDetailComponent } from './games/game-detail/game-detail.component';
import { GamesComponent } from './games/games.component';
import { RandomGameComponent } from './games/random-game/random-game.component';
import { DummyComponent } from './dummy/dummy.component';

@NgModule({
  declarations: [
    AppComponent,
    GameListsComponent,
    GameDetailComponent,
    CreateGameComponent,
    CreateGameListComponent,
    GameListDetailComponent,
    SearchGamesComponent,
    GamesComponent,
    RandomGameComponent,
    DummyComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
