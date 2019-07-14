export class Note{
    id:number;
    title:string;
    description:string;
    finished:boolean;
    notebookId:number;
    deleted:boolean;

    // Auditable
    createdBy:string;
    createdOn:Date;
    updatedBy:string;
    updatedOn:Date;
}