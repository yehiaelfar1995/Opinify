export interface Answer {
  answerId: number;
  text: string;
  votes: number;
}

export interface Question {
  questionId: number;
  text: string;
  answers: Answer[];
}

export interface Poll {
  pollId: number;
  title: string;
  isPublic: boolean;
  createdAt: string;   // ISO string from backend
  questions: Question[];
}


export interface CreatePollDto {
  title: string;
  isPublic: boolean;
  questions: CreateQuestionDto[];
}

export interface CreateQuestionDto {
  questionText: string;
  options: string[];
}

// models/get-my-poll.dto.ts
export interface GetMyPollDto {
  pollId: number;
  title: string;
  isPublic: boolean;
  createdAt: string;
}
