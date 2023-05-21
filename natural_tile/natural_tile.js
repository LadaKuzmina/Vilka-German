import {Filter,  creatCheckboxes, } from "../scripts/adding_filters.js";


let nameColorsCheckboxes = ["Антик", "Вишня", "Графит", "Кирпично-красный", "Кирпичный", "Коричневый", "Красный",
    "Магма", "Серый", "Серый кристалл", "Темно-коричневый", "Темно-серый", "Черный"];

let colorsCheckboxes = ["background-color: rgb(77,34,14)", "background-color: rgb(207,53,52)", "background-color: rgb(71,74,81)",
                   "background-color: rgb(143,20,2)", "background-color: rgb(160,54,35)", "background-color: rgb(77,34,14)",
                   "background-color: rgb(255,0,0)", "background-color: rgb(211,95,17)", "background-color: rgb(160,160,164)",
    "background-color: rgb(190, 190, 190)", "background-color: rgb(101,67,33)", "background-color: rgb(169,169,169)", "background-color: rgb(0,0,0)" ];


let list = [];
list.push(new Filter("color", nameColorsCheckboxes, colorsCheckboxes));

creatCheckboxes(list);
