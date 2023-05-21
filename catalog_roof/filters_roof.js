import {Filter,  creatCheckboxes, } from "../scripts/adding_filters.js";

let profiledCheckboxes = ["МП-20", "С-21", "МП-35", "НС-35", "С-44", "Н-60", "Н-75", "Н-114", "МП-20 Поликарбонат"];
let widthCheckboxes = ["0,35 и менее", "0,4", "0,45", "0,5", "0,55", "0,6", "0,65", "0,7", "0,75", "0,8", "0,9", "1"];
let coatingCheckboxes = ["Полиэстер", "Цинк (Zn)", "AGNETA®", "Atlas X", "Drap", "CLOUDY®", "Drap ST", "ECOSTEEL®", "GreenCoat Pural BT", "GreenCoat Pural BT, matt", "NormanMP®", "PurPro Matt", "PURETAN®", "PurLite Matt", "PURMAN®", "Quarzit", "Quarzit lite", "Quarzit PRO Matt", "Rooftop Matte", "Satin", "Satin Matt", "VALORI", "Velur X", "VikingMP"];
let workWidthCheckboxes = ["1000", "1035", "1100", "600", "750", "845"];
let warrantyCheckboxes = ["10 лет", "20 лет", "25 лет", "30 лет", "50 лет"];
let titlesColorCheckboxes =["RAL 1014 Слоновая кость", "RAL 1015 Светлая cлоновая кость", "RAL 1018 Желтый цинк",
    "RAL 2004 Оранжевый", "RAL 3003 Рубиново-красный", "RAL 3005 Винно-красный"]
let colorsCheckboxes = ["background-color: rgb(222, 208, 159)", "background-color: rgb(234, 222, 189)", "background-color: rgb(243, 224, 59)",
    "background-color: rgb(231, 91, 18)", "background-color: rgb(141, 29, 44)", "background-color: rgb(94, 32, 40)"]


let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", widthCheckboxes));
list.push(new Filter("coating", coatingCheckboxes));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));
list.push(new Filter("workWidth", workWidthCheckboxes));
list.push(new Filter("warranty", warrantyCheckboxes));


creatCheckboxes(list);
