import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';

import { AppRoutingModule } from './app-routing.module';
import { CreateGameListComponent } from './game-lists/create-game-list/create-game-list.component';
import { GameListDetailComponent } from './game-lists/game-list-detail/game-list-detail.component';
import { GameListsComponent } from './game-lists/game-lists.component';
import { CreateGameComponent } from './games/create-game/create-game.component';
import { FindGameComponent } from './games/find-game/find-game.component';
import { GameDetailComponent } from './games/game-detail/game-detail.component';
import { GamesComponent } from './games/games.component';

@NgModule({
  declarations: [
    AppComponent,
    GameListsComponent,
    GameDetailComponent,
    CreateGameComponent,
    CreateGameListComponent,
    GameListDetailComponent,
    FindGameComponent,
    GamesComponent,
  ],
  imports: [BrowserModule, HttpClientModule, FormsModule, AppRoutingModule, ReactiveFormsModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
