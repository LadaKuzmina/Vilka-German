currentPage = -1;
maxPage = 500;
blocksCount = 7;

async function init_pages() {
    let headingOne = getUrlParam('headingOne');
    let headingTwo = getUrlParam('headingTwo');

    if (headingTwo !== null)
        maxPage = await httpPost(`https://localhost:7240/GetCountPagesHeadingTwo?headingTwoTitle=${headingTwo}&productOrder=0&countElements=50`, '{}');
    else
        maxPage = await httpPost(`https://localhost:7240/GetCountPagesHeadingOne?headingOneTitle=${headingOne}&productOrder=0&countElements=50`, '{}');

    setPageWidget(1);
}

function setPageWidget(page) {
    if (page !== currentPage) {
        let newPage = page;
        let pageSwitcherElement = document.getElementsByClassName("page_switcher")[0];
        pageSwitcherElement.textContent = '';
        currentPage = page;

        let blocksLeft = Math.min(blocksCount - 1, maxPage - 1);
        let pagesOnLeft = [];
        let pagesOnRight = [];
        let areDotsOnTheLeft = false;
        let areDotsOnTheRight = false;

        for (let i = 1; i <= (blocksCount - 1) / 2 + 1; i++) {
            newPage--;
            if (newPage === 0) {
                break;
            }
            if (i === (blocksCount - 1) / 2 + 1) {
                pagesOnLeft.pop();
                areDotsOnTheLeft = true;
                break;
            }
            pagesOnLeft.push(newPage);
            blocksLeft--;
        }

        newPage = page;
        let minus = 0;
        for (let i = 1; i <= blocksLeft + 1; i++) {
            newPage++;
            if (newPage === maxPage + 1) {
                break;
            }
            if (i === blocksLeft + 1) {
                pagesOnRight.pop();
                areDotsOnTheRight = true;
                break;
            }
            pagesOnRight.push(newPage);
            minus++;
        }
        blocksLeft -= minus;

        if (blocksLeft !== 0) {
            areDotsOnTheLeft = false;
            newPage = pagesOnLeft[pagesOnLeft.length - 1];
            for (let i = 1; i <= blocksLeft + 2; i++) {
                newPage--;
                if (newPage === 0) {
                    break;
                }
                if (i === blocksLeft + 2) {
                    pagesOnLeft.pop();
                    areDotsOnTheLeft = true;
                    break;
                }
                pagesOnLeft.push(newPage);
            }
        }
        pagesOnLeft.reverse();

        if (page !== 1) {
            pageSwitcherElement.appendChild(getPageSwitcherArrow('<<', 1));
            pageSwitcherElement.appendChild(getPageSwitcherArrow('<', page - 1));
        } else {
            pageSwitcherElement.appendChild(getPageSwitcherInactiveArrow('<<'));
            pageSwitcherElement.appendChild(getPageSwitcherInactiveArrow('<'));
        }

        if (areDotsOnTheLeft) pageSwitcherElement.appendChild(getPageSwitcherDots())

        for (let pageOnLeft of pagesOnLeft) pageSwitcherElement.appendChild(getPageSwitcherButton(pageOnLeft));
        pageSwitcherElement.appendChild(getPageSwitcherButton(page));
        for (let pageOnRight of pagesOnRight) pageSwitcherElement.appendChild(getPageSwitcherButton(pageOnRight));

        if (areDotsOnTheRight) pageSwitcherElement.appendChild(getPageSwitcherDots())

        if (page !== maxPage) {
            pageSwitcherElement.appendChild(getPageSwitcherArrow('>', page + 1));
            pageSwitcherElement.appendChild(getPageSwitcherArrow('>>', maxPage));
        } else {
            pageSwitcherElement.appendChild(getPageSwitcherInactiveArrow('>'));
            pageSwitcherElement.appendChild(getPageSwitcherInactiveArrow('>>'));
        }
    }
}

function getPageSwitcherArrow(textContent, page) {
    let pageSwitcherArrowElement = document.createElement("div");
    pageSwitcherArrowElement.setAttribute("class", "page_switcher_arrow");
    pageSwitcherArrowElement.onclick = function () {
        setPage(page)
    };
    pageSwitcherArrowElement.textContent = textContent;

    return pageSwitcherArrowElement;
}

function getPageSwitcherInactiveArrow(textContent) {
    let pageSwitcherInactiveArrowElement = document.createElement("div");
    pageSwitcherInactiveArrowElement.setAttribute("class", "page_switcher_inactive_arrow");
    pageSwitcherInactiveArrowElement.textContent = textContent;

    return pageSwitcherInactiveArrowElement;
}

function getPageSwitcherDots() {
    let pageSwitcherDotsElement = document.createElement("div");
    pageSwitcherDotsElement.setAttribute("class", "page_switcher_dots");
    pageSwitcherDotsElement.textContent = '...';
    return pageSwitcherDotsElement;
}

function getPageSwitcherButton(page) {
    let pageSwitcherButton = document.createElement("div");
    pageSwitcherButton.setAttribute("class", "page_switcher_button");
    let styleAttribute = '';
    if (page === currentPage) {
        styleAttribute += "background-color: #DBBE00;";
    }
    if (page >= 100 && page <= 999) {
        styleAttribute += "font-size: 20px;";
    }
    if (page >= 1000) {
        styleAttribute += "font-size: 15px;";
    }
    if (styleAttribute !== '') {
        pageSwitcherButton.setAttribute("style", styleAttribute);
    }
    pageSwitcherButton.onclick = function () {
        setPage(page)
    };
    pageSwitcherButton.textContent = page;

    return pageSwitcherButton;
}

function setPage(page) {
    setPageWidget(page);
    createProducts(QUERY_JSON, SORTING_PARAMETER, currentPage);
}

init_pages().then();