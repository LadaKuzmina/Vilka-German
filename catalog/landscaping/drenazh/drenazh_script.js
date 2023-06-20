import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Aquastok",  "Gidrolica"];

let widthCheckboxes = ["1000х135х100", "1000х136х20", "1000х148х120", "1000х148х138", "1000х148х150", "1000х148х180",
    "1000х148х70", "1000х148х55", "116", "117х27х10", "117х27х12", "119х28х14", "135х20", "135х22", "136х15",
    "148х423х500", "152х4х185", "166х200х295", "187х187х20", "195х195х197", "282х282х22", "285х285х21",  "295х297х295",
    "300х166х200",  "390х590х20",  "490х990х20",  "500х135х20",  "500х135х28",  "500х160х423"];


let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", widthCheckboxes));
creatCheckboxes(list);
