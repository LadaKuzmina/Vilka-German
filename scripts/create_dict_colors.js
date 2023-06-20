let dict = new Map();

let titles1 = ["Антик" ,"Антрацит" ,"Арктик" ,"Атакама" ,"Базальт" ,"Бежевый" ,"Белый" ,"Берилл" ,"Бронзовый",
    "Валь-Гардена" ,"Виллар" ,"Давос" ,"Дакота" ," Желтый жженый" ,"Земляной" ,"Золото" ,"Зёльден" ,"Инсбрук" ,"Ишгль",
    "Калахари" ,"Каракумы" ,"Корунд" ,"Красное пламя" ,"Красный жженый" ,"Кремовый" ,"Кукурузный" ,"Куршевель" ,"Лех",
    "Льняной" ,"Мармарис" ,"Марракеш" ,"Молочный" ,"Монте" ,"Навахо" ,"Натуральная шерсть" ,"Осенний лес" ,"Перламутровый",
    "Песочный" ,"Платиновый" ,"Пшеничный" ,"Ржаной" ,"Родонит" ,"Родос" ,"Сахара" ,"Светло-серый" ,"Серая галька" ,"Слоновая кость",
    "Темно-бежевый" ,"Темный мрамор" ,"Темный орех" ,"Терракотовый" ,"Хрусталь"  ,"Циркон" ,"Шамони" ,"Шоколад"];
let colors1 = ["background-color: #C3B091;" ,"background-color: #464646;" ,"background-color: #D8E9E9;" ,"background-color: #C1B7A4;" ,
    "background-color: #4E4E4E;" ,"background-color: #F5F5DC;" ,"background-color: #FFFFFF;" ,"background-color: #D0FFFE;" ,
    "background-color: #CD7F32;" ,"background-color: #F2C3B2;" ,"background-color: #FFE4C4;" ,"background-color: #F0EAE2;" ,"background-color: #6D4C41;" ,"background-color: #FFD700;" ,"background-color: #8B4513;" ,"background-color: #FFD700;"
    ,"background-color: #F8D568;" ,"background-color: #ADD8E6;" ,"background-color: #F5DEB3;" ,"background-color: #FFE772;" ,"background-color: #C2B280;" ,"background-color: #FF7F50;" ,"background-color: #FF4500;" ,"background-color: #A52A2A;" ,
    "background-color: #FFFDD0;" ,"background-color: #FBEC5D;" ,"background-color: #BFC1C2;" ,"background-color: #8B0000;" ,"background-color: #FAF0E6;" ,"background-color: #87CEEB;" ,"background-color: #FFE4C4;" ,"background-color: #F0FFF0;",
    "background-color: #CDB7B5;" ,"background-color: #FFDEAD;" ,"background-color: #F5DEB3;" ,"background-color: #8B0000;" ,"background-color: #EAE0C8;" ,"background-color: #F5DEB3;" ,"background-color: #E5E4E2;" ,"background-color: #F5DEB3;"
    ,"background-color: #805050;" ,"background-color: #AA4069;" ,"background-color: #1F497D;" ,"background-color: #F0DB7D,#D3D3D3;" ,"background-color: #808080;" ,"background-color: #FFFFF0;" ,"background-color: #CDB7B5;" ,"background-color: #8B0000;"
    ,"background-color: #8B0000;" ,"background-color: #CC5533;" ,"background-color: rgb(204, 78, 92);" ,"background-color: #A78A8A;" ,"background-color: #007FFF;" ,"background-color: #D2691E;", "background-color: #55280c;"]

let titles2 = ["RAL 1001 Бежевый", "RAL 1015 Светлая слоновая кость",  "RAL 5009 Лазурно-синий",  "RAL 7001 Серебристо-серый",
    "RAL 7024 Серый графит", "RAL 8024 Бежево-коричневый","RAL 9003 Белый", "RAL 9011 Кремово-белый"];
let colors2 = ["background-color: #D1BC8A;" ,"background-color: #EADEBD;",
    "background-color: #2E5978;" ,"background-color: #8F999F;" ,"background-color: #9EA0A1;" ,
    "background-color: #79553C;" ,"background-color: #F4F8F4;" ,"background-color: #EFEBDC;"];
