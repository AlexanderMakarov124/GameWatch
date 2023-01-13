import { Component, OnInit } from '@angular/core';
import { GameService } from '../games/game.service';
import {
   FormControl,
   FormGroup,
   FormBuilder,
   Validators,
 } from '@angular/forms';
import { Game } from '../games/game';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.css']
})
export class CreateGameComponent implements OnInit {
   

  //   createGameForm = new FormGroup({
  //    name: new FormControl(''),
  //    genre: new FormControl(''),
  //    description: new FormControl('')
  //   });

  createGameForm = this.formBuilder.group({
   name: ['', Validators.required],
   genre: ['', Validators.required],
   description: [''],
 });

  constructor(
   private gameService: GameService,
   private formBuilder: FormBuilder
   ) { }

  ngOnInit(): void {
  }

  

  create(): void {
   const game: Game = this.createGameForm.value as Game;

   game.name = game.name.trim();
   game.genre = game.genre.trim();
   game.description = game.description.trim();
   game.createdAt = new Date();

   this.gameService
     .createGame(game)
     .subscribe();
     
   // this.games$?.pipe()
 }

}
