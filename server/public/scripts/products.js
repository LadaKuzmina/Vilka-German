let profiledCheckboxes = ["МП-20", "С-21", "МП-35", "НС-35", "С-44", "Н-60", "Н-75", "Н-114", "МП-20 Поликарбонат"];
let widthCheckboxes = ["0,35 и менее", "0,4", "0,45", "0,5", "0,55", "0,6", "0,65", "0,7", "0,75", "0,8", "0,9", "1"];
let coatingCheckboxes = ["Полиэстер", "Цинк (Zn)", "AGNETA®", "Atlas X", "Drap", "CLOUDY®", "Drap ST", "ECOSTEEL®", "GreenCoat Pural BT", "GreenCoat Pural BT, matt", "NormanMP®", "PurPro Matt", "PURETAN®", "PurLite Matt", "PURMAN®", "Quarzit", "Quarzit lite", "Quarzit PRO Matt", "Rooftop Matte", "Satin", "Satin Matt", "VALORI", "Velur X", "VikingMP"];
let workWidthCheckboxes = ["1000", "1035", "1100", "600", "750", "845"];
let warrantyCheckboxes = ["10 лет", "20 лет", "25 лет", "30 лет", "50 лет"];
let s = new Map();
s.set("Профнастил", profiledCheckboxes);
s.set("Толщина, мм", widthCheckboxes);
s.set("Покрытие", coatingCheckboxes);
s.set("Рабочая ширина, мм", workWidthCheckboxes);
s.set("Гарантия", warrantyCheckboxes)

async function createCheckboxes() {
    let count = 1;
    let minPrice = -1;
    let maxPrice = -1;

    for (let property of await getAllProperties()) {
        if (property.title === "Минимальная цена")
            minPrice = parseInt(property.value[0]);
        else if (property.title === "Максимальная цена")
            maxPrice = parseInt(property.value[0]);
        else {
            setPricesSlider(minPrice, maxPrice);
            break;
            createRectangleCheckboxes(property, count);
            count += property.value.length;
        }
    }
}

function setPricesSlider(minPrice, maxPrice) {
    let price25 = minPrice + Math.round((maxPrice - minPrice) / 4);
    let price75 = minPrice + Math.round((maxPrice - minPrice) * 3 / 4);

    let wrapperElement = document.getElementsByClassName("wrapper")[0];

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
    let wrapperElement = document.getElementsByClassName("wrapper")[0];

    let detailsElement = document.createElement("details");
    detailsElement.open = true;

    let summaryElement = document.createElement("summary");
    let bElement = document.createElement("b");
    bElement.textContent = elementName;
    summaryElement.appendChild(bElement);
    detailsElement.appendChild(summaryElement);

    let checkboxesContainer = document.createElement("div");
    checkboxesContainer.setAttribute("class", "checkbox_container");
    for (let checkbox of checkboxesList) {
        let checkboxElement = createCheckboxesElement(checkbox, count);
        checkboxesContainer.appendChild(checkboxElement);
        count++;
    }
    detailsElement.appendChild(checkboxesContainer);

    wrapperElement.appendChild(detailsElement);
}

function createCheckboxesElement(checkbox, count) {
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
    rectElement.setAttributeNS(null, "stroke", "#d4aa70");
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
    spanElement.textContent = checkbox;
    labelElement.appendChild(spanElement);

    return labelElement;
}

async function getAllProperties() {
    let response = await httpGet(`https://localhost:7240/GetPropertiesByHeadingTwo?headingTwoTitle=${getHeadingName()}`);
    return response;
}

async function httpGet(url)
{
    let response = await fetch(url);
    return response.json();
}

function getHeadingName() {
    const queryString = window.location.search;
    console.log(queryString);
    const urlParams = new URLSearchParams(queryString);

    return urlParams.get('heading');
}

createCheckboxes().then(() => console.log("OK"));