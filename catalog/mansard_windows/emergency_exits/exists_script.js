import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["1 камера", "2 камеры"];

let widthCheckboxes = ["66х118 см", "78х98 см", "78х140 см", "114х140 см"];


let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", widthCheckboxes));

creatCheckboxes(list);
