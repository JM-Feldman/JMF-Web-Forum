import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { PostsService } from '../Services/posts.service';
import { CommentsService } from '../Services/comments.service';
import { PostTagsService } from '../Services/post-tags.service';
import { LikesService } from '../Services/likes.service';
import jwt_decode from 'jwt-decode';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  posts: any[] = [];
  searchCriteria = {
    tag: '',
    author: '',
    title: '',
    content: ''
  };
  commentContent: string = '';
  newPost = {
    title: '',
    content: '',
    tags: ''
  };
  userId: number | null = null;
  isLoggedIn: boolean = false;

  constructor(
    private postsService: PostsService,
    private commentsService: CommentsService,
    private postTagsService: PostTagsService,
    private likesService: LikesService
  ) {}

  ngOnInit(): void {
    this.getPosts();
    this.checkLoginStatus();
  }

  getPosts() {
    this.postsService.getPosts().subscribe(
      data => {
        this.posts = data;
      },
      error => {
        console.error(error);
      }
    );
  }

  searchByTag() {
    this.postsService.searchPostsByTag([this.searchCriteria.tag]).subscribe(
      data => {
        this.posts = data;
      },
      error => {
        console.error(error);
      }
    );
  }

  searchByAuthor() {
    this.postsService.searchPostsByAuthor(this.searchCriteria.author).subscribe(
      data => {
        this.posts = data;
      },
      error => {
        console.error(error);
      }
    );
  }

  searchByTitle() {
    this.postsService.searchPostsByTitle(this.searchCriteria.title).subscribe(
      data => {
        this.posts = data;
      },
      error => {
        console.error(error);
      }
    );
  }

  searchByContent() {
    this.postsService.searchPostsByContent(this.searchCriteria.content).subscribe(
      data => {
        this.posts = data;
      },
      error => {
        console.error(error);
      }
    );
  }

  addComment(postId: number) {
    if (!this.userId) return;

    const comment = {
      content: this.commentContent,
      postId: postId,
      userId: this.userId
    };

    this.commentsService.addComment(comment).subscribe(
      response => {
        console.log('Comment added:', response);
        this.commentContent = '';
        this.getPosts();
      },
      error => {
        console.error('Error adding comment:', error);
      }
    );
  }

  likeOrDislikePost(postId: number, isLike: boolean) {
    if (!this.userId) return;

    const postLike = {
      postId: postId,
      userId: this.userId,
      isLike: isLike
    };

    this.likesService.likeOrDislikePost(postLike).subscribe(
      response => {
        console.log('Post liked/disliked:', response);
        this.getPosts();
      },
      error => {
        console.error('Error liking/disliking post:', error);
      }
    );
  }

  addTags(postId: number) {
    if (!this.userId) return;

    const tags = this.newPost.tags.split(',').map(tag => tag.trim());

    this.postTagsService.addTagsToPost(postId, tags).subscribe(
      response => {
        console.log('Tags added:', response);
        this.newPost.tags = '';
        this.getPosts();
      },
      error => {
        console.error('Error adding tags:', error);
      }
    );
  }

  createPost() {
    if (!this.userId) return;

    const post = {
      title: this.newPost.title,
      content: this.newPost.content,
      userId: this.userId,
      tags: this.newPost.tags.split(',').map(tag => tag.trim())
    };

    this.postsService.createPost(post).subscribe(
      response => {
        console.log('Post created:', response);
        this.newPost = { title: '', content: '', tags: '' };
        this.getPosts();
      },
      error => {
        console.error('Error creating post:', error);
      }
    );
  }

  checkLoginStatus() {
    const token = localStorage.getItem('token');
    if (token) {
      try {
        const decoded: any = jwt_decode(token);
        this.userId = decoded.userId;
        this.isLoggedIn = true;
      } catch (error) {
        console.error('Invalid token:', error);
        this.isLoggedIn = false;
      }
    } else {
      this.isLoggedIn = false;
    }
  }
}

