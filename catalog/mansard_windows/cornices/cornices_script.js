import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["46х55 см", "78х75 см", "78х98 см", "78х206 см", "78х235 см", "94х160 см", "94х180 см"];

let list = [];
list.push(new Filter("profiled", profiledCheckboxes));



creatCheckboxes(list);
