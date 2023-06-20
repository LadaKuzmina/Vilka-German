import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let width = ["0,45"];
let companyTitles = ["Монтеррей", "Ламонтерра", "Монтерроса", "Монтекристо", "Трамонтана", "Сицилия", "Андалузия",
    "Classic", "Kamea", "Kredo", "Kvinta Uno","Kvinta plus", "Kvinta plus 3D", "Modern", "Quadro profi", "Стандарт"];

let coatingTitles = ["Полиэстер", "AGNETA®", "Atlas X", "Drap", "CLOUDY", "Colorcoat Prisma", "Drap ST", "Drap TX",
    "Drap TwinColor", "GreenCoat Pural BT", "GreenCoat Pural BT, matt","GreenCoat, FAP ,Pural, BT, matt","NormanMP","PROTECT",
    "PurPro Matt","PurPro", "PURETAN", "PurLite Matt","PURMAN","Quarzit","Quarzit lite","Quarzit PRO Matt","Rooftop Matte","Satin","Satin Matt",
    "Satin Matt TX","VALORI","Velur X","VikingMP","Vimatt"];
let nameColorsCheckboxes = ["RAL 3005 Винно-красный","RAL 6005 Зеленый мох"," RAL 7004 Серый", "RAL 7016 Антрацитово-серый",
    "RAL 7024 Серый графит", "RAL 8004 Коричневая медь", "RAL 8017 Коричневый шоколад", "RAL 9003 Белый",
    "RAL 9005 Черный темный", "RR 32 Темно-коричневый"];
let colorsCheckboxes = ["background-color: rgb(94, 32, 40)", "background-color: rgb(15, 67, 54)", "background-color: rgb(158, 160, 161)",
                   "background-color: rgb(55, 63, 67)", "background-color: rgb(71, 74, 80)", "background-color: rgb(143, 78, 53)",
                   "background-color: rgb(68, 50, 45)", "background-color: rgb(244, 248, 244)", "background-color: rgb(10, 10, 13)",
    "background-color: rgb(76, 47, 38)"];
let warrantyCheckboxes = ["10 лет", "20 лет", "30 лет"];


let list = [];
list.push(new Filter("profiled", companyTitles));
list.push(new Filter("width", width));
list.push(new Filter("coating", coatingTitles));
//list.push(new Filter("color", nameColorsCheckboxes, colorsCheckboxes));
list.push(new Filter("warranty", warrantyCheckboxes));

creatCheckboxes(list);
