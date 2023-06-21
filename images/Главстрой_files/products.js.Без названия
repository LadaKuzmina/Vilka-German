let profiledCheckboxes = ["МП-20", "С-21", "МП-35", "НС-35", "С-44", "Н-60", "Н-75", "Н-114", "МП-20 Поликарбонат"];
let widthCheckboxes = ["0,35 и менее", "0,4", "0,45", "0,5", "0,55", "0,6", "0,65", "0,7", "0,75", "0,8", "0,9", "1"];
let coatingCheckboxes = ["Полиэстер", "Цинк (Zn)", "AGNETA®", "Atlas X", "Drap", "CLOUDY®", "Drap ST", "ECOSTEEL®", "GreenCoat Pural BT", "GreenCoat Pural BT, matt", "NormanMP®", "PurPro Matt", "PURETAN®", "PurLite Matt", "PURMAN®", "Quarzit", "Quarzit lite", "Quarzit PRO Matt", "Rooftop Matte", "Satin", "Satin Matt", "VALORI", "Velur X", "VikingMP"];
let workWidthCheckboxes = ["1000", "1035", "1100", "600", "750", "845"];
let warrantyCheckboxes = ["10 лет", "20 лет", "25 лет", "30 лет", "50 лет"];
let s = new Map();
s.set("profiled", profiledCheckboxes);
s.set("width", widthCheckboxes);
s.set("coating", coatingCheckboxes);
s.set("workWidth", workWidthCheckboxes);
s.set("warranty", warrantyCheckboxes)

function creatCheckboxes() {
    let count = 1;
    for (let [name, list] of s) {
        createRectangleCheckboxes(name, list, count);
        count += list.length;
    }

}

function createRectangleCheckboxes(elementName, checkboxesList, count) {
    let profileElement = document.getElementsByClassName(elementName)[0];
    for (let checkbox of checkboxesList) {
        let labelElement = document.createElement("label");
        labelElement.setAttribute("for", `myCheckbox${count}`);
        labelElement.setAttribute("class", "checkbox");

        let inputElement = document.createElement("input");
        inputElement.setAttribute("class", "checkbox__input");
        inputElement.setAttribute("type", "checkbox");
        inputElement.setAttribute("id", `myCheckbox${count}`);
        count++;
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
        profileElement.appendChild(labelElement);
    }
}

creatCheckboxes();
