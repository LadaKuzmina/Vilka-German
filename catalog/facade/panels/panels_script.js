
import {Filter,  creatCheckboxes, } from "../../../scripts/adding_filters.js";

let profiledCheckboxes = ["Docke", "FineBer", "Grand Line", "Grayne", "VOX", "Альта-профиль", "Я-Фасад"];

let coatingCheckboxes = ["Docke Berg", "Docke Burg", "Docke Dufour", "Docke Edel", "Docke Fels", "Docke Flemish",
    "Docke Klinker", "Docke Stein", "Docke Stern", "Docke Сланец", "FineBer Камень крупный"," FineBer Кирпич облицовочный",
    "FineBer Камень дикий", "FineBer Камень природный", "FineBer Скала", "FineBer Сланец", "Kmew под кирпич",
    "SOLID-Brick", "SOLID-Sandstone", "SOLID-Stone", "Белый кедр", "Венеция", "Камень", "Камень Флорентийский",
    "Камень Шотландский", "Каньон", "Кирпич", "Кирпич Клинкерный", "Кирпич состаренный", "Кирпич-Антик",
    "Красный кедр", "Крымский сланец", "Неаполь", "Сибирская дранка", "Фагот","Фасадная плитка",
    "Шотландия", "Docke Алтай"];


let titlesColorCheckboxes =["Антик" ,"Антрацит" ,"Арктик" ,"Атакама" ,"Базальт" ,"Бежевый" ,"Белый" ,"Берилл" ,"Бронзовый",
    "Валь-Гардена" ,"Виллар" ,"Давос" ,"Дакота" ," Желтый жженый" ,"Земляной" ,"Золото" ,"Зёльден" ,"Инсбрук" ,"Ишгль",
    "Калахари" ,"Каракумы" ,"Корунд" ,"Красное пламя" ,"Красный жженый" ,"Кремовый" ," Кукурузный" ," Куршевель" ,"Лех",
    "Льняной" ,"Мармарис" ,"Марракеш" ,"Молочный" ,"Монте" ,"Навахо" ,"Натуральная шерсть" ,"Осенний лес" ,"Перламутровый",
    "Песочный" ,"Платиновый" ,"Пшеничный" ," Ржаной" ,"Родонит" ,"Родос" ,"Сахара" ,"Светло-серый" ,"Серая галька" ,"Слоновая кость",
    "Темно-бежевый" ,"Темный мрамор" ,"Темный орех" ," Терракотовый" ," Хрусталь"  ," Циркон" ," Шамони" ,"Шоколад"];

let colorsCheckboxes = ["background-color: #C3B091;" ,"background-color: #464646;" ,"background-color: #D8E9E9;" ,"background-color: #C1B7A4;" ,
    "background-color: #4E4E4E;" ,"background-color: #F5F5DC;" ,"background-color: #FFFFFF;" ,"background-color: #D0FFFE;" ,
    "background-color: #CD7F32;" ,"background-color: #F2C3B2;" ,"background-color: #FFE4C4;" ,"background-color: #F0EAE2;" ,"background-color: #6D4C41;" ,"background-color: #FFD700;" ,"background-color: #8B4513;" ,"background-color: #FFD700;"
    ,"background-color: #F8D568;" ,"background-color: #ADD8E6;" ,"background-color: #F5DEB3;" ,"background-color: #FFE772;" ,"background-color: #C2B280;" ,"background-color: #FF7F50;" ,"background-color: #FF4500;" ,"background-color: #A52A2A;" ,
    "background-color: #FFFDD0;" ,"background-color: #FBEC5D;" ,"background-color: #BFC1C2;" ,"background-color: #8B0000;" ,"background-color: #FAF0E6;" ,"background-color: #87CEEB;" ,"background-color: #FFE4C4;" ,"background-color: #F0FFF0;",
    "background-color: #CDB7B5;" ,"background-color: #FFDEAD;" ,"background-color: #F5DEB3;" ,"background-color: #8B0000;" ,"background-color: #EAE0C8;" ,"background-color: #F5DEB3;" ,"background-color: #E5E4E2;" ,"background-color: #F5DEB3;"
    ,"background-color: #805050;" ,"background-color: #AA4069;" ,"background-color: #1F497D;" ,"background-color: #F0DB7D,#D3D3D3;" ,"background-color: #808080;" ,"background-color: #FFFFF0;" ,"background-color: #CDB7B5;" ,"background-color: #8B0000;"
    ,"background-color: #8B0000;" ,"background-color: #CC5533;" ,"background-color: rgb(204, 78, 92);" ,"background-color: #A78A8A;" ,"background-color: #007FFF;" ,"background-color: #D2691E;", "background-color: #55280c;"
];

let warrantyCheckboxes = ["10 лет", "50 лет"];

let list = [];
list.push(new Filter("profiled", profiledCheckboxes));
list.push(new Filter("coating", coatingCheckboxes));
list.push(new Filter("color", titlesColorCheckboxes, colorsCheckboxes));
list.push(new Filter("warranty", warrantyCheckboxes));



creatCheckboxes(list);
