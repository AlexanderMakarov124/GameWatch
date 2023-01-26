import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateGameListComponent } from './game-lists/create-game-list/create-game-list.component';
import { CreateGameComponent } from './games/create-game/create-game.component';
import { FindGameComponent } from './games/find-game/find-game.component';

const routes: Routes = [
  { path: 'games/create', component: CreateGameComponent },
  { path: 'gameLists/create', component: CreateGameListComponent },
  { path: 'games/find', component: FindGameComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
