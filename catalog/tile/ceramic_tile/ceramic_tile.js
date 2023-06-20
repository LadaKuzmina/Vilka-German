import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Рубин 9V", "Рубин 11V", "Рубин 13V","Турмалин", "Изумруд", "Опал"];

let titlesColorCheckboxes =["Антрацит", "Глубокий черный","Графит","Зеленая ель", "Каштан", "Кедр", "Королевский серый",
    "Красная лава", "Красная осень", "Красное пламя", "Красный бук", "Медный", "Натуральный красный","Серая галька",
    "Синий бриллиант", "Темно-коричневый", "Тик", "Черный бриллиант", "Черный вулкан", "Черный кристалл"]

let colorsCheckboxes = ["background-color: rgb(70,68,81)", "background-color: rgb(10,10,13)", "background-color: rgb(71,74,81)",
    "background-color: rgb(42,92,3)", "background-color: rgb(116,40,2)", "background-color: rgb(56,79,47)",
    "background-color: rgb(165,165,165)", "background-color: rgb(207,16,32)", "background-color: rgb(240,81,51)",
    "background-color: rgb(134,40,46)", "background-color: rgb(178,34,34)", "background-color: rgb(10,10,13)",
    "background-color: rgb(184,115,51)", "background-color: rgb(143,29,33)", "background-color: rgb(139,140,122)",
    "background-color: rgb(50,109,220)", "background-color: rgb(101,67,33)", "background-color: rgb(171,137,83)",
    "background-color: rgb(31,31,31)", "background-color: rgb(16,18,29)", "background-color: rgb(10,10,10)"]


let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));


creatCheckboxes(list);
