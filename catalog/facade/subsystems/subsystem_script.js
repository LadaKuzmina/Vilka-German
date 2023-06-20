import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Крепежный профиль Z-образный",  "Крепежный профиль Г-образный", "Крепежный профиль шляпный",
    "Кронштейн КК (стандарт)", "Удлинитель кронштейна"];

let widthCheckboxes = ["0,9", "1,2", "2"];

let warrantyCheckboxes = ["10 лет", "50 лет", "6 месяцев"];

let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", widthCheckboxes));
list.push(new Filter("warranty", warrantyCheckboxes));


creatCheckboxes(list);
