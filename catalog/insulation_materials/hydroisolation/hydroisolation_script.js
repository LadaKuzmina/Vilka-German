import {Filter,  creatCheckboxes } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Гидроизоляция","Пароизоляция", "Ветроизоляция"];

let widthCheckboxes =["50", "51", "175", "200", "300"];

let densityCheckboxes = ["Пленка", "Мембрана", "Клей", "Лента", "Паста", "Рулон самоклеящийся"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", widthCheckboxes));
list.push(new Filter("warranty", densityCheckboxes));



creatCheckboxes(list);
