import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";


let nameCheckboxes = ["Желтый",  "Черный"];

let colorsCheckboxes = [ "background-color: rgb(255,255,0);", "background-color:#000000;"];

let list = [];
list.push(new Filter("color", nameCheckboxes, colorsCheckboxes));

creatCheckboxes(list);
