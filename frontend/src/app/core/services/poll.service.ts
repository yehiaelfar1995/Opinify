import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreatePollDto, Poll, GetMyPollDto } from '../models/Poll';

@Injectable({
  providedIn: 'root'
})
export class PollService {
  private apiUrl = 'https://localhost:44325/api/Poll'; // adjust your backend URL

  constructor(private http: HttpClient) {}

  createPoll(poll: CreatePollDto): Observable<Poll> {
    return this.http.post<Poll>(`${this.apiUrl}/create`, poll, { withCredentials: true });
  }

  getPolls(): Observable<GetMyPollDto[]> {
    return this.http.get<GetMyPollDto[]>(`${this.apiUrl}/get`, { withCredentials: true });
  }
  vote(data: { answerId: number; anonymousId?: string }): Observable<any> {
    return this.http.post(`${this.apiUrl}/vote`, data);
  }
  getPollDetails(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

}
