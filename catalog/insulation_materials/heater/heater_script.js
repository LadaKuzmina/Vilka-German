import {Filter,  creatCheckboxes } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["KNAUF", "Knauf", "ROCKWOOL", "Ursa", "Знак Равенства", "Технониколь", "Эковер", "Экстрол"];


let widthCheckboxes =["20", "27", "45", "75"];

let densityCheckboxes = ["10", "10-13", "105", "11", "110", "115",
    "12",    "14",    "14,5",    "15",    "16",    "17",
    "18-20",    "20",    "20-35",    "21-35",    "23-28",    "25",    "26",    "28",    "28-35",    "30",    "32",
    "33",    "35",    "40",    "40-50",    "41",    "45",    "50",    "60",    "7",    "70",    "85",    "9-11"
];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", widthCheckboxes));
list.push(new Filter("warranty", densityCheckboxes));



creatCheckboxes(list);
