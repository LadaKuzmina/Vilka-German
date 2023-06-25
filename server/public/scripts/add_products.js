async function createProducts(json = JSON.stringify([])) {
    const goodsElement = document.getElementsByClassName("goods")[0];
    removeAllKids(goodsElement);
    console.log(getAllProducts(json));
}

function removeAllKids(goodsElement) {
    while (goodsElement.firstChild) {
        goodsElement.removeChild(goodsElement.lastChild);
    }
}

async function getAllProducts(json) {
    let response = await httpGet(`https://localhost:7240/GetPageHeadingTwo?headingTwoTitle=${getHeadingName()}&productOrder=0&pageNumber=1&countElements=4`, json);
    return response;
}

async function httpGet(url, json)
{
    let data = new FormData();
    data.append( "json", json);
    console.log(data);
    let response = await fetch(url, {
        method: 'POST',
        body: data});
    return response.json();
}

function getHeadingName() {
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);

    return urlParams.get('heading');
}

createProducts().then(() => console.log("OK"));
