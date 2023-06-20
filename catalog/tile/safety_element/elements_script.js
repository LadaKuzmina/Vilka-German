import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Планка снегозадержания","Дополнительная комплектация","Снегозадержатель",
    "Ограждение кровельное", "Переходной мостик", "Лестница фасадная", "Лестница кровельная"];

let brendCheckboxes = ["BORGE","ROOFRetail","Snegos", "МеталлПрофиль"];

let coatingCheckboxes = ["Полиэстер", "Полиэстер в пленке", "Цинк (Zn)","AGNETA®","CLOUDY®", "ECOSTEEL®",
                    "NormanMP®", "PURETAN®", "PURMAN", "VALORI", "VikingMP® E"];


let titlesColorCheckboxes = ["RAL 1014 Слоновая кость", "RAL 1015 Светлая слоновая кость", "RAL 1018 Желтый цинк",
    "RAL 2004 Оранжевый", "RAL 3003 Рубиново-красный",  "RAL 3005 Винно-красный", "RAL 3009 Оксид красный",  "RAL 3011 Коричнево-красный",
    "RAL 3020 Транспортный красный", "RAL 5001 Зелено-синий", "RAL 5005 Сигнальный синий", "RAL 5015 Небесно-синий",
    "RAL 5021 Водная синь", "RAL 6002 Лиственно-зелёный",  "RAL 6005 Зеленый мох", "RAL 6007 Бутылочно-зеленый", "RAL 6029 Зеленая мята",
    "RAL 7004 Серый", "RAL 7005 Мышиный", "RAL 7024 Серый графит", "RAL 7035 Ярко-серый",  "RAL 7047 Мягкий серый",  "RAL 8004 Коричневая медь",
    "RAL 8017 Коричневый шоколад", "RAL 8019 Серо-коричневый", "RAL 9002 Серо-белый",  "RAL 9003 Белый", "RAL 9005 Черный темный",
    "RAL 9006 Белый алюминий", "RAL 9010 Чистый белый", "RR 11 Темно-зеленый", "RR 29 Вишневый",  "RR 32 Темно-коричневый",
    "RR 35 Синий", "RR 750 Терракотовый", "Anticato Терракотовый", "Citrine Темно-синий", "Copper/Copper Медный/Медный",
    "Tourmalin Светло-зеленый металлик"];

let colorsCheckboxes = ["background-color: rgb(225,215,196);" ,"background-color: rgb(227,217,201);",
    "background-color: rgb(240,230,140);" ,"background-color: rgb(255,87,33);" ,"background-color: rgb(149,43,41);" ,
    "background-color: rgb(112,25,19);" ,"background-color: rgb(125,27,23);" ,"background-color: rgb(122,36,29);",
    "background-color: rgb(187,0,0);" ,"background-color: rgb(0,97,101);" ,"background-color: rgb(0,45,98);" ,
    "background-color: rgb(0,127,186);" ,"background-color: rgb(0,83,138);" ,"background-color: rgb(58,106,71);",
    "background-color: rgb(38,67,38);" ,"background-color: rgb(49,99,79);" ,"background-color: rgb(0,122,116);",
    "background-color: rgb(150,150,150);" ,"background-color: rgb(115,115,115);" ,"background-color: rgb(62,68,75);",
    "background-color: rgb(215,215,215);" ,"background-color: rgb(143,143,143);" ,"background-color: rgb(139,69,19);",
    "background-color: rgb(81,43,28);" ,"background-color: rgb(102,102,102);" ,"background-color: rgb(215,215,208);",
    "background-color: rgb(255,255,255);" ,"background-color: rgb(0,0,0);" ,"background-color: rgb(162,162,162);" ,
    "background-color: rgb(255,255,255);" ,"background-color: rgb(0,102,71);" ,"background-color: rgb(178,34,34);",
    "background-color: rgb(101,67,33);" ,"background-color: rgb(0,0,128);" ,"background-color: rgb(204,85,0);",
    "background-color: rgb(204,85,0);" ,"background-color: rgb(0,51,102);" ,"background-color: rgb(184,115,51);"
    ,"background-color: rgb(143,188,143);"];


let list = [];
list.push(new Filter("workWidth", profiledCheckboxes));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));

creatCheckboxes(list);
