async function configure() {
    let dropdownContent = document.getElementsByClassName("dropdown_content")[0];

    let headingsOne = await getAllHeadingsOne();
    for (let headingOne of headingsOne) {
        let headingsTwo = await getAllHeadingsTwoByHeadingOne(headingOne.title);

        let aElement = document.createElement("a");
        aElement.setAttribute("class", "nav_link");
        if (headingsTwo.length === 0) {
            aElement.setAttribute("href", `../catalog/catalog.html?headingOne=${headingOne.title}`);
        }
        else {
            aElement.setAttribute("href", `../catalog/subheadings.html?heading=${headingOne.title}`);
        }
        aElement.textContent = headingOne.title;
        dropdownContent.appendChild(aElement);
    }
}

async function getAllHeadingsOne() {
    return await httpGet('https://localhost:7240/GetAllHeadingsOne');
}

async function getAllHeadingsTwoByHeadingOne(headingOnePageLink) {
    return await httpGet(`https://localhost:7240/GetHeadingsTwoByHeadingsOne?headingOneTitle=${headingOnePageLink}`);
}

configure().then();