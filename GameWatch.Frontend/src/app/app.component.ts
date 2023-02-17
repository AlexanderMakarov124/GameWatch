import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Game Watch';

  constructor(private router: Router) {}

  changeRoute(url: string) {
    this.router.navigateByUrl('/dummy', { skipLocationChange: true });
    setTimeout(() => this.router.navigateByUrl(url));
  }
}
