import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Grid from "../../components/grid";
import FilterPropertyInventory from "../realEstate/filter";
import {
  LanguageValues,
  PropertyInventoryOrderTypeValues,
  type PropertyInventoryFilterViewModel,
  type PropertyInventoryListItemViewModel,
} from "../../viewModels/realEstate/propertyInventory/propertyInventoryModel";

import { PropertyInventoryService } from "../../services/realEstate/propertyInventory/propertyInventoryService";

function PropertyInventoryList() {
  const [pageNumber, setPageNumber] = useState(1);
  const [itemsList, setItemsList] = useState<PropertyInventoryListItemViewModel[]>([]);
  const [totalPages, setTotalPages] = useState(1);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [refreshKey, setRefreshKey] = useState(0);

  const [filterModel, setFilterModel] = useState<PropertyInventoryFilterViewModel>({
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
      builderId:null
    });

  const propertyInventoryService = new PropertyInventoryService();

  const fetchData = async () => {
    try {
      setLoading(true);
      setError(null);
      // Fetch houses
      const [data, response] = await propertyInventoryService.FetchPropertyInventoriesList({
        ...filterModel,
        pageNumber,
      });

      if (!response.ok) throw new Error(`Failed to load data: ${response.statusText}`);

      setItemsList(data.itemsList);
      setTotalPages(data.pagesCount);
    } catch (err: any) {
      setError(err.message ?? "Unknown error occurred");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, [pageNumber, filterModel, refreshKey]);
  // const loadFilterData=async ()=>{
  //         // Fetch builders & regions in parallel
  //     const [builders, regions] = await Promise.all([
  //       fetchConstants(ConstantTypeValues.Builder, (list) =>
  //         list.map((b) => ({ value: b.id, text: b.title }))
  //       ),
  //       fetchConstants(ConstantTypeValues.Region, (list) =>
  //         list.map((r) => ({ value: r.id, text: r.title }))
  //       ),
  //     ]);

  //     setFilterModel((prev) => ({
  //       ...prev,
  //       buildersList: builders,
  //       regionsList: regions,
  //     }));
  // }
  // useEffect(() => {
  //   loadFilterData();   
  // }, []);

  const deletePropertyInventory = async (id: string) => {
    if (!window.confirm("Are you sure you want to delete this property?")) return;

    try {
      const [success, response]: [boolean, Response] =
        await propertyInventoryService.DeletePropertyInventory(id);

      if (response.ok && success) {
        setRefreshKey((k) => k + 1); // trigger reload
      } else {
        const msg = await response.text();
        throw new Error(msg || "Delete failed");
      }
    } catch (err: any) {
      console.error("Delete failed:", err);
      setError(err.message ?? "Error deleting property");
    }
  };

  return (
    <div className="p-6 space-y-6">
      {/* Header */}
      <div className="flex justify-between items-center">
        <h2 className="text-xl font-bold">Houses</h2>
        <Link
          to="/Realestate/Houses/Create"
          className="bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700"
        >
          + Add
        </Link>
      </div>

      {/* Error */}
      {error && <div className="text-red-600">{error}</div>}

      {/* Filters */}
      <FilterPropertyInventory  setFilterModel={setFilterModel} />

      {/* Grid */}
      {loading ? (
        <div>Loading...</div>
      ) : (
        <Grid<PropertyInventoryListItemViewModel>
          data={itemsList}
          columns={[
            { name: "title", title: "Title" },
            { name: "price", title: "Price" },
            { name: "region", title: "Region" },
            { name: "builder", title: "Builder" },
            { name: "date", title: "Date" },
          ]}
          pageNumber={pageNumber}
          totalPages={totalPages}
          setPageNumber={setPageNumber}
          showEdit
          editUrl="/Realestate/Houses/Update"
          showDelete
          setDelete={deletePropertyInventory}
        />
      )}
    </div>
  );
}

export default PropertyInventoryList;
