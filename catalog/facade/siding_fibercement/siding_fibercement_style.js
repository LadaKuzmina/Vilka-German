
import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Docke", "FineBer", "Grand Line", "Grayne", "VOX", "Альта-профиль", "Я-Фасад"];


let titlesColorCheckboxes =["RAL 1001 Бежевый", "RAL 1015 Светлая слоновая кость",  "RAL 5009 Лазурно-синий",  "RAL 7001 Серебристо-серый",
    "RAL 7024 Серый графит", "RAL 8024 Бежево-коричневый","RAL 9003 Белый", "RAL 9011 Кремово-белый"];

let colorsCheckboxes = ["background-color: #D1BC8A;" ,"background-color: #EADEBD;",
    "background-color: #2E5978;" ,"background-color: #8F999F;" ,"background-color: #9EA0A1;" ,
    "background-color: #79553C;" ,"background-color: #F4F8F4;" ,"background-color: #EFEBDC;"];


let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("coating", coatingCheckboxes));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));



creatCheckboxes(list);
