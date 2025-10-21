import { Routes } from '@angular/router';
import { FeedComponent } from './components/feed/feed.component';
import { MyPostsComponent } from './components/my-posts/my-posts.component';
import { CreatePostComponent } from './components/create-post/create-post.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';

export const routes: Routes = [
  { path: '', component: FeedComponent },
  { path: 'my-posts', component: MyPostsComponent },
  { path: 'create', component: CreatePostComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: '' }
];