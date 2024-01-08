import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Friend, FriendshipStatus, User } from 'src/app/models/user';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpService, private auth: AuthService) { }

  get() {
    return this.http.get<User[]>('users');
  }

  getFriends() {
    return this.http.get<Friend[]>('users/friends');
  }

  sendFriendRequest(userEmail: string, requesterEmail: string) {
    return this.http.post<FriendshipStatus>('users/send-friend-request', { userEmail, requesterEmail });
  }

  handleFriendRequest(userEmail: string, requesterEmail: string, friendshipStatus: FriendshipStatus) {
    return this.http.post<FriendshipStatus>('users/handle-friend-request', { userEmail, requesterEmail, friendshipStatus });
  }
}
