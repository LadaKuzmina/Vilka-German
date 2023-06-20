import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Aquasystem", "Braas", "Docke", "Grand Line", "Optima", "Vortex", "МеталлПрофиль"];

let coatingCheckboxes = ["Полиэстер", "Цинк (Zn)", "Drap", "Granite", "Алюцинк", "Пластизол"];

let typeProfiled = ["Круглая", "Прямоугольная"];

let pieceDrain = ["Воронка",  "Желоб", "Колено", "Заглушка желоба", "Соединитель желоба", "Угол желоба",
    "Крюк крепления желоба", "Труба", "Хомут", "Тройник", "Коллектор", "S-обвод", "Отвод"];

let titlesColorCheckboxes =["RAL 1014 Слоновая кость",  "RAL 3005 Винно-красный",  "RAL 3011 Коричнево-красный",
    "RAL 6005 Зеленый мох", "RAL 7004 Серый", "RAL 7024 Серый графит", "RAL 8004 Коричневая медь",
    "RAL 8017 Коричневый шоколад",
    "RAL 9003 Белый", "RAL 9005 Черный темный", "RAL 9006 Бело-алюминиевый","RAL 9010 Чистый белый", "RAL Al-Zn",
    "RR 11 Темно-зеленый", "RR 29 Вишневый",  "RR 32 Темно-коричневый", "RR 33 Черный", "Белый","Бордо", "Графит",
    "Графитовый","Зеленый", "Коричневый", "Красный", "Медь", "Пломбир", "Р363 вишневый",
    "Серый", "Темно-коричневый", "Цинк", "Черный","Шоколад"];

let colorsCheckboxes = ["background-color: rgb(225,215,196);", "background-color: rgb(112,25,19);",
    "background-color: rgb(122,36,29);", "background-color: rgb(38,67,38);", "background-color: rgb(150,150,150);",
    "background-color: #9EA0A1;", "background-color: rgb(139,69,19);", "background-color: rgb(81,43,28);",
    "background-color: #F4F8F4;", "background-color: rgb(0,0,0);", "background-color: #ffffff;", "background-color: rgb(255,255,255);",
    "undefined", "background-color: rgb(0,102,71);", "background-color: rgb(178,34,34);",
    "background-color: rgb(101,67,33);", "background-color:#0f1412;", "background-color: #FFFFFF;", "background-color: #800000;",
    "background-color: rgb(71,74,81);", "background-color: rgb(71,74,81);", "background-color: #008000;", "background-color: #964B00;",
    "background-color: #FF0000;", "background-color: rgb(184,115,51);", "background-color: #9E1B34;", "background-color: rgb(222,12,98);",
    "background-color: #808080;", "background-color: rgb(50,109,220);", "background-color: rgb(170,170,175);", "background-color: #000000;",
    "background-color: #D2691E;"];

let material = ["Металл", "Пластик", "Медь"];
let warrantyCheckboxes = ["1 год", "10 лет", "20 лет"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("coating", coatingCheckboxes));
list.push(new Filter("width", typeProfiled));
list.push(new Filter("workWidth", pieceDrain));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));
list.push(new Filter("material", material));
list.push(new Filter("warranty", warrantyCheckboxes));


creatCheckboxes(list);
