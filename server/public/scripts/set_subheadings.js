async function createCards() {
    for (let headingObj of await getAllSubheaders()) {
        console.log(headingObj);
        createCardOfObject(headingObj);
    }
}

function createCardOfObject(obj) {
    console.log(obj);
    let listCatalog = document.getElementsByClassName("list_catalog")[0];

    let productsElement = document.createElement("div");
    productsElement.setAttribute("class", "products");

    let imageParentElement = document.createElement("a");
    imageParentElement.setAttribute("href", `../catalog/catalog.html?headingTwo=${obj.pageLink}`);

    let imageElement = document.createElement("img");
    imageElement.setAttribute("src", `../images/${obj.imageRef}`);
    imageElement.setAttribute("height", "250");
    imageElement.setAttribute("width", "250");

    imageParentElement.appendChild(imageElement);

    productsElement.appendChild(imageParentElement);

    let headerParentElement = document.createElement("a");
    headerParentElement.setAttribute("href", `../catalog/catalog.html?headingTwo=${obj.pageLink}`);

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

createCards().then();