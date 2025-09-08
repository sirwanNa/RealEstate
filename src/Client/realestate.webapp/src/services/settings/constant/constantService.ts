import type { ConstantCreateViewModel, ConstantFilterViewModel, ConstantsResponseViewModel, ConstantType, LanguageType } from '../../../viewModels/settings/constantViewModel';
import {HttpRequest} from '../../httpRequest'
import { ConstantAPI } from './constantApi';

export class ConstantService{
  httpRequest:HttpRequest;
  constructor(){
    this.httpRequest = new HttpRequest();
  }  
  FetchConstantsList=async (pageNumber:number,type:ConstantType,language:LanguageType,title?:string,description?:string):Promise<[ConstantsResponseViewModel,Response]>=>{
    const filterModel:ConstantFilterViewModel=
    {
            pageNumber:pageNumber,
            pageSize: 10,
            type,
            language,
            title:title !== undefined ? title:'',
            description: description !== undefined ? description : "",
    };
    return await this.httpRequest.post<ConstantFilterViewModel,ConstantsResponseViewModel>(ConstantAPI.FetchConstantsList,filterModel);   
  }

  GetConstant = async (id: string): Promise<[ConstantCreateViewModel, Response]> => {
    return await this.httpRequest.get<ConstantCreateViewModel>(ConstantAPI.GetConstant(id));
  };

  CreateConstant = async (
    model: ConstantCreateViewModel
  ): Promise<[boolean, Response]> => {
    return await this.httpRequest.post<ConstantCreateViewModel, boolean>(
      ConstantAPI.CreateConstant,
      model
    );
  };

  UpdateConstant = async (
    model: ConstantCreateViewModel
  ): Promise<[boolean, Response]> => {
    return await this.httpRequest.put<ConstantCreateViewModel, boolean>(
      ConstantAPI.UpdateConstant,
      model
    );
  };

  DeleteConstant=async (id:string):Promise<[boolean,Response]>=>{
      return await this.httpRequest.delete<boolean>(ConstantAPI.DeleteConstant(id));
  }  
}