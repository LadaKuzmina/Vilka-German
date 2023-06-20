import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Fakro", "Velux"];


let widthWindow = ["45x55 см", "66х98 см", "66х118 см", "66х140 см", "78х60 см", "78х95 см", "78х115 см",
    "78х118 см", "80x80 см", "86х87 см", "90x90 см", "114х118 см"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", widthWindow));

creatCheckboxes(list);
