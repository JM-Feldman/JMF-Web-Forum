import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  private apiUrl = 'https://localhost:7100/api/posts';

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

  createPost(post: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, post);
  }
}