let titles3 = ["RAL 1014 Слоновая кость", "RAL 1015 Светлая слоновая кость", "RAL 1018 Желтый цинк",
    "RAL 2004 Оранжевый", "RAL 3003 Рубиново-красный",  "RAL 3005 Винно-красный", "RAL 3009 Оксид красный",  "RAL 3011 Коричнево-красный",
    "RAL 3020 Транспортный красный", "RAL 5001 Зелено-синий", "RAL 5005 Сигнальный синий", "RAL 5015 Небесно-синий",
    "RAL 5021 Водная синь", "RAL 6002 Лиственно-зелёный",  "RAL 6005 Зеленый мох", "RAL 6007 Бутылочно-зеленый", "RAL 6029 Зеленая мята",
    "RAL 7004 Серый", "RAL 7005 Мышиный", "RAL 7024 Серый графит", "RAL 7035 Ярко-серый",  "RAL 7047 Мягкий серый",  "RAL 8004 Коричневая медь",
    "RAL 8017 Коричневый шоколад", "RAL 8019 Серо-коричневый", "RAL 9002 Серо-белый",  "RAL 9003 Белый", "RAL 9005 Черный темный",
    "RAL 9006 Белый алюминий", "RAL 9010 Чистый белый", "RR 11 Темно-зеленый", "RR 29 Вишневый",  "RR 32 Темно-коричневый",
    "RR 35 Синий", "RR 750 Терракотовый"];
let colors3=["background-color: rgb(225,215,196);" ,"background-color: rgb(227,217,201);",
    "background-color: rgb(240,230,140);" ,"background-color: rgb(255,87,33);" ,"background-color: rgb(149,43,41);" ,
    "background-color: rgb(112,25,19);" ,"background-color: rgb(125,27,23);" ,"background-color: rgb(122,36,29);",
    "background-color: rgb(187,0,0);" ,"background-color: rgb(0,97,101);" ,"background-color: rgb(0,45,98);" ,
    "background-color: rgb(0,127,186);" ,"background-color: rgb(0,83,138);" ,"background-color: rgb(58,106,71);",
    "background-color: rgb(38,67,38);" ,"background-color: rgb(49,99,79);" ,"background-color: rgb(0,122,116);",
    "background-color: rgb(150,150,150);" ,"background-color: rgb(115,115,115);" ,"background-color: rgb(62,68,75);",
    "background-color: rgb(215,215,215);" ,"background-color: rgb(143,143,143);" ,"background-color: rgb(139,69,19);",
    "background-color: rgb(81,43,28);" ,"background-color: rgb(102,102,102);" ,"background-color: rgb(215,215,208);",
    "background-color: rgb(255,255,255);" ,"background-color: rgb(0,0,0);" ,"background-color: rgb(162,162,162);" ,
    "background-color: rgb(255,255,255);" ,"background-color: rgb(0,102,71);" ,"background-color: rgb(178,34,34);",
    "background-color: rgb(101,67,33);" ,"background-color: rgb(0,0,128);" ,"background-color: rgb(204,85,0);"];

let titles4 = ["Tundra Береза", "Tundra Граб", "Tundra Граб", "Tundra Кедр", "Tundra Клен", "Tundra Рябина",
    "Tundra Ясень", "АСА Графит", "АСА Темный дуб", "Банан", "Бежевый", "Белый", "Ваниль", "Голубой", "Золотой песок",
    "Зрелый каштан", "Канадская береза", "Капучино", "Карамельный", "Кедр", "Кедровый орех", "Кешью", "Кокос", "Крем-брюле",
    "Кремовый", "Лимон", "Манго", "Миндаль", "Орех", "Пекан", "Персиковый", "Пломбир", "Рябина", "Салатовый", "Серый",
    "Слива", "Сливки", "Слоновая кость", "Темно-бежевый", "Фисташки", "Халва"];

