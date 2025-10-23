import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { PollListComponent } from './features/polls/poll-list/poll-list.component';
import { LoginComponent } from './features/auth/login/login.component';
import { CreatePollComponent} from './features/polls/create-poll/create-poll.component';
import { PollDetail } from './features/polls/poll-detail/poll-detail.component';
import { RegisterComponent } from './features/auth/register/register.component';


export const routes: Routes = [
  { path: '', component: HomeComponent },   
  { path: 'polls', component: PollListComponent },
  { path: 'login', component: LoginComponent },
  { path: 'polls/create', component: CreatePollComponent},
  { path: 'polls/:id', component: PollDetail },
  { path: 'register', component: RegisterComponent },

];

