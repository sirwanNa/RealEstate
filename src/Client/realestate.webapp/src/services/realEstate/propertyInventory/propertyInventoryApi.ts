const BASE_URL = import.meta.env.VITE_API_URL;

export const PropertyInventoryAPI = {
  GetPropertyInventory: (id: string) =>
    `${BASE_URL}/PropertyInventory/GetPropertyInventory/${id}`,
  FetchPropertyInventoriesList: `${BASE_URL}/PropertyInventory/FetchPropertyInventoriesList`,
  Create: `${BASE_URL}/PropertyInventory/Create`,
  CreateFromFiles: `${BASE_URL}/PropertyInventory/CreateFromFiles`,
  Update: `${BASE_URL}/PropertyInventory/Update`,
  Delete: (id: string) => `${BASE_URL}/PropertyInventory/Delete/${id}`,
};
