import { NgModule } from '@angular/core';
import { RouterModule, Routes, UrlSegment } from '@angular/router';
import { DummyComponent } from './dummy/dummy.component';
import { CreateGameListComponent } from './game-lists/create-game-list/create-game-list.component';
import { GameListDetailComponent } from './game-lists/game-list-detail/game-list-detail.component';
import { GameListsComponent } from './game-lists/game-lists.component';
import { CreateGameComponent } from './games/create-game/create-game.component';
import { SearchGameComponent } from './games/search-game/search-game.component';
import { RandomGameComponent } from './games/random-game/random-game.component';
import { GameDetailComponent } from './games/game-detail/game-detail.component';

const routes: Routes = [
  { path: '', redirectTo: '/games', pathMatch: 'full' },
  { path: 'games', component: GameListsComponent },
  { path: 'games/create', component: CreateGameComponent },
  { path: 'games/search', component: SearchGameComponent },
  { path: 'games/random', component: RandomGameComponent },
  { path: 'games/:id', component: GameDetailComponent },
  { path: 'gameLists/create', component: CreateGameListComponent },
  { path: 'dummy', component: DummyComponent},
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
