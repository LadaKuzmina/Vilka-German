async function setMainPage() {
    await addHeadingsOne(await getAllHeadingsOne());
}

async function addHeadingsOne(headingsOne) {
    let productsElements = document.getElementsByClassName("out_products")[0];

    for (let headingOne of headingsOne) {
        let headingOneName = headingOne.title;

        let divElement = document.createElement("div");
        divElement.setAttribute("class", "products");

        let headingOneElement = document.createElement("a");
        headingOneElement.setAttribute("href", `../catalog/subheadings.html?heading=${headingOneName}`);

        let h4Element = document.createElement("h4");
        h4Element.textContent = headingOneName;
        headingOneElement.appendChild(h4Element);

        divElement.appendChild(headingOneElement);

        divElement.appendChild(await getHeadingsTwoElement(headingOneName));

        productsElements.appendChild(divElement);
    }
}

async function getHeadingsTwoElement(headingOneName) {
    let headingsTwo = await getAllHeadingsTwoByHeadingOne(headingOneName);
    let ulElement = document.createElement("ul");

    for (let headingTwo of headingsTwo) {
        let headingTwoName = headingTwo.title;

        let liElement = document.createElement("li");

        let aElement = document.createElement("a");
        aElement.setAttribute("href", `../catalog/catalog.html?heading=${headingTwoName}`);
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

async function getAllHeadingsTwoByHeadingOne(headingOneName) {
    let headingsTwo = await httpGet(`https://localhost:7240/GetHeadingsTwoByHeadingsOne?headingOneTitle=${headingOneName}`);
    console.log(headingsTwo);
    return headingsTwo;
}

async function httpGet(url)
{
    let response = await fetch(url);

    return response.json();
}

setMainPage().then(() => console.log("OK"));