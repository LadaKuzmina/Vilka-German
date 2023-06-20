import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let companyTitles = ["Docke","Icopal","Katepal","RoofShield","Shinglas","Tegola"];
let nameColorsCheckboxes =["Авокадо", "Антрацит","Бархан", "Белый Тополь", "Бисквит", "Брауни", "Бук"];
let colorsCheckboxes = ["background-color: rgb(144,177,52)", "background-color:rgb(70,68,81)",
    "background-color:rgb(176,0,0)", "background-color:rgb(129, 120, 99)","background-color:rgb(255, 228, 196)",
    "background-color:rgb(150,75,0)", "background-color:rgb(177, 116, 77)"];
let warrantyCheckboxes = ["10 лет","20 лет", "25 лет", "30 лет", "35 лет", "50 лет","55 лет", "60 лет", "65 лет"];


let list = [];
list.push(new Filter("profiled", companyTitles));
list.push(new Filter("color", nameColorsCheckboxes, colorsCheckboxes));
list.push(new Filter("warranty", warrantyCheckboxes));

creatCheckboxes(list);
