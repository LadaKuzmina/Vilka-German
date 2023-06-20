import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["CM Decking", "Grand Line", "MasterDeck"];

let widthCheckboxes = ["-х140х22","-х140х26", "2000х50х10",  "3000х130х9", "3000х135х25", "3000х138х23",
    "3000х140х22", "3000х140х24",  "3000х140х25",  "3000х140х26",  "3000х145х16",  "3000х150х10",
    "3000х150х9,5",  "3000х160х22", "3000х285х30", "3000х320х22",  "3000х70х9",  "4000х130х9",  "4000х140х20",
    "4000х140х22",  "4000х140х25", "4000х140х26",  "4000х145х16", "4000х160х22","4000х285х30",  "4000х320х22",  "6000х140х26"];

let nameCheckboxes = ["Антрацит", "Бежевый",  "Бронзовый", "Жёлтый", "Коричневый", "Синий"];

let colorsCheckboxes = ["background-color: #464451;", "background-color: #f5f5dc;", "background-color: #cd7f32;",
                "background-color: #ffff00;", "background-color: #964b00;", "background-color: #0000ff;"];

let warrantyCheckboxes = ["10 лет", "2 года", "20 лет", "25 лет", "30 лет", "50 лет"];

let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("color", nameCheckboxes, colorsCheckboxes));
list.push(new Filter("width", widthCheckboxes));
list.push(new Filter("warranty", warrantyCheckboxes));



creatCheckboxes(list);
