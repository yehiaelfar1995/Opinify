import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthStateService {
  private loggedIn = new BehaviorSubject<boolean>(!!localStorage.getItem('authToken'));
  isLoggedIn$ = this.loggedIn.asObservable();

  setLogin(token: string) {
    localStorage.setItem('authToken', token);
    this.loggedIn.next(true);
  }

  logout() {
    localStorage.removeItem('authToken');
    this.loggedIn.next(false);
  }
}