let colors4 = ["background-color: #C5C7C4;" ,"background-color: #B8BFAF;" ,"background-color: #B8BFAF;" ,"background-color: #A9A58C;" ,"background-color: #D1C6B4;" ,"background-color: #9E1B34;" ,"background-color: #A68E64;" ,"background-color: #4D4D4D;" ,
    "background-color: #3E3E3E;" ,"background-color: #FFE135;" ,"background-color: #F5F5DC;" ,"background-color: #FFFFFF;" ,"background-color: #F3E5AB;" ,"background-color: #00FFFF,#FFCC33;" ,"background-color: #8B2500;" ,
    "background-color: #D2B48C;" ,"background-color: #6F4E37;" ,"background-color: #FF7E00;" ,"background-color: #A52A2A;" ,"background-color: #90713A;" ,"background-color: #FFBD5F;" ,"background-color: #F8F8FF;" ,"background-color: #FFE5B4;" ,
    "background-color: #FFFDD0;", "background-color: #FFF700;" ,"background-color: #FF8243;" ,"background-color: #EFDECD;" ,"background-color: #8B6F4D;" ,"background-color: #C76114;","background-color:#FFE5B4;","background-color: #EEE8AA;" ,"background-color: #9E1B34;" ,"background-color: #7FFF00;",
    "background-color: #808080;" ,"background-color: #DDA0DD;", "background-color: #FFFACD;" ,"background-color: #FFEBCD;" ,"background-color: #8B6914;" ,"background-color: #BEF574;" ,"background-color: #E6BE8A;",
    "background-color: #BEF574;", "background-color: #E6BE8A;"];

let titles5 = ["Антрацит", "Глубокий черный","Графит","Зеленая ель", "Каштан", "Кедр", "Королевский серый",
    "Красная лава", "Красная осень", "Красное пламя", "Красный бук", "Медный", "Натуральный красный","Серая галька",
    "Синий бриллиант", "Темно-коричневый", "Тик", "Черный бриллиант", "Черный вулкан", "Черный кристалл"];
let colors5 =["background-color: rgb(70,68,81);", "background-color: rgb(10,10,13);", "background-color: rgb(71,74,81);",
    "background-color: rgb(42,92,3);", "background-color: rgb(116,40,2);", "background-color: rgb(56,79,47);",
    "background-color: rgb(165,165,165);", "background-color: rgb(207,16,32);", "background-color: rgb(240,81,51);",
    "background-color: rgb(134,40,46);", "background-color: rgb(178,34,34);", "background-color: rgb(10,10,13);",
    "background-color: rgb(184,115,51);", "background-color: rgb(143,29,33);", "background-color: rgb(139,140,122);",
    "background-color: rgb(50,109,220);", "background-color: rgb(101,67,33);", "background-color: rgb(171,137,83);",
    "background-color: rgb(31,31,31);", "background-color: rgb(16,18,29);", "background-color: rgb(10,10,10);"];

let titles6 = ["Bark", "Chestnut", "Charcoal", "Dark silver", "Deep black", "Eclipse","Forest green", "Pepper", "Redwood",
    "Rosso","Sage", "Spanish red", "Terracotta", "Абсент", "Айрон-барк", "Алланит", "Американо", "Бордо", "Викториан", "Гранат", "Зеленый",
    "Какао", "Капучино", "Кленовый латте", "Коралл", "Коричневый", "Кофе", "Кофейно-серый", "Красно-черный","Красный","Малахит", "Мокко",
    "Мятный мокко", "Оникс", "Охра", "Песочный раф", "Пробка", "Пустыня", "Рустик", "Серый", "Скарлет","Сланцево-серый",  "Сланцевый",
    "Темно-зеленый","Терракотово-желтый", "Терракотовый", "Черный", "Шоколад", "Эспрессо"];

