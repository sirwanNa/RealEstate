import { useNavigate } from "react-router-dom";
import UpdateConstant from "../updateConstant";


function UpdateRegion(){
    const navigate = useNavigate();
    const returnBackToList=()=>{
        navigate("/Settings/Region");
    }
    return (<UpdateConstant type="Region" returnBack={returnBackToList}></UpdateConstant>)
;}

export default UpdateRegion;