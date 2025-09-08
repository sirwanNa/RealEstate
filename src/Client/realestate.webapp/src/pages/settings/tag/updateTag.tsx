import { useNavigate } from "react-router-dom";
import UpdateConstant from "../updateConstant";


function UpdateTag(){
    const navigate = useNavigate();
    const returnBackToList=()=>{
        navigate("/Settings/Tag");
    }
    return (<UpdateConstant type="Tag" returnBack={returnBackToList}></UpdateConstant>)
;}

export default UpdateTag;