let colors6 = ["background-color: #8A6642" ,"background-color: #CD5C5C" ,"background-color: #36454f" ,"background-color: #71706e" ,"background-color: #000000" ,"background-color: #3C3939" ,"background-color: #228B22" ,"background-color: #3E3A39" ,
    "background-color: #A45A52" ,"background-color: #D40000" ,"background-color: #BCB88A" ,"background-color: #E60026" ,"background-color: #E2725B" ,"background-color: #7FFF00" ,"background-color: #261414" ,"background-color: #007FFF" ,
    "background-color:#8B5742" ,"background-color: #800000" ,"background-color: #CDB7B5" ,"background-color: #FFD700" ,"background-color: #008000" ,"background-color: #D2691E" ,"background-color: #6F4E37" ,"background-color: #BDB76B" ,"background-color: #FF7F50"
    ,"background-color: #964B00" ,"background-color: #6F4E37" ,"background-color: #A67B5B" ,"background-color: #8B0000" ,"background-color: #FF0000" ,"background-color: #0BDA51" ,"background-color: #6F4E37" ,"background-color: #CDB7B5" ,
    "background-color: #353839" ,"background-color: #CC7722" ,"background-color: #C2B280" ,"background-color: #483C32" ,"background-color: #C19A6B" ,"background-color: #8B5A2B" ,"background-color: #808080" ,"background-color: #FF2400" ,
    "background-color: #708090" ,"background-color: #464646" ,"background-color: #006400" ,"background-color: #FFCC33" ,"background-color: #CC5533" ,"background-color: #000000" ,"background-color: #D2691E" ,"background-color: #61210F"]

let titles7 = ["Авокадо", "Антрацит","Бархан", "Белый Тополь", "Бисквит", "Брауни", "Бук"];
let colors7 = ["background-color: rgb(144,177,52)", "background-color:rgb(70,68,81)",
    "background-color:rgb(176,0,0)", "background-color:rgb(129, 120, 99)","background-color:rgb(255, 228, 196)",
    "background-color:rgb(150,75,0)", "background-color:rgb(177, 116, 77)"];

let titles8 = ["RAL 3005 Винно-красный","RAL 6005 Зеленый мох"," RAL 7004 Серый", "RAL 7016 Антрацитово-серый",
    "RAL 7024 Серый графит", "RAL 8004 Коричневая медь", "RAL 8017 Коричневый шоколад", "RAL 9003 Белый",
    "RAL 9005 Черный темный", "RR 32 Темно-коричневый"];
let colors8 = ["background-color: rgb(94, 32, 40)", "background-color: rgb(15, 67, 54)", "background-color: rgb(158, 160, 161)",
    "background-color: rgb(55, 63, 67)", "background-color: rgb(71, 74, 80)", "background-color: rgb(143, 78, 53)",
    "background-color: rgb(68, 50, 45)", "background-color: rgb(244, 248, 244)", "background-color: rgb(10, 10, 13)",
    "background-color: rgb(76, 47, 38)"];
let titles9 = ["RAL 1014 Слоновая кость", "RAL 1015 Светлая слоновая кость", "RAL 1018 Желтый цинк",
    "RAL 2004 Оранжевый", "RAL 3003 Рубиново-красный",  "RAL 3005 Винно-красный", "RAL 3009 Оксид красный",  "RAL 3011 Коричнево-красный",
    "RAL 3020 Транспортный красный", "RAL 5001 Зелено-синий", "RAL 5005 Сигнальный синий", "RAL 5015 Небесно-синий",
    "RAL 5021 Водная синь", "RAL 6002 Лиственно-зелёный",  "RAL 6005 Зеленый мох", "RAL 6029 Зеленая мята",
    "RAL 7004 Серый", "RAL 7005 Мышиный", "RAL 7024 Серый графит", "RAL 7035 Ярко-серый",  "RAL 8004 Коричневая медь",
    "RAL 8017 Коричневый шоколад", "RAL 8019 Серо-коричневый", "RAL 9002 Серо-белый",  "RAL 9003 Белый", "RAL 9005 Черный темный",
    "RAL 9006 Белый алюминий", "RAL 9010 Чистый белый", "RR 11 Темно-зеленый", "RR 29 Вишневый",  "RR 32 Темно-коричневый",
    "RR 35 Синий", "RR 750 Терракотовый", "Anticato Терракотовый", "Citrine Темно-синий", "Copper/Copper Медный/Медный",
    "Tourmalin Светло-зеленый металлик"];
