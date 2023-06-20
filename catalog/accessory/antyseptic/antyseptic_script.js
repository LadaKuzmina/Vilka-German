import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["NEOMID", "Технониколь"];

let list = [];
list.push(new Filter("profiled", profiledCheckboxes));

creatCheckboxes(list);
