import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Grand Line",   "Locinox"];

let nameCheckboxes = ["RAL 1013 Жемчужно-белый",    "RAL 1014 Слоновая кость",
    "RAL 3005 Винно-красный",    "RAL 3005",    "RAL 3005/3005 Винно-красный двухсторонний",
    "RAL 6005 Зеленый мох",    "RAL 7004 Серый",    "RAL 7024 Серый графит",    "RAL 7040 Серое окно",
    "RAL 8017 Коричневый шоколад",    "RAL 8017/8017 Коричневый шоколад двухсторонний",    "RAL 9003 Белый",    "RR 32 Темно-коричневый" ];

let colorsCheckboxes = ["background-color: rgb(227, 217, 198);", "background-color: rgb(225,215,196);", "background-color: rgb(112,25,19);",
    "background-color: rgb(112,31,41);", "background-color:#6C1B2A;", "background-color: rgb(38,67,38);", "background-color: rgb(150,150,150);",
    "background-color: #9EA0A1;", "background-color: rgb(150,150,150);", "background-color: rgb(81,43,28);", "background-color:#45322E;",
    "background-color: #F4F8F4;", "background-color:rgb(128, 0, 0);"];

let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("color", nameCheckboxes, colorsCheckboxes));

creatCheckboxes(list);
