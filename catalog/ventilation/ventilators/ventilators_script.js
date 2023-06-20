import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["S-вентиляторы", "P-вентиляторы", "FLOW вентиляторы", "Регуляторы"];

let titlesColorCheckboxes =["Зелёный", "Кирпичный", "Коричневый", "Красный", "Светло-серый", "Серый", "Черный"];

let colorsCheckboxes = ["background-color: #008000;", "background-color: #884535;", "background-color: #964B00;",
    "background-color: #FF0000;", "background-color: #808080;", "background-color: #808080;",
    "background-color:#000000;"];

let width = ["110", "125", "200"];
let heightCheckboxes = ["500", "700"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", width));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));
list.push(new Filter("workWidth", heightCheckboxes));


creatCheckboxes(list);
