import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateGameListComponent } from './create-game-list.component';

describe('CreateGameListComponent', () => {
  let component: CreateGameListComponent;
  let fixture: ComponentFixture<CreateGameListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateGameListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateGameListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
