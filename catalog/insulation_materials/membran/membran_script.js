import {Filter,  creatCheckboxes } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["PLANTER", "Изостуд", "Тефонд"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));

creatCheckboxes(list);
