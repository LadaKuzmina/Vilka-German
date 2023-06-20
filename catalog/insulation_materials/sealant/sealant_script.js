import {Filter,  creatCheckboxes } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Nicoband", "TYTAN"];

let colorsCheckboxes =["Медный", "Серебристый", "Серый", "Темно-серый", "Черный"];
let rgbCheckboxes = ["background-color: rgb(184,115,51);", "background-color: rgb(201,201,201);",  "background-color: #808080;",
    "background-color: rgb(169,169,169);", "background-color: #000000;",
];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("color", colorsCheckboxes, rgbCheckboxes));



creatCheckboxes(list);
