function addRow() {
    const tableBody = document.getElementsByClassName('body')[0];
    let newRow = document.createElement('tr');

    let characteristicElement = document.createElement("td");
    let characteristicInputElement = document.createElement("input");
    characteristicInputElement.setAttribute("type", "text");
    characteristicInputElement.setAttribute("class", "characteristic");
    characteristicInputElement.setAttribute("placeholder", "Характеристика");
    characteristicElement.appendChild(characteristicInputElement);
    newRow.appendChild(characteristicElement);

    let valueElement = document.createElement("td");
    let valueInputElement = document.createElement("input");
    valueInputElement.setAttribute("type", "text");
    valueInputElement.setAttribute("class", "value");
    valueInputElement.setAttribute("placeholder", "Значение");
    valueElement.appendChild(valueInputElement);
    newRow.appendChild(valueElement);

    tableBody.appendChild(newRow);
}
