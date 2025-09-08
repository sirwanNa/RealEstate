
export interface BaseViewModel{
     id?: string;
     isDeleted: boolean;
     createdDate?: Date;
}

export interface GridBaseViewModel{
   id: string;
}

export interface GridColumnViewModel{
  name:string;
  title:string;
}