words = [];
search_limit = 30;

async function search() {
    let new_words = document.getElementById('search_bar').value.split(" ").filter((str) => str !== '');
    if (JSON.stringify(new_words) !== JSON.stringify(words)) {
        let searchBlock = document.getElementById('search_block');
        searchBlock.textContent = '';

        words = new_words;
        let substrings = words.join(' ');

        if (substrings !== '') {
            let headingsOne = await getAllHeadingsOneBySubstrings(substrings);
            let headingsTwo = await getAllHeadingsTwoBySubstrings(substrings);
            let products = await getAllProductsBySubstrings(substrings);

            console.log(headingsOne);

            let counter = 0;

            for (let headingOne of headingsOne) {
                if (counter === search_limit) {
                    break;
                }
                let headingsTwo = await getAllHeadingsTwoByHeadingOne(headingOne.title);
                let aElement = document.createElement('a');
                if (headingsTwo.length === 0) {
                    aElement.setAttribute("href", `/catalog?headingOne=${headingOne.title}`);
                }
                else {
                    aElement.setAttribute("href", `/subheadings?heading=${headingOne.title}`);
                }
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
}

async function getAllHeadingsOneBySubstrings(substrings) {
    let response = await httpGet(`https://localhost:7240/GetHeadingsOneBySubstrings?substrings=${substrings}`);

    return response;
}

async function getAllHeadingsTwoBySubstrings(substrings) {
    let response = await httpGet(`https://localhost:7240/GetHeadingsTwoBySubstrings?substrings=${substrings}`);

    return response;
}

async function getAllProductsBySubstrings(substrings) {
    let response = await httpPost(`https://localhost:7240/GetProductBySubstrings?substrings=${substrings}&productOrder=0&pageNumber=1&countElements=${search_limit}`, '[]');

    return response;
}