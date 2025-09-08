import { useState, useEffect, type ChangeEvent, type FormEvent } from "react";
import { useParams } from "react-router-dom";
import type { ConstantCreateViewModel, ConstantType } from "../../viewModels/settings/constantViewModel";
import { ConstantTypeValues } from "../../viewModels/settings/constantViewModel";
import { ConstantService } from "../../services/settings/constant/constantService";
interface UpdateConstantProps{
  type:string,
  returnBack:()=>void
}
const UpdateConstant=({type,returnBack}:UpdateConstantProps)=> {
  const getConstantType = (type?: string): number  => {
    const constantType = type !== undefined 
      ? ConstantTypeValues[type as keyof typeof ConstantTypeValues] 
      : 0;
    console.log('Type',type);
    console.log('Constant Type',constantType);
    return constantType;
  };
  const constantType:number = getConstantType(type);  
  const { id } = useParams<{ id?: string }>();
  const [model, setModel] = useState<ConstantCreateViewModel>({
    type: constantType,
    title: "",
    description: "",
    title_Fa: "",
    description_Fa: "",
    title_Ar: "",
    description_Ar: "",
    isDeleted:false
  });
  const [error,setError]= useState('');

  const [loading, setLoading] = useState(false);
 
  const constantService:ConstantService=new ConstantService();

  const fetchData = async () => {
      try {
        setLoading(true);
        if(id === undefined) throw new Error('Id not found');
        const [data, response] = await constantService.GetConstant(id);
        if (response.ok) {
          setModel(data);
        } else {
          throw new Error(`Failed with status ${response.status}`);
        }
      } catch (err) {
        console.error("Error fetching constant:", err);
        setError("Error in fetching data");
      } finally {
        setLoading(false);
      }
  };
  useEffect(() => {
    if (!id) return;
    fetchData();
  }, [id]);


  const handleChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setModel((prev) => ({
      ...prev,
      [name]: name === "type" ? Number(value) as ConstantType : value,
    }));
  };

const handleSubmit = async (e: FormEvent) => {
  e.preventDefault();
  try {
    const [result, response] = id
      ? await constantService.UpdateConstant(model)
      : await constantService.CreateConstant(model);

    if (response.ok && result) {      
      returnBack();
    }
  } catch (err) {
    console.error("Save failed:", err);
  }
};

const BackToList =()=>{
  returnBack();
}

  if (loading) return <p>Loading...</p>;

  return (
    <form onSubmit={handleSubmit} className="space-y-4 max-w-lg mx-auto p-4 border rounded shadow">
      <div>
        <p>{error}</p>
        <label className="block font-semibold">Type</label>
        <select name="type" value={model.type} onChange={handleChange} className="w-full border rounded p-2">
          {Object.entries(ConstantTypeValues).map(([key, val]) => (
            <option key={key} value={val}>{key}</option>
          ))}
        </select>
      </div>

      <div>
        <label className="block font-semibold">Title (EN)</label>
        <input name="title" value={model.title || ""} onChange={handleChange} className="w-full border rounded p-2" />
      </div>

      <div>
        <label className="block font-semibold">Description (EN)</label>
        <textarea name="description" value={model.description || ""} onChange={handleChange} className="w-full border rounded p-2" />
      </div>

      <div>
        <label className="block font-semibold">Title (FA)</label>
        <input name="title_Fa" value={model.title_Fa || ""} onChange={handleChange} className="w-full border rounded p-2" />
      </div>

      <div>
        <label className="block font-semibold">Description (FA)</label>
        <textarea name="description_Fa" value={model.description_Fa || ""} onChange={handleChange} className="w-full border rounded p-2" />
      </div>

      <div>
        <label className="block font-semibold">Title (AR)</label>
        <input name="title_Ar" value={model.title_Ar || ""} onChange={handleChange} className="w-full border rounded p-2" />
      </div>

      <div>
        <label className="block font-semibold">Description (AR)</label>
        <textarea name="description_Ar" value={model.description_Ar || ""} onChange={handleChange} className="w-full border rounded p-2" />
      </div>

      <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded">
        {id ? "Update" : "Create"}
      </button>
       <button type="button" onClick={BackToList} className="bg-blue-600 text-white px-4 py-2 rounded">
        Back
      </button>
    </form>
  );
}

export default UpdateConstant;
