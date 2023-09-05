async function loadProduct(){
    let productName = getHeadingName();
    let product = await getProductByName(productName);

    let head = document.getElementsByClassName("productHeading")[0];
    head.textContent = product.title;

    await addSlides(product);

    let price = document.getElementsByClassName("priceProduct")[0];
    let priceH3 = document.createElement("h3");
    // priceH3.setAttribute("style", "font-size:48px");
    priceH3.textContent = `${product.salePrice} ${product.unitMeasurement}`;
    price.appendChild(priceH3);

    currentSlide(1);

    let description = document.getElementsByClassName("descriptionProduct")[0];
    let descriptionH3 = document.createElement("h3");
    descriptionH3.textContent = "Описание:";
    description.appendChild(descriptionH3);
    let pElement = document.createElement("p");
    pElement.textContent = product.description;
    description.appendChild(pElement);

    await loadProperties(productName);
}

async function addSlides(product) {
    let slidesShow = document.getElementsByClassName("slidesshow")[0];
    let imageDots = document.getElementsByClassName("imageDots")[0];

    for (let i = 0; i < product.imageRefs.length; i++) {
        let divElement = document.createElement("div");
        divElement.setAttribute("class", "mySlides fade");

        let imgElement = document.createElement("img");
        imgElement.setAttribute("src", `../images/${product.imageRefs[i]}`);
        imgElement.setAttribute("style", "width:100%");
        divElement.appendChild(imgElement);
        slidesShow.appendChild(divElement);

        let spanElement = document.createElement("span");
        if (i === 0) {
            spanElement.setAttribute("class", "dot active");
        }
        else {
            spanElement.setAttribute("class", "dot");
        }
        spanElement.onclick = function () { currentSlide(i + 1) };
        imageDots.appendChild(spanElement);
    }
}

async function loadProperties(productName) {
    let properties = await getProperties(productName);
    let propertiesElement = document.getElementsByClassName("characteristicProduct")[0];
    let h3Element = document.createElement("h3");
    h3Element.textContent = "Характеристика:";
    propertiesElement.appendChild(h3Element);

    let tableElement = document.createElement("table");
    let index = 0;
    let trElement;

    for (let property of properties) {
        if (property.values[0] === "Не указано") continue;
        if (index % 2 === 0) {
            trElement = document.createElement("tr");
        }

        let head = document.createElement("td");
        head.setAttribute("style", "font-weight:bold");
        head.textContent = `${property.title}:`;
        trElement.appendChild(head);

        let value = document.createElement("td");
        if (property.title === 'Цвет' || property.title === 'Цветовой оттенок') {
            value.textContent = property.values[0].split(' ').slice(1).join(' ');
        }
        else
            value.textContent = property.values[0];
        trElement.appendChild(value);

        index += 1;

        if (index % 2 === 0) {
            tableElement.appendChild(trElement);
        }
    }

    if (index % 2 === 1) {
        tableElement.appendChild(trElement);
    }

    propertiesElement.appendChild(tableElement);
}

async function getProductByName(productName) {
    let response = await httpGet(`https://localhost:7240/GetProductByTitle?title=${productName}`);
    return response;
}

async function getProperties(productName) {
    let response = await httpGet(`https://localhost:7240/GetAllPropertiesByProduct?productTitle=${productName}`);
    return response;
}

function getHeadingName() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    return urlParams.get('heading');
}

loadProduct().then();
