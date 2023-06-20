
import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let titlesColorCheckboxes =["Беленый дуб","Дуб","Орех"];

let colorsCheckboxes = ["background-color: rgb(172,138,86);" ,"background-color: rgb(139,90,43);" ,"background-color: rgb(91,58,41);"];

let list = [];
list.push(new Filter("profiled", titlesColorCheckboxes, colorsCheckboxes));

creatCheckboxes(list);
