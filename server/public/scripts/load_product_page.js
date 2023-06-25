async function loadProduct(){
    let productName = getHeadingName();
    let product = await getProductByName(productName);

    let head = document.getElementsByClassName("head")[0];
    let headH1 = document.createElement("h1");
    headH1.textContent = product.title;
    head.appendChild(headH1);
    head.appendChild(document.createElement("hr"));

    let photo = document.getElementsByClassName("photo_product")[0];
    let imgElement = document.createElement("img");
    imgElement.setAttribute("src", "../images/profnastil_goods.jpg");
    imgElement.setAttribute("width", "400");
    imgElement.setAttribute("height", "400");
    photo.appendChild(imgElement);

    let price = document.getElementsByClassName("price")[0];
    let priceH1 = document.createElement("h1");
    priceH1.textContent = `${product.salePrice} ${product.unitMeasurement}`;
    price.appendChild(priceH1);

    let description = document.getElementsByClassName("description")[0];
    let pElement = document.createElement("p");
    pElement.textContent = product.description;
    description.appendChild(pElement);

    await loadProperties(productName);
}

async function loadProperties(productName) {
    let properties = await getProperties(productName);
    let propertiesElement = document.getElementsByClassName("characteristic_dl")[0];

    for (let property of properties) {
        if (property.values[0] === "Не указано") continue;

        let dtElement = document.createElement("dt");
        dtElement.textContent = `${property.title}:`;
        propertiesElement.appendChild(dtElement);

        let ddElement = document.createElement("dd");
        if (property.title === 'Цвет')
            ddElement.textContent = property.values[0].split(' ').slice(1).join(' ');
        else
            ddElement.textContent = property.values[0];
        propertiesElement.appendChild(ddElement);
    }
}

async function getProductByName(productName) {
    let response = await httpGet(`https://localhost:7240/GetProductByTitle?title=${productName}`);
    return response;
}

async function getProperties(productName) {
    let response = await httpGet(`https://localhost:7240/GetAllPropertiesByProduct?productTitle=${productName}`);
    return response;
}

async function httpGet(url)
{
    let response = await fetch(url);
    return response.json();
}

function getHeadingName() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    return urlParams.get('heading');
}

loadProduct().then(() => console.log("OK"));
