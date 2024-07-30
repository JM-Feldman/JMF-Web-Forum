import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostTagsService {
  private apiUrl = 'https://localhost:7100/api/posttags';

  constructor(private http: HttpClient) {}

  addTagsToPost(postId: number, tags: string[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, { postId, tags });
  }

  removeTagsFromPost(postId: number, tags: string[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/remove`, { postId, tags });
  }
}
