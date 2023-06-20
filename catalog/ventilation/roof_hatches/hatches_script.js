import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let titlesCheckboxes = ["Зеленый", "Кирпичный", "Коричневый", "Красный", "Серый", "Черный"];
let colorsCheckboxes = ["background-color: #008000;", "background-color: #884535;", "background-color: #964B00;",
    "background-color: #FF0000;", "background-color: #808080;", "background-color: #000000;"];

let list = [];

list.push(new Filter("profiled", titlesCheckboxes, colorsCheckboxes));



creatCheckboxes(list);
