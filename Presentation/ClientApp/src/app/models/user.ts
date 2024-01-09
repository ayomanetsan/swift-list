export interface User {
  fullname: string,
  email: string
}

export interface Friend {
  userEmail: string,
  userFullname: string,
  requesterEmail: string,
  requesterFullname: string,
  friendshipStatus: FriendshipStatus
}

export enum FriendshipStatus {
  Pending = 0,
  Rejected = 1,
  Accepted = 2
}
