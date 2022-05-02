import { Reply } from "./reply"

export interface Thread {
    id: number
    title: string
    content: string
    categoryId: string
    repliesCount: string
    viewsCount: string
    userName: string
    email: string
    wasCreated: Date
    replies: Reply[]
}