import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LikesService {
  private apiUrl = 'https://localhost:7100/api/postlike';

  constructor(private http: HttpClient) {}

  likeOrDislikePost(postLike: any): Observable<any> {
    return this.http.post(this.apiUrl, postLike);
  }
}
