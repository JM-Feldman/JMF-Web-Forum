import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {
  private apiUrl = 'https://localhost:7100/api/comments'; 

  constructor(private http: HttpClient) {}

  addComment(comment: any): Observable<any> {
    return this.http.post(this.apiUrl, comment);
  }

  getCommentById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }
}

