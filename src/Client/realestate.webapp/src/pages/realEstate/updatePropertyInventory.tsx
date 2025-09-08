import { useState, useEffect, type ChangeEvent, type FormEvent } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { CurrencyTypeValues, PositionTypeValues,  StructureTypeValues, type PropertyInventoryViewModel, type SelectListItem } from "../../viewModels/realEstate/propertyInventory/propertyInventoryModel";
import { PropertyInventoryService } from "../../services/realEstate/propertyInventory/propertyInventoryService";
import { ConstantTypeValues, LanguageTypeValues, type ConstantsResponseViewModel, type ConstantType } from "../../viewModels/settings/constantViewModel";
import { ConstantService } from "../../services/settings/constant/constantService";
import Dropdown from "../../components/dropDown";


const UpdatePropertyInventory = () => {
  const { id } = useParams<{ id?: string }>();

  const [model, setModel] = useState<PropertyInventoryViewModel>({
    structureType: StructureTypeValues.Ready,
    //realEstateType: RealEstateTypeValues.Apartment,
    positionType: PositionTypeValues.Freehold,
    currency: CurrencyTypeValues.AED,
    price: 0,
    capacity: null,
    //bedrooms: 0,
    startDate: "",
    finishDate: "",
    regionId: null,
    builderId: null,
    title: "",
    title_Fa: "",
    title_Ar: "",
    shortDescription: "",
    shortDescription_Fa: "",
    shortDescription_Ar: "",
    description: "",
    description_Fa: "",
    description_Ar: "",
    //isFeatured: false,
    //isInstallment: false,
    selectedTagIdsList_En: [],
    selectedTagIdsList_Fa: [],
    selectedTagIdsList_Ar: [],
    selectedTypesList: [],
    selectedFeaturesList: [],
    buildersList: [],
    regionsList: [],
    tagsList_En: [],
    tagsList_Fa: [],
    tagsList_Ar: [],
    //typesList: [],
    featuresList: [],
    isDeleted:false
  });

  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();
  const propertyInventoryService = new PropertyInventoryService();

  const fetchData = async () => {
    try {
      setLoading(true);
      if (!id) return;
      const [data, response] = await propertyInventoryService.GetPropertyInventory(id);
      if (response.ok) {
        setModel(data);
      } else {
        throw new Error(`Failed with status ${response.status}`);
      }
    } catch (err) {
      console.error("Error fetching property inventory:", err);
      setError("Error in fetching data");
    } finally {
      setLoading(false);
    }
  };
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

  const loadModel=async ()=>{      
    const [builders, regions] = await Promise.all([
      fetchConstants(ConstantTypeValues.Builder, (list) =>
        list.map((b) => ({ value: b.id, text: b.title }))
      ),
      fetchConstants(ConstantTypeValues.Region, (list) =>
        list.map((r) => ({ value: r.id, text: r.title }))
      ),
    ]);

    setModel((prev) => ({
      ...prev,
      buildersList: builders,
      regionsList: regions,
    }));
  }

  useEffect(() => {
    loadModel();
    if (id) fetchData();
  }, [id]);

  const handleChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setModel((prev) => ({
      ...prev,
      [name]: value,
    }));
  };

  const returnBack=()=>{
     navigate("/realestate/houses/list");
  }

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();
    try {
      const [result, response] = id
        ? await propertyInventoryService.UpdatePropertyInventory(model)
        : await propertyInventoryService.CreatePropertyInventory(model);

      if (response.ok && result) {
        returnBack();
      }
    } catch (err) {
      console.error("Save failed:", err);
      setError("Save failed");
    }
  };

  const BackToList = () => {
    returnBack();
  };

  if (loading) return <p>Loading...</p>;

  return (
    <form
      onSubmit={handleSubmit}
      className="space-y-6 max-w-6xl mx-auto p-6 border rounded shadow"
    >
      <p className="text-red-500">{error}</p>
      <div className="grid grid-cols-3 gap-4">
        <div>          
          <Dropdown
            label="Structure Type"
            name="structureType"
            value={model.structureType}
            onChange={handleChange}
            options={Object.entries(StructureTypeValues).map(([key, val]) => ({
              text: key,
              value: val.toString()
            }))}
          />
        </div>
        <div>
          <Dropdown
            label="Position Type"
            name="positionType"
            value={model.positionType}
            onChange={handleChange}
            options={Object.entries(PositionTypeValues).map(([key, val]) => ({
              text: key,
              value: val.toString()
            }))}
          />
        </div>
      </div>

      {/* Price / Currency / Capacity / Bedrooms */}
      <div className="grid grid-cols-4 gap-4">
        <div>
          <label className="block font-semibold">Price</label>
          <input
            type="number"
            name="price"
            value={model.price || ""}
            onChange={handleChange}
            className="w-full border rounded p-2"
          />
        </div>

        <div>
         <Dropdown
            label="Currency"
            name="currency"
            value={model.currency}
            onChange={handleChange}
            options={Object.entries(CurrencyTypeValues).map(([key, val]) => ({
              text: key,
              value: val.toString()
            }))}
          />
        </div>

        <div>
          <label className="block font-semibold">Capacity</label>
          <input
            type="number"
            name="capacity"
            value={model.capacity || ""}
            onChange={handleChange}
            className="w-full border rounded p-2"
          />
        </div>
      </div>

      {/* Dates */}
      <div className="grid grid-cols-2 gap-4">
        <div>
          <label className="block font-semibold">Start Date</label>
          <input
            type="date"
            name="startDate"
            value={model.startDate || ""}
            onChange={handleChange}
            className="w-full border rounded p-2"
          />
        </div>
        <div>
          <label className="block font-semibold">Finish Date</label>
          <input
            type="date"
            name="finishDate"
            value={model.finishDate || ""}
            onChange={handleChange}
            className="w-full border rounded p-2"
          />
        </div>
      </div>

      {/* Region & Builder */}
      <div className="grid grid-cols-2 gap-4">
        <div>
          <Dropdown
            label="Region"
            name="regionId"
            value={model.regionId}
            onChange={handleChange}
            options={model.regionsList?.map((r: SelectListItem) => ({
              text: r.text,
              value: r.value
            }))}
          />
        </div>

        <div>
          <Dropdown
            label="Builder"
            name="builderId"
            value={model.builderId}
            onChange={handleChange}
            options={model.buildersList?.map((b: SelectListItem) => ({
              text: b.text,
              value: b.value
            }))}
          />
        </div>
      </div>

      {/* Multilingual fields */}
      <div className="grid grid-cols-3 gap-4">
        <div>
          <label className="block font-semibold">Title (EN)</label>
          <input
            name="title"
            value={model.title}
            onChange={handleChange}
            className="w-full border rounded p-2"
          />
        </div>
        <div>
          <label className="block font-semibold">Title (FA)</label>
          <input
            name="title_Fa"
            value={model.title_Fa}
            onChange={handleChange}
            className="w-full border rounded p-2"
          />
        </div>
        <div>
          <label className="block font-semibold">Title (AR)</label>
          <input
            name="title_Ar"
            value={model.title_Ar}
            onChange={handleChange}
            className="w-full border rounded p-2"
          />
        </div>
      </div>

      {/* Short Descriptions */}
      <div className="grid grid-cols-3 gap-4">
        <div>
          <label className="block font-semibold">Short Description (EN)</label>
          <textarea
            name="shortDescription"
            value={model.shortDescription}
            onChange={handleChange}
            className="w-full border rounded p-2"
          />
        </div>
        <div>
          <label className="block font-semibold">Short Description (FA)</label>
          <textarea
            name="shortDescription_Fa"
            value={model.shortDescription_Fa}
            onChange={handleChange}
            className="w-full border rounded p-2"
          />
        </div>
        <div>
          <label className="block font-semibold">Short Description (AR)</label>
          <textarea
            name="shortDescription_Ar"
            value={model.shortDescription_Ar}
            onChange={handleChange}
            className="w-full border rounded p-2"
          />
        </div>
      </div>

      {/* Full Descriptions */}
      <div>
        <label className="block font-semibold">Description (EN)</label>
        <textarea
          name="description"
          value={model.description}
          onChange={handleChange}
          className="w-full border rounded p-2"
          rows={5}
        />
      </div>
      <div>
        <label className="block font-semibold">Description (FA)</label>
        <textarea
          name="description_Fa"
          value={model.description_Fa}
          onChange={handleChange}
          className="w-full border rounded p-2"
          rows={5}
        />
      </div>
      <div>
        <label className="block font-semibold">Description (AR)</label>
        <textarea
          name="description_Ar"
          value={model.description_Ar}
          onChange={handleChange}
          className="w-full border rounded p-2"
          rows={5}
        />
      </div>

      {/* File Upload */}
      <div>
        <label className="block font-semibold">Upload Images</label>
        <input type="file" multiple className="w-full border rounded p-2" />
      </div>

      {/* Actions */}
      <div className="flex space-x-4">
        <button
          type="submit"
          className="bg-blue-600 text-white px-4 py-2 rounded"
        >
          {id ? "Update" : "Create"}
        </button>
        <button
          type="button"
          onClick={BackToList}
          className="bg-gray-600 text-white px-4 py-2 rounded"
        >
          Back
        </button>
      </div>
    </form>
  );
};

export default UpdatePropertyInventory;
