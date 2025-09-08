
import { BrowserRouter, Routes, Route } from "react-router-dom";
import AdminLayout from "../layouts/adminLayout";
import Posts from "../pages/Weblog/Posts";
import Home from "../pages/home/Home";
import TagsList from "../pages/settings/tag/tagsList";
import ConstractorsList from "../pages/settings/constructor/constractorsList";
import RegionsList from "../pages/settings/region/regionsList";
import UpdateConstructor from "../pages/settings/constructor/updateConstructor";
import UpdateRegion from "../pages/settings/region/updateRegion";
import UpdateTag from "../pages/settings/tag/updateTag";
import PropertyInventoryList from "../pages/realEstate/propertyInventoriesList";
import UpdatePropertyInventory from "../pages/realEstate/updatePropertyInventory";


function AppRoutes() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<AdminLayout />}>
          <Route index path="/" element={<Home />} />
          <Route path="/Settings/Builder" element={<ConstractorsList />} />
          <Route path="/Settings/Builder/Create" element={<UpdateConstructor />} />
          <Route path="/Settings/Builder/Update/:id" element={<UpdateConstructor />} />
          <Route path="/Settings/Region" element={<RegionsList />} />
          <Route path="/Settings/Region/Create" element={<UpdateRegion />} />
          <Route path="/Settings/Region/Update/:id" element={<UpdateRegion />} />
          <Route path="/Settings/Tag" element={<TagsList />} />
          <Route path="/Settings/Tag/Create" element={<UpdateTag />} />
          <Route path="/Settings/Tag/Update/:id" element={<UpdateTag />} />
          <Route path="/Realestate/Houses/List" element={<PropertyInventoryList />} />
          <Route path="/Realestate/Houses/Create" element={<UpdatePropertyInventory />} />
          <Route path="/Realestate/Houses/Update/:id" element={<UpdatePropertyInventory />} />
          <Route path="/weblog/posts" element={<Posts />} />         
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default AppRoutes;
