import { Component, OnInit } from '@angular/core';
import { PostsService } from '../../services/posts.service';
import { Post } from '../../models/post.model';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { PostCardComponent } from '../post-card/post-card.component';


@Component({
selector: 'app-feed',
templateUrl: './feed.component.html',
styleUrls: ['./feed.component.scss'],
  standalone: true,
 imports: [
    CommonModule,
    ReactiveFormsModule,
    MatToolbarModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    PostCardComponent
  ]
})
export class FeedComponent implements OnInit {
posts: Post[] = [];


constructor(private postsService: PostsService) {}


ngOnInit(): void {
this.load();
}


load() {
this.postsService.getAll().subscribe(p => this.posts = p);
}


onDeleted() {
this.load();
}
}