let colors9 = ["background-color: rgb(225,215,196);" ,"background-color: rgb(227,217,201);",
    "background-color: rgb(240,230,140);" ,"background-color: rgb(255,87,33);" ,"background-color: rgb(149,43,41);" ,
    "background-color: rgb(112,25,19);" ,"background-color: rgb(125,27,23);" ,"background-color: rgb(122,36,29);",
    "background-color: rgb(187,0,0);" ,"background-color: rgb(0,97,101);" ,"background-color: rgb(0,45,98);" ,
    "background-color: rgb(0,127,186);" ,"background-color: rgb(0,83,138);" ,"background-color: rgb(58,106,71);",
    "background-color: rgb(38,67,38);" ,"background-color: rgb(49,99,79);" ,"background-color: rgb(0,122,116);",
    "background-color: rgb(150,150,150);" ,"background-color: rgb(115,115,115);" ,"background-color: rgb(62,68,75);",
    "background-color: rgb(215,215,215);" ,"background-color: rgb(143,143,143);" ,"background-color: rgb(139,69,19);",
    "background-color: rgb(81,43,28);" ,"background-color: rgb(102,102,102);" ,"background-color: rgb(215,215,208);",
    "background-color: rgb(255,255,255);" ,"background-color: rgb(0,0,0);" ,"background-color: rgb(162,162,162);" ,
    "background-color: rgb(255,255,255);" ,"background-color: rgb(0,102,71);" ,"background-color: rgb(178,34,34);",
    "background-color: rgb(101,67,33);" ,"background-color: rgb(0,0,128);" ,"background-color: rgb(204,85,0);",
    "background-color: rgb(204,85,0);" ,"background-color: rgb(0,51,102);" ,"background-color: rgb(184,115,51);"
    ,"background-color: rgb(143,188,143);"];
function addInDict(titlesColorCheckboxes, colorsCheckboxes){
    for (let i = 0; i < colorsCheckboxes.length; i++){
        if (!dict.has(titlesColorCheckboxes[i])){
            let str = colorsCheckboxes[i];
            if (!str.includes(";"))
                str = colorsCheckboxes[i] + ";";
            let title = titlesColorCheckboxes[i];
            dict.set(title, str);
        }
    }
}
let titles10 =["RAL 1014 Слоновая кость",  "RAL 3005 Винно-красный",  "RAL 3011 Коричнево-красный",
    "RAL 6005 Зеленый мох", "RAL 7004 Серый", "RAL 7024 Серый графит", "RAL 8004 Коричневая медь",
    "RAL 8017 Коричневый шоколад",
    "RAL 9003 Белый", "RAL 9005 Черный темный", "RAL 9006 Бело-алюминиевый","RAL 9010 Чистый белый", "RAL Al-Zn",
    "RR 11 Темно-зеленый", "RR 29 Вишневый",  "RR 32 Темно-коричневый", "RR 33 Черный", "Белый","Бордо", "Графит",
    "Графитовый","Зеленый", "Коричневый", "Красный", "Медь", "Пломбир", "Р363 вишневый",
    "Серый", "Темно-коричневый", "Цинк", "Черный","Шоколад"];

let colors10 = ["background-color: rgb(225,215,196);", "background-color: rgb(112,25,19);",
    "background-color: rgb(122,36,29);", "background-color: rgb(38,67,38);", "background-color: rgb(150,150,150);",
    "background-color: #9EA0A1;", "background-color: rgb(139,69,19);", "background-color: rgb(81,43,28);",
    "background-color: #F4F8F4;", "background-color: rgb(0,0,0);", "background-color: #ffffff;", "background-color: rgb(255,255,255);",
    "undefined", "background-color: rgb(0,102,71);", "background-color: rgb(178,34,34);",
    "background-color: rgb(101,67,33);", "background-color:#0f1412;", "background-color: #FFFFFF;", "background-color: #800000;",
    "background-color: rgb(71,74,81);", "background-color: rgb(71,74,81);", "background-color: #008000;", "background-color: #964B00;",
    "background-color: #FF0000;", "background-color: rgb(184,115,51);", "background-color: #9E1B34;", "background-color: rgb(222,12,98);",
    "background-color: #808080;", "background-color: rgb(50,109,220);", "background-color: rgb(170,170,175);", "background-color: #000000;",
    "background-color: #D2691E;"];

