import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { GameListsComponent } from './game-lists/game-lists.component';
import { GameDetailComponent } from './game-detail/game-detail.component';
import { AppRoutingModule } from './app-routing.module';
import { CreateGameComponent } from './create-game/create-game.component';
import { CreateGameListComponent } from './create-game-list/create-game-list.component';
import { GameListDetailComponent } from './game-list-detail/game-list-detail.component';
import { FindGameComponent } from './find-game/find-game.component';
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
