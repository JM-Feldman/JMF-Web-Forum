import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostTagsService {
  private apiUrl = 'http://http://localhost:5206/api/posttags';

  constructor(private http: HttpClient) {}

  addTagsToPost(postId: number, tags: string[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, { postId, tags });
  }

  removeTagsFromPost(postId: number, tags: string[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/remove`, { postId, tags });
  }
}
