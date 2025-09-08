import type {
  PropertyInventoryViewModel,
  PropertyInventoryListViewModel,
  PropertyInventoryFilterViewModel,
} from "../../../viewModels/realEstate/propertyInventory/propertyInventoryModel";
import { HttpRequest } from "../../httpRequest";
import { PropertyInventoryAPI } from "./propertyInventoryApi";

export class PropertyInventoryService {
  httpRequest: HttpRequest;

  constructor() {
    this.httpRequest = new HttpRequest();
  }

  // Fetch list of property inventories
  FetchPropertyInventoriesList = async (
    filterModel: PropertyInventoryFilterViewModel
  ): Promise<[PropertyInventoryListViewModel, Response]> => {
    return await this.httpRequest.post<
      PropertyInventoryFilterViewModel,
      PropertyInventoryListViewModel
    >(PropertyInventoryAPI.FetchPropertyInventoriesList, filterModel);
  };

  // Get single property inventory by ID
  GetPropertyInventory = async (
    id: string
  ): Promise<[PropertyInventoryViewModel, Response]> => {
    return await this.httpRequest.get<PropertyInventoryViewModel>(
      PropertyInventoryAPI.GetPropertyInventory(id)
    );
  };

  // Create new property inventory
  CreatePropertyInventory = async (
    model: PropertyInventoryViewModel
  ): Promise<[boolean, Response]> => {
    return await this.httpRequest.post<PropertyInventoryViewModel, boolean>(
      PropertyInventoryAPI.Create,
      model
    );
  };


  // Update property inventory
  UpdatePropertyInventory = async (
    model: PropertyInventoryViewModel
  ): Promise<[boolean, Response]> => {
    return await this.httpRequest.put<PropertyInventoryViewModel, boolean>(
      PropertyInventoryAPI.Update,
      model
    );
  };

  // Delete property inventory
  DeletePropertyInventory = async (id: string): Promise<[boolean, Response]> => {
    return await this.httpRequest.delete<boolean>(
      PropertyInventoryAPI.Delete(id)
    );
  };
}
