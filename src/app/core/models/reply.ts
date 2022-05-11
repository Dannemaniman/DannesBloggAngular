import { Thread } from "./thread"

export interface Reply {
    id: number
    wasCreated?: Date
    wasModified?: Date
    title?: string
    content?: string
    userName?: string
    email?: string
    thread: Thread
}