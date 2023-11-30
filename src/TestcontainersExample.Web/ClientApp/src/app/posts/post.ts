import {User} from "../users/user";

export type Post = {
  id: string; // guid
  title: string;
  body: string;
  user: User;
}