let titles11 =["RAL 3005 Винно-красный", "RAL 5005 Сигнальный синий",  "RAL 6005 Зеленый мох",
    "RAL 7024 Серый графит", "RAL 8004 Коричневая медь", "RAL 8017 Коричневый шоколад",  "RAL 8019 Серо-коричневый",
    "RAL 9005 Черный темный",  "RR 11 Темно-зеленый",  "RR 23 Темно-серый",  "RR 28 Темно-вишневый", "RR 32 Темно-коричневый",
    "RR 750 Терракотовый", "Зеленый", "Кирпичный", "Коричневый", "Красный", "Светло-серый", "Серый",  "Синий", "Темно-серый",
    "Черный", "Шоколад"];

let colors11 = ["background-color: rgb(112,25,19);", "background-color: rgb(0,45,98);", "background-color: rgb(38,67,38);",
    "background-color: #9EA0A1;", "background-color: rgb(139,69,19);", "background-color: rgb(81,43,28);",
    "background-color: rgb(102,102,102);", "background-color: rgb(0,0,0);", "background-color: rgb(0,102,71);",
    "background-color: rgb(42, 58, 65);", "background-color: rgb(100, 17, 21);", "background-color: rgb(101,67,33);",
    "background-color: rgb(204,85,0);",   "background-color: #008000;", "background-color: #884535;",
    "background-color:#964B00;", "background-color: #FF0000;",
    "background-color: #808080;", "background-color: #808080;", "background-color: #0000ff",
    "background-color: rgb(169,169,169);", "background-color: #000000;", "background-color: #D2691E;"];
let titles12 =["Зелёный", "Кирпичный", "Коричневый", "Красный", "Светло-серый", "Серый", "Черный"];

let colors12 = ["background-color: #008000;", "background-color: #884535;", "background-color: #964B00;",
    "background-color: #FF0000;", "background-color: #808080;", "background-color: #808080;",
    "background-color:#000000;"];
let titles13 = ["RAL 1014 Слоновая кость","RAL 1015 Светлая слоновая кость", "RAL 1018 Желтый цинк",  "RAL 1019 Серо-бежевый",
    "RAL 2004 Оранжевый", "RAL 3003 Рубиново-красный", "RAL 3005 Винно-красный", "RAL 3005/3005 Винно-красный двухсторонний",
    "RAL 3009 Оксид красный", "RAL 3011 Коричнево-красный", "RAL 3020 Транспортный красный", "RAL 5001 Зелено-синий",
    "RAL 5002 Ультрамариново-синий",  "RAL 5005 Сигнальный синий",    "RAL 5015 Небесно-синий", "RAL 5021 Водная синь",
    "RAL 5021 Водная синь",  "RAL 6002 Лиственно-зелёный", "RAL 6005 Зеленый мох", "RAL 6005/6005 Зеленый мох двухсторонний",
    "RAL 6007 Бутылочно-зеленый", "RAL 6011 Резедово-зелёный", "RAL 6019 Зеленая пастель", "RAL 6029 Зеленая мята",
    "RAL 7004 Серый",  "RAL 7005 Мышиный","RAL 7016 Антрацитово-серый", "RAL 7024 Серый графит",
    "RAL 7024/7024 Серый графит/Серый графит", "RAL 7035 Ярко-серый",  "RAL 7039 Кварцевый серый", "RAL 7047 Мягкий серый",
    "RAL 8004 Коричневая медь", "RAL 8017 Коричневый шоколад",    "RAL 8017/8017 Коричневый шоколад двухсторонний",
    "RAL 8019 Серо-коричневый",   "RAL 9002 Серо-белый", "RAL 9003 Белый", "RAL 9005 Черный темный",  "RAL 9006 Бело-алюминиевый",
    "RAL 9006 Белый алюминий","RAL 9010 Чистый белый",  "RR 11 Темно-зеленый",  "RR 21 Светло-серый", "RR 23 Темно-серый",
    "RR 29 Вишневый", "RR 32 Темно-коричневый", "RR 33 Черный", "RR 35 Синий", "RR 750 Терракотовый",  "RR 887"];
