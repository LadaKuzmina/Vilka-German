import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";


let profiledCheckboxes = ["50x70","50x80","50x90","50x100", "60x80","60x90","60х100","60x110","60x120",
    "70x80","70x100","70x113", "70x110", "70x120"];

let heightCheckboxes = ["280 см", "300 см"];

let widthCheckboxes = ["36 мм","60 мм", "Не утепленная"];

let modelCheckboxes = ["LST", "LSF", "LSZ"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", heightCheckboxes));
list.push(new Filter("warranty", modelCheckboxes));
list.push(new Filter("workWidth", widthCheckboxes));

creatCheckboxes(list);
