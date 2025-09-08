import { useEffect, useState, type ChangeEvent } from 'react';
import Dropdown from '../../components/dropDown';
import {LanguageValues, PropertyInventoryOrderTypeValues, type PropertyInventoryFilterViewModel, type SelectListItem} from '../../viewModels/realEstate/propertyInventory/propertyInventoryModel'
import { ConstantTypeValues, LanguageTypeValues, type ConstantsResponseViewModel, type ConstantType } from '../../viewModels/settings/constantViewModel';
import { ConstantService } from '../../services/settings/constant/constantService';


interface FilterProps{
    //filterModel:PropertyInventoryFilterViewModel,
    setFilterModel:(filterModel:PropertyInventoryFilterViewModel)=>void
}


function FilterPropertyInventory({setFilterModel }: FilterProps) {
  const [localFilter,setLocalFilter] = useState<PropertyInventoryFilterViewModel>({
      pageNumber: 1,
      pageSize: 10,
      language: LanguageValues.English,
      orderBy: PropertyInventoryOrderTypeValues.Date_New_To_Old,
      structureType: undefined,
      realEstateType: undefined,
      startDate: undefined,
      finishDate: undefined,
      price_From: undefined,
      price_To: undefined,
      title:'',
      regionId:null,
      builderId:null,
      buildersList: [],
      regionsList: [],
    });
    const constantService = new ConstantService();     
    const fetchConstants = async (
        type: ConstantType,
        mapper: (items: ConstantsResponseViewModel["itemsList"]) => SelectListItem[]
      ) => {
        const [result, response]: [ConstantsResponseViewModel, Response] =
          await constantService.FetchConstantsList(1, type, LanguageTypeValues.English);
  
        if (response.ok) {
          return mapper(result.itemsList);
        }
        return [];
      };   
    const loadFilterData=async ()=>{
            // Fetch builders & regions in parallel
        const [builders, regions] = await Promise.all([
          fetchConstants(ConstantTypeValues.Builder, (list) =>
            list.map((b) => ({ value: b.id, text: b.title }))
          ),
          fetchConstants(ConstantTypeValues.Region, (list) =>
            list.map((r) => ({ value: r.id, text: r.title }))
          ),
        ]);
  
        setLocalFilter((prev) => ({
          ...prev,
          buildersList: builders,
          regionsList: regions,
        }));
    }
    useEffect(() => {
      loadFilterData();   
    }, []);
  // const changeModel = (name: string, value: string) => {
  //     setLocalFilter({
  //       ...localFilter,
  //       [name]: value === "" ? null : value,
  //     });
  // };
  const changeModel = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setLocalFilter((prev) => ({
      ...prev,
      [name]: value === "" ? null : value,
    }));
  };
  const submitFilter =()=>{
      setFilterModel(localFilter);     
  }
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4 p-4 border rounded-lg shadow">
      {/* Title text filter */}
      <div className="mb-4">
        <label className="block mb-1 font-medium">Title</label>
        <input
          type="text"
          className="border rounded px-2 py-1 w-full"
          name="title"
          value={localFilter.title ?? ""}
          onChange={changeModel}
          placeholder="Enter property title"
        />
      </div>

      {/* Dropdowns */}
      <Dropdown
        label="Language"
        name="language"
        value={localFilter.language as unknown as string}
        options={localFilter.languagesList}
        onChange={changeModel}
      />

      <Dropdown
        label="Structure Type"
        name="structureType"
        value={localFilter.structureType as unknown as string}
        options={localFilter.structureTypesList}
        onChange={changeModel}
      />

      <Dropdown
        label="Real Estate Type"
        name="realEstateType"
        value={localFilter.realEstateType as unknown as string}
        options={localFilter.realEstateTypesList}
        onChange={changeModel}
      />

      <Dropdown
        label="Region"
        name="regionId"
        value={localFilter.regionId}
        options={localFilter.regionsList}
        onChange={changeModel}
      />

      <Dropdown
        label="Builder"
        name="builderId"
        value={localFilter.builderId}
        options={localFilter.buildersList}
        onChange={changeModel}
      />

      {/* Price range */}
      <div className="mb-4">
        <label className="block mb-1 font-medium">Price (From)</label>
        <input
          type="number"
          className="border rounded px-2 py-1 w-full"
          value={localFilter.price_From ?? ""}
          name="price_From"
          onChange={changeModel}
          placeholder="Min price"
        />
      </div>

      <div className="mb-4">
        <label className="block mb-1 font-medium">Price (To)</label>
        <input
          type="number"
          className="border rounded px-2 py-1 w-full"
          value={localFilter.price_To ?? ""}
          name="price_To"
          onChange={changeModel}
          placeholder="Max price"
        />
      </div>
      <div>
        <input type='button' onClick={submitFilter} value='Filter'/>
      </div>
    </div>
  );
}

export default FilterPropertyInventory;