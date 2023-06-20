import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Встраиваемые модели", "Настенные модели", "Островные модели"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));



creatCheckboxes(list);
