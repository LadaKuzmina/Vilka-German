import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Fakro", "Velux"];

let modelCheckboxes = ["ESV", "EZV", "EDS 2000", "EDW 2000", "EWR 0000", "ESR 0000",  "BBX", "BDX", "XLC", "XDS",
    "XDP", "XDK", "XWT"];

let widthCheckboxes = ["55х78 см", "55х98 см", "66х98 см", "66х118 см", "66х140 см", "78х98 см", "78х118 см",
    "78х140 см", "94х118 см", "94х140 см", "114х140 см"];

let warrantyCheckboxes = ["10 лет", "5 лет"];

let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("coating", modelCheckboxes));
list.push(new Filter("width", widthCheckboxes));
list.push(new Filter("warranty", warrantyCheckboxes));


creatCheckboxes(list);
