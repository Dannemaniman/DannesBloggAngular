import { Thread } from "./thread"

export interface Member {
    id: number
    userName: string
    firstName: string
    lastName: string
    age: number
    email: string
    threads: Thread
    created: Date
}