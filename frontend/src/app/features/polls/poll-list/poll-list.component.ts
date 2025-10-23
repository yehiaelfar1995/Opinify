import { Component } from '@angular/core';
import { GetMyPollDto } from '../../../core/models/Poll'
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PollService } from 'app/core/services/poll.service';
@Component({
  selector: 'app-poll-list',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './poll-list.component.html',
  styleUrls: ['./poll-list.component.css']
})
export class PollListComponent {

  myPolls: GetMyPollDto[] = [];
  loading = true;
  errorMessage = '';

  constructor(private router: Router,private pollService: PollService) {}

  ngOnInit(): void {
    // TODO: Replace with API call
    this.myPolls = [
      { pollId: 1, title: 'Favorite Programming Language?', isPublic: true, createdAt: '2025-09-01T12:00:00' },
      { pollId: 2, title: 'Best Framework in 2025?', isPublic: false, createdAt: '2025-09-05T15:30:00' }
    ];
    this.loadPolls();
  }

  loadPolls() {
    this.pollService.getPolls().subscribe({
      next: (polls) => {
        this.myPolls = polls;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading polls', err);
        this.errorMessage = 'Failed to load your polls. Please try again later.';
        this.loading = false;
      }
    });
  }

  viewPoll(poll: any) {
    this.router.navigate(['/polls', poll.pollId]);
  }

}