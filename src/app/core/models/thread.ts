export interface Thread {
    id: number
    replies: number
    views: number
    content: string
    firstName: string
    lastName: string
    age: number
    email: string
    threads: Thread
    created: Date
}