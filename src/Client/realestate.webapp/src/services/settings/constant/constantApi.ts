const BASE_URL = import.meta.env.VITE_API_URL;

export const ConstantAPI={
   FetchConstantsList:`${BASE_URL}/Constant/FetchConstantsList`,
   GetConstant: (id: string) => `${BASE_URL}/Constant/GetConstant/${id}`,
   CreateConstant: `${BASE_URL}/Constant/Create`,
   UpdateConstant: `${BASE_URL}/Constant/Update`,
   DeleteConstant :(id:string)=>`${BASE_URL}/Constant/Delete/${id}`

}