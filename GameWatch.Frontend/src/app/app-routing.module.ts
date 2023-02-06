import { NgModule } from '@angular/core';
import { RouterModule, Routes, UrlSegment } from '@angular/router';
import { CreateGameListComponent } from './game-lists/create-game-list/create-game-list.component';
import { GameListDetailComponent } from './game-lists/game-list-detail/game-list-detail.component';
import { GameListsComponent } from './game-lists/game-lists.component';
import { CreateGameComponent } from './games/create-game/create-game.component';
import { FindGameComponent } from './games/find-game/find-game.component';
import { RandomGameComponent } from './games/random-game/random-game.component';

const routes: Routes = [
  { path: '', component: GameListsComponent },
  { path: 'games/create', component: CreateGameComponent },
  { path: 'games/find', component: FindGameComponent },
  { path: 'games/random', component: RandomGameComponent },
  { path: 'gameLists/create', component: CreateGameListComponent },
  {
    matcher: url => {
      if (url.length === 2 && url[0].path == 'gameLists' && url[1].path.match(/\d/gm)) {
        return {
          consumed: url,
          posParams: {
            id: new UrlSegment(url[1].path, {}),
          },
        };
      }

      return null;
    },
    component: GameListDetailComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
