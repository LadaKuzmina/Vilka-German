import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Для битумной кровли", "Для металлических кровель",
    "Для натуральной черепицы", "Для круглых дымоходов"];

let brandCheckboxes =["Krovent", "Vilpe", "Viotto"];

let titlesColorCheckboxes = ["RAL 9005 Черный темный", "Антрацит", "Баклажан", "Бордо", "Зеленый", "Кирпичный",
    "Коричневый", "Красный", "Светло-серый", "Серый", "Синий", "Черный", "Шоколад"];

let colorsCheckboxes = ["background-color: rgb(0,0,0);", "background-color: #464646;", "background-color:#990066;",
    "background-color: #800000;", "background-color: #008000;", "background-color: #884535;",
    "background-color: #964B00;", "background-color: #FF0000;", "background-color: #808080;",
    "background-color: #808080;", "background-color: #0000ff;", "background-color: #000000;",
    "background-color: #D2691E;"]

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));
list.push(new Filter("workWidth", brandCheckboxes));


creatCheckboxes(list);
