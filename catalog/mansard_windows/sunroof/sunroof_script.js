import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Fakro", "Velux"];

let widthCheckboxes = ["45x55 см", "45x73 см", "46х55 см", "46х75 см", "54х75 см", "60x90 см",
    "85x85 см", "86х86 см", "86х87 см"];


let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", widthCheckboxes));


creatCheckboxes(list);
