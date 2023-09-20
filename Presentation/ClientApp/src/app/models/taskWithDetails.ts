import { Task } from "./task";

export interface TaskWithDetails extends Task {
    createdAt: Date;
    dueDate: Date;
    createdBy: string;
    labels: Label[] | undefined;
    toDoItems: ToDoItem[] | undefined;
}

interface Label {
    title: string;
    color: string;
}

export interface ToDoItem {
    id: string;
    title: string;
    isCompleted: boolean;
}