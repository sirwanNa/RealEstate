import type { BaseViewModel } from "../../baseViewModel";

export interface PropertyInventoryListViewModel extends BaseViewModel {
  pagesCount: number;
  itemsList: PropertyInventoryListItemViewModel[];
}

export interface PropertyInventoryListItemViewModel extends BaseViewModel {
  structureType: StructureType;
  realEstateTypesList?: string;
  title: string; // required
  price: number;
  totalOfRooms?: string;
  capacity?: string;
  region?: string;
  builder?: string;
  date?: string; 
  editCommand?: string; 
}
export interface PropertyInventoryBaseViewModel extends BaseViewModel {
  structureType: StructureType;
  positionType: PositionType;
 

  startDate?: string | null;
  finishDate?: string | null;
  price: number;
  totalOfRooms?: string | null;
  capacity?: string | null;
  currency: Currency;
  regionId: string | null; 
  builderId: string | null; 

  title: string; // required
  description?: string;

  title_Fa: string; // required
  description_Fa?: string;

  title_Ar: string; // required
  description_Ar?: string;

  shortDescription: string; // required
  shortDescription_Fa: string; // required
  shortDescription_Ar: string; // required

  selectedTagIdsList_En?: string[]; // Guid[] → string[]
  selectedTagIdsList_Fa?: string[];
  selectedTagIdsList_Ar?: string[];

  selectedTypesList?: RealEstateType[];
  selectedFeaturesList?: FeatureType[];

  prepayment?: string;
  duringProjectPayment?: string;
  handoverPayment?: string;
  afterHandoverPayment?: string;
  brochureLink?: string;
}
export interface PropertyInventoryViewModel extends PropertyInventoryBaseViewModel {
  attachments?: FileUploadViewModel;
  
  tagsList_En?: SelectListItem[];
  tagsList_Fa?: SelectListItem[];
  tagsList_Ar?: SelectListItem[];
  
  structureTypesList?: SelectListItem[];
  realEstateTypesList?: SelectListItem[];
  currenciesList?: SelectListItem[];
  regionsList?: SelectListItem[];
  buildersList?: SelectListItem[];
  featuresList?: SelectListItem[];
  positionTypesList?: SelectListItem[];
}
export interface SelectListItem {
  value: string;
  text: string;
  selected?: boolean;
}
export interface FileUploadViewModel {
  fileName: string;
  filePath: string;
  size?: number;
}
export interface PropertyInventoryFilterViewModel {
  pageNumber: number;
  pageSize: number;
  language: Language;
  structureType?: StructureType;
  realEstateType?: RealEstateType;
  startDate?: string;
  finishDate?: string;
  price_From?: number;
  price_To?: number;
  regionId?: string | null; 
  builderId?: string | null; 
  title?: string | undefined | null;
  orderBy: PropertyInventoryOrderType;
  languagesList?: SelectListItem[];
  structureTypesList?: SelectListItem[];
  realEstateTypesList?: SelectListItem[];
  regionsList?: SelectListItem[];
  buildersList?: SelectListItem[];
}
//enums

// StructureType
export type StructureType = 0 | 1;

export const StructureTypeValues = {
  Ready: 0 as StructureType,
  Inprogress: 1 as StructureType,
};

// Currency
export type Currency = 0 | 1;

export const CurrencyTypeValues = {
  AED: 0 as Currency,
  USD: 1 as Currency,
};

export type PositionType = 0 | 1;

export const PositionTypeValues = {
  Freehold: 0 as PositionType,
  Leasehold: 1 as PositionType,
};

export type RealEstateType = 0 | 1 | 2 | 3 | 4;

export const RealEstateTypeValues = {
  Apartment: 0 as RealEstateType,
  Villa: 1 as RealEstateType,
  TownHouse: 2 as RealEstateType,
  PentHouse: 3 as RealEstateType,
  Duplex: 4 as RealEstateType,
};

// FeatureType.ts
export type FeatureType =
  | 0
  | 1
  | 2
  | 3
  | 4
  | 5
  | 6
  | 7
  | 8
  | 9
  | 10
  | 11
  | 12;

export const FeatureTypeValues = {
  SmartHome: 0 as FeatureType,
  ChildrenPlayground: 1 as FeatureType,
  Pool: 2 as FeatureType,
  Gym: 3 as FeatureType,
  Security: 4 as FeatureType,
  GreenAreas: 5 as FeatureType,
  JoggingTracks: 6 as FeatureType,
  PadelCourt: 7 as FeatureType,
  OutdoorDiningArea: 8 as FeatureType,
  BBQArea: 9 as FeatureType,
  OutdoorLoungeSpace: 10 as FeatureType,
  Clubhouse: 11 as FeatureType,
  PremiumServices: 12 as FeatureType,
};

export type PropertyInventoryOrderType = 0 | 1 | 2 | 3 | 4 | 5;

export const PropertyInventoryOrderTypeValues = {
  Default: 0 as PropertyInventoryOrderType,
  Price_Low_To_High: 1 as PropertyInventoryOrderType,
  Price_High_To_Low: 2 as PropertyInventoryOrderType,
  Date_New_To_Old: 3 as PropertyInventoryOrderType,
  Date_Old_To_New: 4 as PropertyInventoryOrderType,
  CreateDate: 5 as PropertyInventoryOrderType,
};

export type Language = 0 | 1 | 2;

export const LanguageValues = {
  Persian: 0 as Language,
  English: 1 as Language,
  Arabic: 2 as Language,
};
