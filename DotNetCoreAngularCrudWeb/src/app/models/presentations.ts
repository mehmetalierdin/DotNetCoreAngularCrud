export class Presentations {
    id: number;
    title: string;
    thumbnail: string;
    creator: Creator;
    createdAt: Date;
}
export class Creator{
  name: string;
  profileUrl:string;
}