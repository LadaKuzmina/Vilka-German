import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";


let nameCheckboxes = ["Бронзовый", "Молочный", "Прозрачный", "Серебристый"];

let colorsCheckboxes = ["background-color: #cd7f32;", "background-color: rgb(253,255,245);",
                    "", "background-color:rgb(201, 201, 201);"];

let list = [];
list.push(new Filter("profiled", nameCheckboxes, colorsCheckboxes));

creatCheckboxes(list);
