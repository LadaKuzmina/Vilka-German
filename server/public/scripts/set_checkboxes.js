async function createCheckboxes() {
    let count = 1;
    let minPrice = -1;
    let maxPrice = -1;

    for (let property of await getAllProperties()) {
        if (property.title === "Минимальная цена") {
            minPrice = parseInt(property.values[0]);
            if (minPrice !== -1 && maxPrice !== -1) {
                setPricesSlider(minPrice, maxPrice);
            }
        }
        else if (property.title === "Максимальная цена") {
            maxPrice = parseInt(property.values[0]);
            if (minPrice !== -1 && maxPrice !== -1) {
                setPricesSlider(minPrice, maxPrice);
            }
        }
        else {
            createRectangleCheckboxes(property, count);
            count += property.values.length;
        }
    }
}

function setPricesSlider(minPrice, maxPrice) {
    let price25 = minPrice + Math.round((maxPrice - minPrice) / 4);
    let price75 = minPrice + Math.round((maxPrice - minPrice) * 3 / 4);

    let wrapperElement = document.getElementsByClassName("filters_checkbox")[0];

    let detailsElement = document.createElement("details");
    detailsElement.open = true;

    let summaryElement = document.createElement("summary");
    let bElement = document.createElement("b");
    bElement.textContent = "Цена";
    summaryElement.appendChild(bElement);

    detailsElement.appendChild(summaryElement);

    let priceInputElement = document.createElement("div");
    priceInputElement.setAttribute("class", "price-input");
    priceInputElement.appendChild(getInputField("От", "input-min", price25));

    let separator = document.createElement("div");
    separator.setAttribute("class", "separator");
    separator.textContent = "-";
    priceInputElement.appendChild(separator);

    priceInputElement.appendChild(getInputField("До", "input-max", price75));

    detailsElement.appendChild(priceInputElement);

    let sliderRange = document.createElement("div");
    sliderRange.setAttribute("class", "slider-range");
    let progress = document.createElement("div");
    progress.setAttribute("class", "progress");
    sliderRange.appendChild(progress);
    detailsElement.appendChild(sliderRange);

    let rangeInput = document.createElement("div");
    rangeInput.setAttribute("class", "range-input");
    rangeInput.appendChild(getInputElement("range-min", minPrice, maxPrice, price25));
    rangeInput.appendChild(getInputElement("range-max", minPrice, maxPrice, price75));

    detailsElement.appendChild(rangeInput);

    wrapperElement.appendChild(detailsElement);

    let rangeScript = document.createElement("script");
    rangeScript.setAttribute("src", "../scripts/range.js");
    document.body.appendChild(rangeScript);
}

function getInputField(spanText, className, price) {
    let fieldElement = document.createElement("div");
    fieldElement.setAttribute("class", "field");

    let spanElement = document.createElement("span");
    spanElement.textContent = spanText;
    fieldElement.appendChild(spanElement);

    let inputElement = document.createElement("input");
    inputElement.setAttribute("type", "number");
    inputElement.setAttribute("class", className);
    inputElement.setAttribute("value", `${price}`);
    fieldElement.appendChild(inputElement);

    return fieldElement;
}

function getInputElement(className, min, max, value) {
    let inputElement = document.createElement("input");
    inputElement.setAttribute("type", "range");
    inputElement.setAttribute("class", className);
    inputElement.setAttribute("min", `${min}`);
    inputElement.setAttribute("max", `${max}`);
    inputElement.setAttribute("value", `${value}`);

    return inputElement;
}

function createRectangleCheckboxes(property, count) {
    let wrapperElement = document.getElementsByClassName("filters_checkbox")[0];

    let detailsElement = document.createElement("details");
    detailsElement.setAttribute("class", "checkboxes-details");
    detailsElement.open = true;

    let summaryElement = document.createElement("summary");
    let bElement = document.createElement("b");
    bElement.setAttribute("class", "checkboxes-group-name");
    bElement.textContent = property.title;
    summaryElement.appendChild(bElement);
    detailsElement.appendChild(summaryElement);

    let checkboxesContainer = document.createElement("div");
    checkboxesContainer.setAttribute("class", "checkbox_container");
    property.values.sort();
    for (let checkbox of property.values) {
        let checkboxElement = createCheckboxesElement(checkbox, count, property.title === "Цвет" && checkbox !== "Не указано");
        checkboxesContainer.appendChild(checkboxElement);
        count++;
    }
    detailsElement.appendChild(checkboxesContainer);

    wrapperElement.appendChild(detailsElement);
}

function createCheckboxesElement(checkbox, count, is_color) {
    let labelElement = document.createElement("label");
    labelElement.setAttribute("for", `myCheckbox${count}`);
    labelElement.setAttribute("class", "checkbox");

    let inputElement = document.createElement("input");
    inputElement.setAttribute("class", "checkbox__input");
    inputElement.setAttribute("type", "checkbox");
    inputElement.setAttribute("id", `myCheckbox${count}`);
    labelElement.appendChild(inputElement);

    let svgElement = document.createElementNS("http://www.w3.org/2000/svg", "svg");
    svgElement.setAttributeNS(null, "class", "checkbox__icon");
    svgElement.setAttributeNS(null, "viewBox", "0 0 22 22");

    let rectElement = document.createElementNS("http://www.w3.org/2000/svg", "rect");
    rectElement.setAttributeNS(null, "width", "21");
    rectElement.setAttributeNS(null, "height", "21");
    rectElement.setAttributeNS(null, "x", ".5");
    rectElement.setAttributeNS(null, "y", ".5");
    rectElement.setAttributeNS(null, "fill", "#FFF");
    rectElement.setAttributeNS(null, "stroke", "#000000");
    rectElement.setAttributeNS(null, "rx", "3");

    let pathElement = document.createElementNS("http://www.w3.org/2000/svg", "path");
    pathElement.setAttributeNS(null, "class", "tick");
    pathElement.setAttributeNS(null, "stroke", "#6EA340");
    pathElement.setAttributeNS(null, "fill", "none");
    pathElement.setAttributeNS(null, "stroke-linecap", "round");
    pathElement.setAttributeNS(null, "stroke-width", "4");
    pathElement.setAttributeNS(null, "d", "M4 10l5 5 9-9");

    svgElement.appendChild(rectElement);
    svgElement.appendChild(pathElement);
    labelElement.appendChild(svgElement);

    let spanElement = document.createElement("span");
    spanElement.setAttribute("class", "checkbox__label");
    if (is_color) {
        spanElement.textContent = ` ${checkbox.split(' ').slice(1).join(' ')}`;
        let squareElement = document.createElement("div");
        squareElement.setAttribute("class", "square");
        squareElement.setAttribute("style", `background-color: ${checkbox.split(' ')[0]}`);
        spanElement.insertAdjacentElement('afterbegin', squareElement);
    }
    else {
        spanElement.textContent = checkbox;
    }
    labelElement.appendChild(spanElement);

    return labelElement;
}

async function getAllProperties() {
    let headingName = getUrlParam('heading');
    let searchQuery = getUrlParam('search');

    if (searchQuery !== null) {
        return await httpGet(`https://localhost:7240/GetPropertiesBySubstrings?substrings=${searchQuery}`);
    }

    return await httpGet(`https://localhost:7240/GetPropertiesByHeadingTwo?headingTwoTitle=${headingName}`);
}

async function httpGet(url)
{
    let response = await fetch(url);
    return response.json();
}

createCheckboxes().then();
