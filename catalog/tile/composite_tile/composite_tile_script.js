import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Barсelona","Classic","Diamant", "Gallo", "Heritage", "Luxard Classiс","Luxard Roman", "MetroBond",
    "MetroClassic", "MetroRoman","MetroShake II", "Milano", "Mistral", "Palermo", "Roman", "Romana", "Shake", "Shingle", "Slate"];

let brandCheckboxes = ["Grand Line", "Luxard"];

let titlesColorCheckboxes =["Bark", "Chestnut", "Charcoal", "Dark silver", "Deep black", "Eclipse","Forest green", "Pepper", "Redwood",
    "Rosso","Sage", "Spanish red", "Terracotta", "Абсент", "Айрон-барк", "Алланит", "Американо", "Бордо", "Викториан", "Гранат", "Зеленый",
    "Какао", "Капучино", "Кленовый латте", "Коралл", "Коричневый", "Кофе", "Кофейно-серый", "Красно-черный","Красный","Малахит", "Мокко",
    "Мятный мокко", "Оникс", "Охра", "Песочный раф", "Пробка", "Пустыня", "Рустик", "Серый", "Скарлет","Сланцево-серый",  "Сланцевый",
    "Темно-зеленый","Терракотово-желтый", "Терракотовый", "Черный", "Шоколад", "Эспрессо"]

let colorsCheckboxes = ["background-color: #8A6642" ,"background-color: #CD5C5C" ,"background-color: #36454f" ,"background-color: #71706e" ,"background-color: #000000" ,"background-color: #3C3939" ,"background-color: #228B22" ,"background-color: #3E3A39" ,
    "background-color: #A45A52" ,"background-color: #D40000" ,"background-color: #BCB88A" ,"background-color: #E60026" ,"background-color: #E2725B" ,"background-color: #7FFF00" ,"background-color: #261414" ,"background-color: #007FFF" ,
    "background-color:#8B5742" ,"background-color: #800000" ,"background-color: #CDB7B5" ,"background-color: #FFD700" ,"background-color: #008000" ,"background-color: #D2691E" ,"background-color: #6F4E37" ,"background-color: #BDB76B" ,"background-color: #FF7F50"
    ,"background-color: #964B00" ,"background-color: #6F4E37" ,"background-color: #A67B5B" ,"background-color: #8B0000" ,"background-color: #FF0000" ,"background-color: #0BDA51" ,"background-color: #6F4E37" ,"background-color: #CDB7B5" ,
    "background-color: #353839" ,"background-color: #CC7722" ,"background-color: #C2B280" ,"background-color: #483C32" ,"background-color: #C19A6B" ,"background-color: #8B5A2B" ,"background-color: #808080" ,"background-color: #FF2400" ,
    "background-color: #708090" ,"background-color: #464646" ,"background-color: #006400" ,"background-color: #FFCC33" ,"background-color: #CC5533" ,"background-color: #000000" ,"background-color: #D2691E" ,"background-color: #61210F"
]


let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("width", brandCheckboxes));

list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));



creatCheckboxes(list);
