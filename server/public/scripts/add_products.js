async function createProducts(json = JSON.stringify([]), sortingParameter = 0) {
    console.log(json);
    const goodsElement = document.getElementsByClassName("goods")[0];
    removeAllKids(goodsElement);
    let products = await getAllProducts(json, sortingParameter);
    addProducts(products);
}

function addProducts(products) {
    const goodsElement = document.getElementsByClassName("goods")[0];

    for (let product of products) {
        let productElement = document.createElement("div");
        productElement.setAttribute("class", "product");

        let imageElement = document.createElement("a");
        imageElement.setAttribute("href", `../product/product.html?heading=${product.title}`);

        let imgElement = document.createElement("img");
        imgElement.setAttribute("src", "../images/profnastil_goods.jpg");
        imgElement.setAttribute("height", "200");
        imgElement.setAttribute("alt", "photo");
        imageElement.appendChild(imgElement);
        productElement.appendChild(imageElement);

        let nameProductElement = document.createElement("a");
        nameProductElement.setAttribute("class", "name_product");
        nameProductElement.setAttribute("href", `../product/product.html?heading=${product.title}`);
        nameProductElement.textContent = product.title;
        productElement.appendChild(nameProductElement);

        let count = 1;
        for (let priorityProperty of product.priorityProperties) {
            productElement.appendChild(getPriorityPropertyElement(priorityProperty, count));
            count += 1;
        }

        let priceElement = document.createElement("div");
        priceElement.setAttribute("class", "price_product");
        let bElement = document.createElement("b");
        bElement.textContent = `${numberWithSpaces(product.salePrice)} ${product.unitMeasurement}`;
        priceElement.appendChild(bElement);
        productElement.appendChild(priceElement);

        goodsElement.appendChild(productElement);
    }
}

function getPriorityPropertyElement(priorityProperty, count) {
    let divElement = document.createElement('div');
    divElement.setAttribute("class", `characteristic_${count}`);
    divElement.textContent = `${priorityProperty.title}: ${priorityProperty.values[0]}`;

    return divElement;
}

function removeAllKids(goodsElement) {
    while (goodsElement.firstChild) {
        goodsElement.removeChild(goodsElement.lastChild);
    }
}

async function getAllProducts(json, sortingParameter) {
    let headingName = getUrlParam('heading');
    let searchQuery = getUrlParam('search');

    if (searchQuery !== null) {
        return await httpPost(`https://localhost:7240/GetProductBySubstrings?substrings=${searchQuery}&productOrder=${sortingParameter}&pageNumber=1&countElements=99999`, json);
    }

    return await httpPost(`https://localhost:7240/GetPageHeadingTwo?headingTwoTitle=${headingName}&productOrder=${sortingParameter}&pageNumber=1&countElements=99999`, json);
}

async function httpPost(url, json)
{
    let response = await fetch(url, {
        method: 'POST',
        headers: {
            "Accept": "text/plain",
            "Content-Type": "application/json"
        },
        body: json});
    return response.json();
}

function getUrlParam(urlParam) {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    return urlParams.get(urlParam);
}

function numberWithSpaces(x) {
    let parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    return parts.join(".");
}

createProducts().then();
