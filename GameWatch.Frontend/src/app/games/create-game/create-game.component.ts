import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { GameList } from 'src/app/game-lists/shared/game-list.model';
import { GameListService } from 'src/app/game-lists/shared/game-list.service';
import { Game } from '../shared/game.model';
import { GameService } from '../shared/game.service';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.css'],
})
export class CreateGameComponent implements OnInit {
  linkRegex = /http|^$/;

  createGameForm = this.formBuilder.group({
    name: ['', [Validators.required, Validators.maxLength(75), this.alreadyExistName()]],
    genre: ['', [Validators.required, Validators.maxLength(50)]],
    description: [''],
    gameListName: ['', [this.forbiddenNameValidator(/default/), Validators.required]],
    storeLink: ['', this.mustBeLinkValidator(this.linkRegex)],
    downloadLink: ['', this.mustBeLinkValidator(this.linkRegex)],
  });

  gameLists: GameList[] = [];
  games?: Game[];

  constructor(
    private gameService: GameService,
    private gameListService: GameListService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.getAllGameLists();
    this.getAllGames();
  }

  get name() {
    return this.createGameForm.get('name');
  }

  get genre() {
    return this.createGameForm.get('genre');
  }

  get gameListName() {
    return this.createGameForm.get('gameListName');
  }

  get storeLink() {
    return this.createGameForm.get('storeLink');
  }

  get downloadLink() {
    return this.createGameForm.get('downloadLink');
  }

  create(): void {
    const game = this.createGameForm.value as Game;

    game.name = game.name.trim();
    game.genre = game.genre.trim();
    game.description = game.description.trim();
    game.gameListName = game.gameListName.trim();
    game.storeLink = game.storeLink.trim();
    game.downloadLink = game.downloadLink.trim();

    this.gameService.createGame(game).subscribe();

    // this.games$?.pipe()
  }

  getAllGameLists(): void {
    this.gameListService.getAllGameLists().subscribe(gameLists => (this.gameLists = gameLists));
  }

  getAllGames(): void {
    this.gameService.getAllGames().subscribe(allGames => (this.games = allGames));
  }

  /** A name can't match the given regular expression */
  forbiddenNameValidator(nameRe: RegExp): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const forbidden = nameRe.test(control.value);
      return forbidden ? { forbiddenName: { value: control.value } } : null;
    };
  }
  alreadyExistName(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (control.value) {
        const game = this.games?.find(game => game.name.toLowerCase() == control.value.toLowerCase());

        return game ? { alreadyExistName: { value: control.value } } : null;
      }
      return null;
    };
  }

  /** Checks that string whether link or not. */
  mustBeLinkValidator(linkRe: RegExp): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const isLink = linkRe.test(control.value);
      return isLink ? null : { isLink: { value: control.value } };
    };
  }
}
