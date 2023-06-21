function createCards() {
    for (let heading of getAllSubheaders()) {
        createCardOfObject(heading);
    }
}

function createCardOfObject(obj) {
    let listCatalog = document.getElementsByClassName("list_catalog")[0];

    let productsElement = document.createElement("div");
    productsElement.setAttribute("class", "products");

    let imageParentElement = document.createElement("a");
    imageParentElement.setAttribute("href", "#");

    let imageElement = document.createElement("img");
    imageElement.setAttribute("src", `${obj[1]}`);
    imageElement.setAttribute("height", "250");
    imageElement.setAttribute("width", "250");

    imageParentElement.appendChild(imageElement);

    productsElement.appendChild(imageParentElement);

    let headerParentElement = document.createElement("a");
    headerParentElement.setAttribute("href", "#");

    let headerElement = document.createElement("h4");
    headerElement.textContent = obj[0];

    headerParentElement.appendChild(headerElement);

    productsElement.appendChild(headerParentElement);

    listCatalog.appendChild(productsElement);
}

function getAllSubheaders() {
    let obj = new Object();
    obj.Заголовок = `${document.getElementsByClassName('head')[0].textContent}`;
    let jsonString= JSON.stringify(obj);

    // Запрос на получение всех подзаголовков

    let placeHolder =
        `{
        "Заголовки": [["Сайдинг виниловый", "https://drive.google.com/uc?export=view&id=1hzQ3RZW5ls_-aLay5dwjM2tOGDA2OgYC", "NULL"], ["Сайдинг металлический", "../images/99574_sayding.png", "NULL"], ["Сайдинг фиброцементный", "../images/99574_sayding.png", "NULL"], ["Софиты", "../images/99570_sofity.png", "NULL"], ["Фасадные панели", "../images/99568_fasadnye-paneli.png", "NULL"], ["Фасадная плитка", "../images/120350_fasad-fasadnaya-plitka.png", "NULL"], ["Доборные элементы", "../images/120351_fasad-dobornye-elementy.png", "NULL"], ["Подсистема для фасада", "../images/120352_fasad_podsistema_dlya-fasada.png", "NULL"]]
        }`;
    let parsedJSON = JSON.parse(placeHolder);

    return parsedJSON.Заголовки;
}

createCards();