import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["60х100", "60x120", "60x130", "70x100", "70x120", "70x130", "70x140"];

let heightCheckboxes = ["280 см",  "305 см", "330 см"];

let widthCheckboxes = ["36 мм",  "56 мм", "66 мм", "80 мм"];

let modelCheckboxes = ["LTK / LTK Energy", "LWL Extra", "LWT"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("coating", heightCheckboxes));
list.push(new Filter("width", modelCheckboxes));
list.push(new Filter("workWidth", widthCheckboxes));

creatCheckboxes(list);
