import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Манжета кровельная", "Антенный ворот", "Ворот трубы"];
let brandCheckboxes = ["Vilpe", "Viotto", "МеталлПрофиль"];
let widthCheckboxes = ["3.5", "6-50", "10–100", "100–230", "12-38", "12-90", "12-100", "12-460", "12-660",
"19–90","25","30","32-76","40","40–70","50","50-60","60","70","75, 110, 125, 160","76-152","80-140","110-125",
"110-155","110–170","125","130-140","150-175","160","175-250","200","200-250", "350-400", "500-575", "800-875"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("workWidth", brandCheckboxes));
list.push(new Filter("width", widthCheckboxes));



creatCheckboxes(list);
