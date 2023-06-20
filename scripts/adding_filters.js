
export  class Filter{
    constructor(name, titles, colors=null) {
        this._name = name;
        this._titles = titles;
        this._colorsMaterials = colors;
    }
    get titles(){
        return this._titles;
    }
    get colorsMaterials(){
        return this._colorsMaterials;
    }
    get name(){
        return this._name;
    }
}

export function creatCheckboxes(list) {
    let count = 1;
    for (let filter of list) {
        createRectangleCheckboxes(filter.name, filter.titles, count, filter.colorsMaterials);
        count += filter.titles.length;
    }
}
export function createRectangleCheckboxes(elementName, checkboxesList, count, colors=null) {
    let profileElement = document.getElementsByClassName(elementName)[0];
    for (let i = 0; i < checkboxesList.length;i++) {
        let checkbox = checkboxesList[i];
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

        let textElement = document.createElement( "div");
        textElement.textContent = checkbox;

        if (colors !== null){
            let colorElement = document.createElement("div");
            colorElement.className='square';
            colorElement.setAttribute("style", colors[i]);
            spanElement.appendChild(colorElement);
        }
        spanElement.appendChild(textElement);
        labelElement.appendChild(spanElement);
        profileElement.appendChild(labelElement);
    }
}