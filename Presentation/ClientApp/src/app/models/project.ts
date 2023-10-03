import { Task } from "./task";

export interface Project {
    id: string;
    title: string;
    description: string;
    tasks: Task[] | null;
}