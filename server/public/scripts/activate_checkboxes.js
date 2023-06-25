async function activateCheckboxes(sortingParameter = 0){
    let detailsElements = document.getElementsByClassName("checkboxes-details");

    let queryArray = [];

    queryArray.push({
        "title": "Минимальная цена",
        "values": [document.querySelectorAll(".price-input input")[0].value],
        "isPriority": false
    });

    queryArray.push({
        "title": "Максимальная цена",
        "values": [document.querySelectorAll(".price-input input")[1].value],
        "isPriority": false
    });

    for (let detailsElement of detailsElements) {
        let groupObject = getGroupObject(detailsElement);
        if (groupObject.values.length !== 0) {
            queryArray.push(groupObject);
        }
    }

    let queryJson = JSON.stringify(queryArray);
    createProducts(queryJson, sortingParameter);
}

function getGroupObject(detailsElement) {
    let checkboxesGroupName = detailsElement.getElementsByClassName("checkboxes-group-name")[0].textContent;
    let groupObject = {
        "title": checkboxesGroupName,
        "values": [],
        "isPriority": false
    }

    let checkboxes = detailsElement.getElementsByClassName("checkbox");
    for (let checkbox of checkboxes) {
        if (checkbox.getElementsByClassName("checkbox__input")[0].checked) {
            let checkboxTextContent = checkbox.getElementsByClassName("checkbox__label")[0].textContent;
            if (checkboxesGroupName !== "Цвет" || checkboxTextContent === "Не указано")
                groupObject.values.push(checkboxTextContent);
            else {
                let hex =  checkbox.getElementsByClassName("square")[0]
                    .getAttribute("style").split(' ')[1];
                groupObject.values.push(`${hex}${checkboxTextContent}`);
            }
        }
    }

    return groupObject;
}