let colors13 = ["background-color: rgb(225,215,196);", "background-color: #EADEBD;", "background-color: rgb(240,230,140);",
    "background-color: #D9C2A4;",    "background-color: rgb(255,87,33);", "background-color: rgb(149,43,41);", "background-color: rgb(112,25,19);",
    "background-color:#6C1B2A;", "background-color: rgb(125,27,23);", "background-color: rgb(122,36,29);", "background-color: rgb(187,0,0);",
    "background-color: rgb(0,97,101);", "background-color:#0D1C33;", "background-color: rgb(0,45,98);", "background-color: rgb(0,127,186);",
    "background-color: rgb(0,83,138);", "background-color: rgb(0,83,138);", "background-color: rgb(58,106,71);",
    "background-color: rgb(38,67,38);", "background-color:#0F4336;", "background-color: rgb(49,99,79);", "background-color:#587246;", "background-color:#BDECB6;",
    "background-color: rgb(0,122,116);", "background-color: rgb(150,150,150);", "background-color: rgb(115,115,115);",
    "background-color: rgb(55, 63,67);", "background-color: #9EA0A1;", "background-color: #484B52;",
    "background-color: rgb(215,215,215);", "background-color: #B9B7BD;", "background-color: rgb(143,143,143);",
    "background-color: rgb(139,69,19);", "background-color: rgb(81,43,28);", "background-color:#45322E;",
    "background-color: rgb(102,102,102);", "background-color: rgb(215,215,208);", "background-color: #F4F8F4;",
    "background-color: rgb(0,0,0);", "background-color: #ffffff;", "background-color: rgb(162,162,162);", "background-color: rgb(255,255,255);",
    "background-color: rgb(0,102,71);", "background-color: rgb(0, 51, 0);", "background-color: rgb(192, 192, 192);",
    "background-color: rgb(128, 128, 128)", "background-color: rgb(128, 0, 0)","background-color: rgb(51, 25, 0);",
    "background-color: rgb(0, 0, 0);","background-color: rgb(0, 0, 255);", "background-color: rgb(204, 85, 51);",
    "background-color: rgb(255, 204, 153);"];

addInDict(titles6, colors6);
addInDict(titles13, colors13);
addInDict(titles1, colors1);
addInDict(titles2, colors2);
addInDict(titles3, colors3);
addInDict(titles4, colors4);
addInDict(titles5, colors5)
addInDict(titles7, colors7);
addInDict(titles8, colors8);
addInDict(titles9, colors9);
addInDict(titles10, colors10);
addInDict(titles11, colors11);
addInDict(titles12, colors12);


let titles =["RAL 1014 Слоновая кость", "RAL 1015 Светлая слоновая кость",  "RAL 1018 Желтый цинк",
    "RAL 1035 Перламутрово-бежевый", "RAL 2004 Оранжевый",  "RAL 3003 Рубиново-красный", "RAL 3005 Винно-красный",
    "RAL 3009 Оксид красный",  "RAL 3011 Коричнево-красный",  "RAL 3020 Транспортный красный",   "RAL 5001 Зелено-синий",
    "RAL 5005 Сигнальный синий", "RAL 5015 Небесно-синий", "RAL 5021 Водная синь", "RAL 6002 Лиственно-зелёный",
    "RAL 6005 Зеленый мох",  "RAL 6007 Бутылочно-зеленый",  "RAL 6019 Зеленая пастель",  "RAL 6020 Хромовый зелёный",
    "RAL 6029 Зеленая мята", "RAL 7004 Серый",  "RAL 7005 Мышиный",  "RAL 7016 Антрацитово-серый",    "RAL 7024 Серый графит",
    "RAL 7035 Ярко-серый",  "RAL 7047 Мягкий серый",  "RAL 8004 Коричневая медь",  "RAL 8017 Коричневый шоколад",
    "RAL 8019 Серо-коричневый", "RAL 9002 Серо-белый",  "RAL 9003 Белый",  "RAL 9005 Черный темный",  "RAL 9006 Бело-алюминиевый",
    "RAL 9006 Белый алюминий",  "RAL 9010 Чистый белый",  "RR 11 Темно-зеленый",   "RR 23 Темно-серый",    "RR 29 Вишневый",
    "RR 32 Темно-коричневый",    "RR 33 Черный", "RR 35 Синий", "RR 750 Терракотовый", "RR 887"];
let result = "";

for (let i =0; i< titles.length; i++){
    let color = dict.get(titles[i]);
    result += `"${color}", `;
}
console.log(result);