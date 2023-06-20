import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Fakro", "Velux"];

let coatingCheckboxes = ["1 камера", "2 камеры","3 камеры"];

let typeProfiled = ["FTP (CH)", "FTP-V (CH)", "FTP-V (CH) U5", "FWP U3",
    "FPP-V Max U3", "PTP-V U3",  "FTT U8 Thermo", "FTP-V U3 Z-Wave", "FTP-V U4", "FTS U2", "FTP-V U3",
    "FTS-V U4", "WLI", "FTZ-V U2", "FTZ-V U4", "FYP-V U3 proSky",  "FDY-V U3 Duet proSky",    "GLP 0073BIS",
    "GGU 0068",    "GLU 0061/0061B",  "Integra GGL/GGU",    "GLL 1061/1061B",    "GZR 3050/3050B", "GZR 3061/3061B",
    "GGL 3068", "GVT 0059", "VLT 1000", "FTP-V U3 WiFi", "FTS-V U2 (V22)", "FTP-V P2", "FTP-V U5", "FTP-V U3 (CU)",
    "FTU-V U3", "AHRD", "FTW-V U3", "FTP-V U5 WiFi",  "FTT U6 Thermo", "FPT Max U6",    "PPP-V Max U3",
    "PTP U3",    "PTP-V U4",    "PTP-V U5",    "PTP-V /GO U3",    "PTP-V /PI U3",    "DXC-C P2",    "DMC-C P2",
    "DEC-C P2",    "DXF-D U6", "DMF-D U6","DEF-D U6",  "GLU 0051", "GPU 0068", "GPL 3068",   "GZL 1051/1051B"];

let pieceDrain = ["45x55 см", "45x73 см", "46х55 см", "46х75 см", "54х75 см", "55х78 см", "55х98 см",
    "60x60 см", "60x90 см", "66х98 см", "66х118 см", "66х140 см", "78х98 см", "78х118 см", "78х140 см", "78х160 см",
    "78х180 см", "78х235 см", "78х255 см", "85x85 см", "86х86 см", "86х87 см", "94х118 см", "94х140 см", "94х160 см",
    "94х180 см", "94х206 см", "94х235 см", "94х255 см", "100x100 см", "100x150 см", "114х75 см", "114х95 см",
    "114х118 см",  "114х140 см", "120x120 см", "120x220 см", "134х98 см", "140x140 см"];

let material = ["Среднеповоротное",  "Комбинированное",  "Приподнятая ось открывания", "Распашное", "Окно-Люк"];

let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("coating", coatingCheckboxes));
list.push(new Filter("color", typeProfiled));
list.push(new Filter("width", pieceDrain));
list.push(new Filter("workWidth", material));


creatCheckboxes(list);
