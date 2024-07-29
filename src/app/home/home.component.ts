import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { PostsService } from '../Services/posts.service';
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

  constructor(private postsService: PostsService) {}

  ngOnInit(): void {
    this.getPosts();
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
}
