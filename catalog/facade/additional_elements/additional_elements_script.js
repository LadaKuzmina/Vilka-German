import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Cedral", "Kmew", "Ю-Пласт"];


let titlesColorCheckboxes =["RAL 1014 Слоновая кость", "RAL 1015 Светлая слоновая кость","RAL 3005 Винно-красный",
    "RAL 3011 Коричнево-красный", "RAL 7004 Серый", "RAL 7016 Антрацитово-серый", "RAL 7024 Серый графит",
    "RAL 8004 Коричневая медь", "RAL 8017 Коричневый шоколад", "RAL 9003 Белый", "RAL 9005 Черный темный",
    "RAL 9006 Бело-алюминиевый",  "RR 32 Темно-коричневый"];

let colorsCheckboxes = ["background-color: rgb(225,215,196);", "background-color: #EADEBD;",
    "background-color: rgb(112,25,19);", "background-color: rgb(122,36,29);", "background-color: rgb(150,150,150);",
    "background-color: rgb(55, 63, 67);", "background-color: #9EA0A1;", "background-color: rgb(139,69,19);",
    "background-color: rgb(81,43,28);", "background-color: #F4F8F4;", "background-color: rgb(0,0,0);",
    "background-color: rgb(161,161,160);", "background-color: rgb(101,67,33);"
];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));


creatCheckboxes(list);
