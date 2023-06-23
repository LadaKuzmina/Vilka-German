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
    let response = httpGet(`https://localhost:7240/GetHeadingsTwoByHeadingsOne?headingOneTitle=Кровля`);
    alert(response);
    let parsedJSON = JSON.parse(placeHolder);

    return parsedJSON.Заголовки;
}

function httpGet(url)
{
    const responsePromise = fetch(url)
        .then((response) => {
            return response;
        });

    let response = '';
    const printAddress = async () => {
        response = await responsePromise;
    };

    printAddress();

    return response;
}

createCards();