import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["60x120", "70x120"];

let heightCheckboxes = ["280 см", "300 см"];

let widthCheckboxes = ["30 мм",  "36 мм",  "80 мм", "не утеплена"];

let modelCheckboxes = ["Dacha", "Standart", "Premium", "Lux"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("coating", heightCheckboxes));
list.push(new Filter("width", modelCheckboxes));
list.push(new Filter("workWidth", widthCheckboxes));

creatCheckboxes(list);
