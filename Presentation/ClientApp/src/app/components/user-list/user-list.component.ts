import { Component } from '@angular/core';
import { UsersService } from 'src/app/core/services/users.service';
import { Friend, FriendshipStatus, User } from 'src/app/models/user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.sass'],
})
export class UserListComponent {
  users: User[] = [];
  friends: Friend[] = [];
  displayAll = true;
  userEmail = '';

  constructor(private usersService: UsersService) {}

  ngOnInit() {
    this.userEmail = localStorage.getItem('userEmail') as string;
    this.usersService.getFriends().subscribe((friends) => {
      this.friends = friends;

      this.usersService.get().subscribe((res) => {
        this.users = res.filter((user) => {
          const isFriend = this.friends.some((friend) => {
            return (
              friend.userEmail === user.email ||
              friend.requesterEmail === user.email
            );
          });

          return !isFriend && user.email !== this.userEmail;
        });
      });
    });
  }

  sendFriendRequest(userEmail: string, userFullname: string) {
    this.usersService.sendFriendRequest(userEmail, this.userEmail).subscribe(() => {
      this.users = this.users.filter(user => user.email !== userEmail);
      const friend: Friend = {
        userEmail: userEmail,
        userFullname: userFullname,
        requesterEmail: this.userEmail,
        requesterFullname: '',
        friendshipStatus: FriendshipStatus.Pending
      }
      this.friends.push(friend);
    })
  }

  handleFriendRequest(friend: Friend) {
    switch (friend.friendshipStatus) {
      case FriendshipStatus.Accepted:
        this.usersService
          .handleFriendRequest(
            friend.userEmail,
            friend.requesterEmail,
            this.frienshipStatus.Rejected
          )
          .subscribe(() => {
            this.friends = this.friends.filter(
              (it) =>
                it.userEmail !== friend.userEmail &&
                it.requesterEmail !== friend.requesterEmail
            );
            if (friend.userEmail === this.userEmail) {
              const user: User = {
                fullname: friend.requesterFullname,
                email: friend.requesterEmail
              }
              this.users.push(user);
            } else {
              const user: User = {
                fullname: friend.userFullname,
                email: friend.userEmail
              }
              this.users.push(user);
            }
          });
        break;
      case FriendshipStatus.Pending:
        if (friend.requesterEmail === this.userEmail) {
          break;
        }

        this.usersService
          .handleFriendRequest(
            friend.userEmail,
            friend.requesterEmail,
            this.frienshipStatus.Accepted
          )
          .subscribe((res) => {
            friend.friendshipStatus = res;
          });
        break;
    }
  }

  public get frienshipStatus(): typeof FriendshipStatus {
    return FriendshipStatus;
  }
}
