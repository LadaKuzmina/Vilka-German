import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Fakro", "Velux"];

let coatingCheckboxes = ["Классическая", "Универсальная", "Затемняющая", "Рулонная", "Плиссированая"];

let widthWindow = ["55х78 см", "55х98 см", "66х98 см", "66х118 см", "78х98 см", "78х118 см","78х140 см", "78х160 см",
    "94х118 см","94х140 см", "114х118 см", "114х140 см"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("coating", coatingCheckboxes));
list.push(new Filter("width", widthWindow));

creatCheckboxes(list);
