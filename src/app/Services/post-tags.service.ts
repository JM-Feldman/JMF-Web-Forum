import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostTagsService {
  private apiUrl = 'http://your-api-url/api/posttags'; // Replace with your API URL

  constructor(private http: HttpClient) {}

  addTagsToPost(postId: number, tags: string[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/add`, { postId, tags });
  }

  removeTagsFromPost(postId: number, tags: string[]): Observable<any> {
    return this.http.post(`${this.apiUrl}/remove`, { postId, tags });
  }
}
