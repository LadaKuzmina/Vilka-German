import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let brandCheckboxes = ["Ондулин", "Ондутис"];

let list = [];
list.push(new Filter("profiled", brandCheckboxes));

creatCheckboxes(list);
