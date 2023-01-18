import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateGameListComponent } from './create-game-list/create-game-list.component';
import { CreateGameComponent } from './create-game/create-game.component';

const routes: Routes = [
  { path: 'create/game', component: CreateGameComponent },
  { path: 'create/gameList', component: CreateGameListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
