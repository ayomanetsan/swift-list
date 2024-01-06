import { Task } from "./task";

export interface TaskWithDetails extends Task {
    createdAt: Date;
    dueDate: Date;
    createdBy: string;
    labels: Label[] | undefined;
    toDoItems: ToDoItem[] | undefined;
    isCompleted: boolean;
    projectId?: string;
}

export interface Label {
    taskId: string;
    title: string;
    color: string;
}

export interface ToDoItem {
    id: string;
    title: string;
    isCompleted: boolean;
}

export const EmptyTask: TaskWithDetails = {
    id: '00000000-0000-0000-0000-000000000000',
    title: '',
    description: '',
    isCompleted: false,
    isArchived: false,
    createdAt: new Date(),
    createdBy: '',
    dueDate: new Date(),
    labels: [],
    toDoItems: [],
    projectId: ''
}