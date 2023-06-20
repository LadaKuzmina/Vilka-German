import {Filter,  creatCheckboxes } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Технониколь"];
let widthCheckboxes = ["1", "1,5", "5"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", widthCheckboxes));

creatCheckboxes(list);
