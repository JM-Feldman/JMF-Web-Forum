import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  private apiUrl = 'http://http://localhost:5206/api/posts';

  constructor(private http: HttpClient) {}

  getPosts(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/latest`);
  }

  searchPostsByTag(tags: string[]): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/search/byTag`, { params: { tags } });
  }

  searchPostsByAuthor(author: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/search/byAuthor`, { params: { author } });
  }

  searchPostsByTitle(title: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/search/byTitle`, { params: { title } });
  }

  searchPostsByContent(content: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/search/byContent`, { params: { content } });
  }
}

