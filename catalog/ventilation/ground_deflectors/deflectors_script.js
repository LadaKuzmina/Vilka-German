import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["ROSS удлинители", "ROSS рем. комплекты", "ROSS цокольные дефлекторы"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));



creatCheckboxes(list);
