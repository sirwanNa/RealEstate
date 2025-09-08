import UpdateConstant from "../updateConstant";
import { useNavigate } from "react-router-dom";

function UpdateConstructor(){
    const navigate = useNavigate();
    const returnBackToList=()=>{
        navigate("/Settings/Builder");
     }
    return (<UpdateConstant type="Builder" returnBack={returnBackToList}></UpdateConstant>)
;}

export default UpdateConstructor;