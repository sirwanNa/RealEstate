import type {BaseViewModel, GridBaseViewModel} from '../baseViewModel'

export interface ConstantItemViewModel extends GridBaseViewModel {
  title: string;
  description: string | null;
}

export interface ConstantsResponseViewModel {
  pagesCount: number;
  itemsList: ConstantItemViewModel[];
}

export interface ConstantCreateViewModel extends BaseViewModel{
    
  type: number; 

  title: string | null;
  description: string | null;

  title_Fa: string | null;
  description_Fa: string | null;

  title_Ar: string | null;
  description_Ar: string | null;
}

export type ConstantType = 0 | 1 | 2;

export const ConstantTypeValues = {
  Builder: 0 as ConstantType,
  Region: 1 as ConstantType,
  Tag: 2 as ConstantType,
};

export type LanguageType = 0 | 1 | 2;

export const LanguageTypeValues = {
  Persian: 0 as LanguageType,
  English: 1 as LanguageType,  
  Arabic: 2 as LanguageType,
};

export interface ConstantFilterViewModel{
    pageNumber:number;
    pageSize: number;
    type: ConstantType;
    language: number;
    title: string;
    description: string;
}