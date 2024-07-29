import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  private apiUrl = 'http://your-api-url/api/posts'; // Replace with your API URL

  constructor(private http: HttpClient) {}

  getPosts(): Observable<any> {
    return this.http.get(`${this.apiUrl}/latest`);
  }

  searchPostsByTag(tags: string[]): Observable<any> {
    return this.http.get(`${this.apiUrl}/search/byTag`, { params: { tags: tags.join(',') } });
  }

  searchPostsByAuthor(author: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/search/byAuthor`, { params: { author } });
  }

  searchPostsByTitle(title: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/search/byTitle`, { params: { title } });
  }

  searchPostsByContent(content: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/search/byContent`, { params: { content } });
  }

  createPost(post: any): Observable<any> {
    return this.http.post(this.apiUrl, post);
  }
}
