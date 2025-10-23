import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PollService } from '../../../core/services/poll.service';
import { Poll, Question, Answer } from '../../../core/models/Poll';

@Component({
  selector: 'app-poll-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './poll-detail.component.html',
  styleUrls: ['./poll-detail.component.css']
})
export class PollDetail implements OnInit {
  poll?: Poll;
  currentQuestionIndex = 0;
  message = '';

  constructor(private route: ActivatedRoute, private pollService: PollService) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.pollService.getPollDetails(id).subscribe({
      next: (poll) => this.poll = poll,
      error: () => this.message = "Failed to load poll"
    });
  }

  get currentQuestion(): Question | undefined {
    return this.poll?.questions[this.currentQuestionIndex];
  }

  vote(answer: Answer) {
    const anonymousId = localStorage.getItem('anonymousId') ?? undefined;
    this.pollService.vote({ answerId: answer.answerId, anonymousId }).subscribe({
      next: (res) => {
        this.message = res.message;

        // Move to next question if available
        if (this.poll && this.currentQuestionIndex < this.poll.questions.length - 1) {
          this.currentQuestionIndex++;
        } else {
          this.message = "Thank you for completing the poll!";
        }
      },
      error: (err) => {
        this.message = err.error.message || "Vote failed";
      }
    });
  }}
