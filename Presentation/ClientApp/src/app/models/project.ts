import { Task } from "./task";

export interface Project {
    id: string;
    title: string;
    description: string;
    tasks: Task[] | null;
    accessRights: AccessRights
}

export enum AccessRights {
  Read = 1,
  Write = 2,
  Owner = 3
}
