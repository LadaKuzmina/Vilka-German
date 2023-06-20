import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Grand Line"];

let nameCheckboxes = ["RAL 1014 Слоновая кость","RAL 1015 Светлая слоновая кость", "RAL 1018 Желтый цинк","RAL 2004 Оранжевый",
    "RAL 3003 Рубиново-красный", "RAL 3005 Винно-красный",    "RAL 3009 Оксид красный",    "RAL 3011 Коричнево-красный",
    "RAL 5002 Ультрамариново-синий",    "RAL 5005 Сигнальный синий",    "RAL 5021 Водная синь",    "RAL 6002 Лиственно-зелёный",
    "RAL 6005 Зеленый мох",    "RAL 6019 Зеленая пастель",    "RAL 6020 Хромовый зелёный",    "RAL 7004 Серый",
    "RAL 7005 Мышиный",    "RAL 7016 Антрацитово-серый",    "RAL 7024 Серый графит",    "RAL 7040 Серое окно",
    "RAL 8004 Коричневая медь",    "RAL 8017 Коричневый шоколад",    "RAL 8022 Чёрно-коричневый",    "RAL 9003 Белый",
    "RAL 9005 Черный темный",    "RAL 9006 Бело-алюминиевый",    "RR 11 Темно-зеленый",    "RR 21 Светло-серый",
    "RR 23 Темно-серый",  "RR 29 Вишневый",  "RR 32 Темно-коричневый",  "RR 33 Черный",  "RR 887"];

let colorsCheckboxes = ["background-color: rgb(225,215,196);", "background-color: #EADEBD;",
    "background-color: rgb(240,230,140);", "background-color: rgb(255,87,33);", "background-color: rgb(149,43,41);",
    "background-color: rgb(112,25,19);", "background-color: rgb(125,27,23);", "background-color: rgb(122,36,29);",
    "background-color:#0D1C33;", "background-color: rgb(0,45,98);", "background-color: rgb(0,83,138);",
    "background-color: rgb(58,106,71);", "background-color: rgb(38,67,38);", "background-color:#BDECB6;",
    "background-color: rgb(55,66,47);", "background-color: rgb(150,150,150);", "background-color: rgb(115,115,115);",
    "background-color: rgb(55, 63,67);", "background-color: #9EA0A1;", "background-color:rgb(55,66,47);", "background-color: rgb(139,69,19);",
    "background-color: rgb(81,43,28);", "background-color: rgb(61,60,58);", "background-color: #F4F8F4;", "background-color: rgb(0,0,0);",
    "background-color: #ffffff;", "background-color: rgb(0,102,71);", "background-color: rgb(0, 51, 0);",
    "background-color: rgb(192, 192, 192);", "background-color: rgb(128, 128, 128);", "background-color: rgb(128, 0, 0);",
    "background-color: rgb(51, 25, 0);", "background-color: rgb(204, 85, 51);"];

let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("color", nameCheckboxes, colorsCheckboxes));


creatCheckboxes(list);
