import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GameListDetailComponent } from './game-list-detail.component';

describe('GameListDetailComponent', () => {
  let component: GameListDetailComponent;
  let fixture: ComponentFixture<GameListDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GameListDetailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GameListDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
