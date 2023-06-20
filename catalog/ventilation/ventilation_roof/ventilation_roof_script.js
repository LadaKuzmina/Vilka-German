import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Krovent", "Vilpe", "Viotto"];

let titlesColorCheckboxes =["RAL 3005 Винно-красный", "RAL 5005 Сигнальный синий",  "RAL 6005 Зеленый мох",
    "RAL 7024 Серый графит", "RAL 8004 Коричневая медь", "RAL 8017 Коричневый шоколад",  "RAL 8019 Серо-коричневый",
    "RAL 9005 Черный темный",  "RR 11 Темно-зеленый",  "RR 23 Темно-серый",  "RR 28 Темно-вишневый", "RR 32 Темно-коричневый",
    "RR 750 Терракотовый", "Зеленый", "Кирпичный", "Коричневый", "Красный", "Светло-серый", "Серый",  "Синий", "Темно-серый",
    "Черный", "Шоколад"];

let colorsCheckboxes = ["background-color: rgb(112,25,19);", "background-color: rgb(0,45,98);", "background-color: rgb(38,67,38);",
    "background-color: #9EA0A1;", "background-color: rgb(139,69,19);", "background-color: rgb(81,43,28);",
    "background-color: rgb(102,102,102);", "background-color: rgb(0,0,0);", "background-color: rgb(0,102,71);",
    "background-color: rgb(42, 58, 65);", "background-color: rgb(100, 17, 21);", "background-color: rgb(101,67,33);",
    "background-color: rgb(204,85,0);",   "background-color: #008000;", "background-color: #884535;",
    "background-color:#964B00;", "background-color: #FF0000;",
    "background-color: #808080;", "background-color: #808080;", "background-color: #0000ff",
    "background-color: rgb(169,169,169);", "background-color: #000000;", "background-color: #D2691E;"];

let ventils  = ["Muotokate KTV", "Huopa KTV", "Tiili KTV", "Pelti KTV", "Huopa KTV /harja", "DECRA KTV",
    "Universal KTV", "Krovent KTV Wave", "Krovent KTV Seam", "Krovent KTV General"];


let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));
list.push(new Filter("width", ventils));





creatCheckboxes(list);
