import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Post } from '../models/post.model';
import { PostDto } from '../models/post-dto.model';
import { environment } from '../../enviorments/enviorment';


@Injectable({ providedIn: 'root' })
export class PostsService {
private base = `${environment.apiUrl}/Posts`;


constructor(private http: HttpClient) {}


getAll(): Observable<Post[]> {
return this.http.get<Post[]>(this.base);
}


getByUsername(username: string): Observable<Post[]> {
return this.http.get<Post[]>(`${this.base}/${encodeURIComponent(username)}`);
}


create(dto: PostDto) {
return this.http.post<Post>(this.base, dto);
}


delete(id: number) {
return this.http.delete(`${this.base}/${id}`);
}
}