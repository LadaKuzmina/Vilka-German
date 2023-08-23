words = [];
search_limit = 30;

async function search() {
    let new_words = document.getElementById('search_bar').value.split(" ").filter((str) => str !== '');
    if (JSON.stringify(new_words) !== JSON.stringify(words)) {
        let searchBlock = document.getElementById('search_block');
        searchBlock.textContent = '';

        words = new_words;
        let json = JSON.stringify(words);

        let headingsOne = await getAllHeadingsOne(json);
        let headingsTwo = await getAllHeadingsTwo(json);
        let products = await getAllProducts(json);

        let counter = 0;

        for (let headingOne of headingsOne) {
            let aElement = document.createElement('a');
            aElement.setAttribute('href', `http://localhost:3000/catalog/subheadings.html?heading=${headingOne.title}`);
            aElement.setAttribute('style', '')
            aElement.textContent = headingOne.title;

            searchBlock.appendChild(aElement);
            counter++;
        }

        for (let headingTwo of headingsTwo) {
            if (counter === search_limit) {
                break;
            }
            let aElement = document.createElement('a');
            aElement.setAttribute('href', `http://localhost:3000/catalog/catalog.html?heading=${headingTwo.title}`);
            aElement.setAttribute('style', '')
            aElement.textContent = headingTwo.title;

            searchBlock.appendChild(aElement);
            counter++;
        }

        for (let product of products) {
            if (counter === search_limit) {
                break;
            }

            let aElement = document.createElement('a');
            aElement.setAttribute('href', `http://localhost:3000/product/product.html?heading=${product.title}`);
            aElement.setAttribute('style', '')
            aElement.textContent = product.title;

            searchBlock.appendChild(aElement);
            counter++;
        }
    }
}

async function getAllHeadingsOne(json) {
    let response = await httpPost(`https://localhost:7240/GetHeadingsOneBySubstrings`, json);

    return response;
}

async function getAllHeadingsTwo(json) {
    let response = await httpPost(`https://localhost:7240/GetHeadingsTwoBySubstrings`, json);

    return response;
}

async function getAllProducts(json) {
    let response = await httpPost(`https://localhost:7240/GetProductBySubstrings`, json);

    return response;
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