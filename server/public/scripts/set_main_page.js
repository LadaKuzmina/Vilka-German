async function setMainPage() {
    let headingsOne = await getAllHeadingsOne();
    await addHeadingsOne(headingsOne);
}

async function addHeadingsOne(headingsOne) {
    let productsElements = document.getElementsByClassName("out_products")[0];

    for (let headingOne of headingsOne) {
        let headingOneName = headingOne.title;
        let headingOneImageRef = headingOne.imageRef;

        let productElement = document.createElement("div");
        productElement.setAttribute("class", "products");

        let imgElement = document.createElement("img");
        imgElement.setAttribute("class", "img_prod");
        imgElement.setAttribute("src", `../images/${headingOneImageRef}`);
        imgElement.setAttribute("height", "300");
        imgElement.setAttribute("width", "300");
        productElement.appendChild(imgElement);

        let pElement = document.createElement("p");
        pElement.textContent = headingOneName;
        productElement.appendChild(pElement);

        productElement.appendChild(await getProdTextElement(headingOne));

        productsElements.appendChild(productElement);
    }
}

async function getProdTextElement(headingOne) {
    let headingsTwo = await getAllHeadingsTwoByHeadingOne(headingOne.title);

    let prodTextElement = document.createElement("div");
    prodTextElement.setAttribute("class", "prod_text");
    prodTextElement.setAttribute("style", "top: 40px");

    let h4Element = document.createElement("h4");
    let aElement = document.createElement("a");
    if (headingsTwo.length === 0) {
        aElement.setAttribute("href", `../catalog/catalog.html?headingOne=${headingOne.title}`);
    }
    else {
        aElement.setAttribute("href", `../catalog/subheadings.html?heading=${headingOne.title}`);
    }
    aElement.textContent = headingOne.title;
    h4Element.appendChild(aElement);
    prodTextElement.appendChild(h4Element);

    if (headingsTwo.length !== 0) {
        prodTextElement.appendChild(await getHeadingsTwoElement(headingsTwo));
    }

    return prodTextElement;
}

async function getHeadingsTwoElement(headingsTwo) {
    let ulElement = document.createElement("ul");

    for (let headingTwo of headingsTwo) {
        let headingTwoName = headingTwo.title;
        let headingOnePageLink = headingTwo.pageLink;

        let liElement = document.createElement("li");

        let aElement = document.createElement("a");
        aElement.setAttribute("href", `../catalog/catalog.html?headingTwo=${headingOnePageLink}`);
        aElement.textContent = headingTwoName;
        liElement.appendChild(aElement);

        ulElement.appendChild(liElement);
    }

    return ulElement;
}

async function getAllHeadingsOne() {
    let headingsOne = await httpGet('https://localhost:7240/GetAllHeadingsOne');

    return headingsOne;
}

async function getAllHeadingsTwoByHeadingOne(headingOnePageLink) {
    let headingsTwo = await httpGet(`https://localhost:7240/GetHeadingsTwoByHeadingsOne?headingOneTitle=${headingOnePageLink}`);

    return headingsTwo;
}

setMainPage().then();