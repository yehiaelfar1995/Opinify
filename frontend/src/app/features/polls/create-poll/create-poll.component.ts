import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { PollService } from '../../../core/services/poll.service';
import { CreatePollDto } from '../../../core/models/Poll';

@Component({
  selector: 'app-create-poll',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-poll.component.html',
  styleUrls: ['./create-poll.component.css']
})
export class CreatePollComponent {
  title = '';
  isPublic = true;
  questions = [
    { questionText: '', options: ['', ''] }
  ];

  constructor(private pollService: PollService, private router: Router) {}

  addQuestion() {
    this.questions.push({ questionText: '', options: ['', ''] });
  }

  removeQuestion(index: number) {
    if (this.questions.length > 1) {
      this.questions.splice(index, 1);
    }
  }

  addOption(qIndex: number) {
    this.questions[qIndex].options.push('');
  }

  removeOption(qIndex: number, oIndex: number) {
    if (this.questions[qIndex].options.length > 2) {
      this.questions[qIndex].options.splice(oIndex, 1);
    }
  }

  updateOption(qIndex: number, oIndex: number, value: string) {
    this.questions[qIndex].options[oIndex] = value;
  }

  trackByIndex(index: number): number {
    return index;
  }

  createPoll() {
    if (!this.title || this.questions.some(q => !q.questionText || q.options.some(o => !o))) {
      return;
    }

    const poll: CreatePollDto = {
      title: this.title,
      isPublic: this.isPublic,
      questions: this.questions
    };

    this.pollService.createPoll(poll).subscribe({
      next: () => this.router.navigate(['/']),
      error: err => console.error('Create poll failed', err)
    });
  }
}
