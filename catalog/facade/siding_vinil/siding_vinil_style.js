import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Docke", "Grand Line", "Mitten", "Ю-Пласт"];

let collectionCheckboxes = ["Docke D4,5D", "Docke D6S", "Docke Lux Bergart", "Docke Lux Rocky", "Docke Lux Ёлочка D5C",
    "Docke Lux Блок-Хаус", "Docke Premium Блок-Хаус", "Docke Premium Корабельный брус", "Docke Standart Ёлочка",
    "Docke Standart Корабельный брус", "Ю-Пласт Timberblock Ясень", "Ю-Пласт Timberblock Ель","Ю-Пласт Timberblock Пихта",
    "Ю-Пласт Timberblock Кедр", "Docke Lux Корабельный брус"];

let warrantyCheckboxes = ["30 лет", "50 лет", "пожизненная передаваемая гарантия"];

let titlesColorCheckboxes =["Tundra Береза", "Tundra Граб", "Tundra Граб", "Tundra Кедр", "Tundra Клен", "Tundra Рябина",
    "Tundra Ясень", "АСА Графит", "АСА Темный дуб", "Банан", "Бежевый", "Белый", "Ваниль", "Голубой", "Золотой песок",
    "Зрелый каштан", "Канадская береза", "Капучино", "Карамельный", "Кедр", "Кедровый орех", "Кешью", "Кокос", "Крем-брюле",
    "Кремовый", "Лимон", "Манго", "Миндаль", "Орех", "Пекан", "Персиковый", "Пломбир", "Рябина", "Салатовый", "Серый",
    "Слива", "Сливки", "Слоновая кость", "Темно-бежевый", "Фисташки", "Халва"];

let colorsCheckboxes = ["background-color: #C5C7C4;" ,"background-color: #B8BFAF;" ,"background-color: #B8BFAF;" ,"background-color: #A9A58C;" ,"background-color: #D1C6B4;" ,"background-color: #9E1B34;" ,"background-color: #A68E64;" ,"background-color: #4D4D4D;" ,
    "background-color: #3E3E3E;" ,"background-color: #FFE135;" ,"background-color: #F5F5DC;" ,"background-color: #FFFFFF;" ,"background-color: #F3E5AB;" ,"background-color: #00FFFF,#FFCC33;" ,"background-color: #8B2500;" ,
    "background-color: #D2B48C;" ,"background-color: #6F4E37;" ,"background-color: #FF7E00;" ,"background-color: #A52A2A;" ,"background-color: #90713A;" ,"background-color: #FFBD5F;" ,"background-color: #F8F8FF;" ,"background-color: #FFE5B4;" ,
    "background-color: #FFFDD0;", "background-color: #FFF700;" ,"background-color: #FF8243;" ,"background-color: #EFDECD;" ,"background-color: #8B6F4D;" ,"background-color: #C76114,#FFE5B4;" ,"background-color: #EEE8AA;" ,"background-color: #9E1B34;" ,"background-color: #7FFF00;",
    "background-color: #808080;" ,"background-color: #DDA0DD;", "background-color: #FFFACD;" ,"background-color: #FFEBCD;" ,"background-color: #8B6914;" ,"background-color: #BEF574;" ,"background-color: #E6BE8A;",
    "background-color: #BEF574;", "background-color: #E6BE8A;"];


let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("workWidth", collectionCheckboxes));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));
list.push(new Filter("warranty", warrantyCheckboxes));


creatCheckboxes(list);
