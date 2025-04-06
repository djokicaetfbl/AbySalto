import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './nav/nav.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  // http = inject(HttpClient);
  title = 'client';
  //products: any[] = [];

  ngOnInit(): void {
    // this.http.get('https://dummyjson.com/products').subscribe((data: any) => {
    //   console.log(data);
    // });
  }
}
