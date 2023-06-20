import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Приточные вентили", "Приточные элементы", "Вентиляционные решетки",
    "Сетки вентиляционной решетки", "Фланцы вентиляционной решетки"];

let brandCheckboxes = ["Krovent", "Vilpe"];

let titlesColorCheckboxes =["Бежевый", "Белый", "Зеленый", "Кирпичный", "Коричневый", "Красный", "Малярный белый",
    "Светло-серый", "Серый", "Черный", "Шоколад"];

let colorsCheckboxes = ["background-color: #F5F5DC;", "background-color: #FFFFFF;", "background-color: #008000;",
    "background-color: #884535;", "background-color: #964B00;", "background-color: #FF0000;",
    "background-color: #ffffff;", "background-color: #808080;", "background-color: #808080;", "background-color: #000000;",
    "background-color: #D2691E;"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("coating", brandCheckboxes));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));



creatCheckboxes(list);
