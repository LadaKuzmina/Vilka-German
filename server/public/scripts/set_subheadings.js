async function createCards() {
    for (let headingObj of await getAllSubheaders()) {
        console.log(headingObj);
        createCardOfObject(headingObj);
    }
}

function createCardOfObject(obj) {
    let listCatalog = document.getElementsByClassName("list_catalog")[0];

    let productsElement = document.createElement("div");
    productsElement.setAttribute("class", "products");

    let imageParentElement = document.createElement("a");
    imageParentElement.setAttribute("href", `../catalog/catalog.html?heading=${obj.title}`);

    let imageElement = document.createElement("img");
    console.log(obj.pageLink);
    imageElement.setAttribute("src", `../images/${obj.pageLink}`);
    imageElement.setAttribute("height", "250");
    imageElement.setAttribute("width", "250");

    imageParentElement.appendChild(imageElement);

    productsElement.appendChild(imageParentElement);

    let headerParentElement = document.createElement("a");
    headerParentElement.setAttribute("href", "../catalog/catalog.html?heading=${headingTwoName}");

    let headerElement = document.createElement("h4");
    headerElement.textContent = obj.title;

    headerParentElement.appendChild(headerElement);

    productsElement.appendChild(headerParentElement);

    listCatalog.appendChild(productsElement);
}

async function getAllSubheaders() {
    let response = await httpGet(`https://localhost:7240/GetHeadingsTwoByHeadingsOne?headingOneTitle=${document.getElementsByClassName('headText')[0].textContent}`);
    return response;
}

async function httpGet(url)
{
    let response = await fetch(url);
    return response.json();
}

createCards().then();