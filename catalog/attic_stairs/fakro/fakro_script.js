import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["50x70", "50x80", "60x90", "60х94", "60х100", "60x120", "60x130", "60x140", "60x144",
    "70x80", "70x94", "70x100", "70x110", "70x120", "70x130", "70x140", "70x144", "86x130", "86x144", "92x130"];

let heightCheckboxes = ["280 см", "300 см", "305 см", "330 см",  "335 см", "366 см"];

let widthCheckboxes = ["30 мм",  "36 мм",  "56 мм","66 мм", "80 мм"];

let modelCheckboxes = ["LWS Plus", "LWK Plus", "LTK / LTK Energy", "LWL Extra", "LWT", "LMS", "LMK", "LMP", "LST",
    "LSF", "LML", "Komfort LWK", "LWS", "LDK", "LSZ"];

let list = [];

list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("coating", heightCheckboxes));
list.push(new Filter("width", modelCheckboxes));
list.push(new Filter("workWidth", widthCheckboxes));

creatCheckboxes(list);
