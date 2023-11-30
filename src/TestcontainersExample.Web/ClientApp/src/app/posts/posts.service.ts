import { inject, Injectable } from '@angular/core';
import { API_URL } from '../injectable-tokens';
import { HttpClient } from '@angular/common/http';
import { Post } from './post';

@Injectable({
  providedIn: 'root',
})
export class PostsService {
  apiUrl = inject(API_URL);
  http = inject(HttpClient);

  public readonly getPosts = () =>
    this.http.get<Post[]>(this.apiUrl + '/posts');

  public readonly addPost = (post: Post) =>
    this.http.post<Post>(this.apiUrl + '/posts', post);
}
