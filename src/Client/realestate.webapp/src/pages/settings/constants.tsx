import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Grid from "../../components/grid";
import {
  ConstantTypeValues,
  LanguageTypeValues,
  type ConstantItemViewModel,
  type ConstantsResponseViewModel,  
  type ConstantType
} from "../../viewModels/settings/constantViewModel";
import { ConstantService } from "../../services/settings/constant/constantService";
interface ConstantsListProps{
  type:string,
  editUrl:string  
}
const Constants = ({type,editUrl}:ConstantsListProps) => {
  const [itemsList, setItems] = useState<ConstantItemViewModel[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [pageNumber, setPageNumber] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [refreshGrid,setRefreshGrid] = useState(false);
  // const { type } = useParams<{ type?: string }>();

  const constantService:ConstantService=new ConstantService();
  const fetchItems = async () => {
    try {
      setLoading(true);      
      const constantType:ConstantType | undefined = getConstantType(type);
      const [data,response]:[ConstantsResponseViewModel,Response] =await constantService.FetchConstantsList(pageNumber,
        constantType !== undefined? constantType : ConstantTypeValues.Builder,LanguageTypeValues.English);
      if(response.ok){
        setItems(data.itemsList);
        setTotalPages(data.pagesCount); 
      }      
    } catch (err: any) {
      console.error("Error fetching items:", err);
      setError(err.message ?? "Unknown error");
    } finally {
      setLoading(false);
      setRefreshGrid(false);
    }
  };
  useEffect(() => {
    fetchItems();
  }, [pageNumber,refreshGrid]);

const getConstantType=(type?:string): ConstantType | undefined=>{
  const constantType: ConstantType | undefined = type !== undefined ? ConstantTypeValues[type as keyof typeof ConstantTypeValues] : undefined;
  return constantType;  
}

const deleteConstans = async (id: string) => {
    const confirmDelete = window.confirm("Are you sure you want to delete this constructor?");
    if (!confirmDelete) return;
  try {
    const [result,response]:[boolean,Response] =await constantService.DeleteConstant(id);
    if (response.ok && result) {  
      setRefreshGrid(true);
    } else {
       console.error("Delete failed:", response.status, await response.text());
       throw new Error(await response.text());
    }
  } catch (err) {
    console.error("Delete failed:", err);
    setError('Error in deleting the item');
    setRefreshGrid(true);
  }
};

  if (loading) return <div>Loading...</div>;

  return (
    <div className="p-6 space-y-4">
      <div className="flex justify-between items-center">
        <p>{error}</p>       
        <Link
          to="/Settings/Builder/Create"
          className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700"
        >
          + Add
        </Link>
      </div>

      <Grid<ConstantItemViewModel>
        data={itemsList}
        columns={[{name:'title',title:'Title'},{name:'description',title:'Description'}]}
        pageNumber={pageNumber}
        totalPages={totalPages}
        setPageNumber={setPageNumber}
        showEdit={true}
        editUrl={editUrl}
        showDelete={true}
        setDelete={deleteConstans}
      />
    </div>
  );
};

export default Constants;
