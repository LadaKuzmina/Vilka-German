let cardsList = [
    {
        "image": "project_photo/project1.jpg",
        "face1": "Учалы",
        "face2": "Дом Германа"
    },
    {
        "image": "project_photo/project2.jpg",
        "face1": "Россия 2024 год",
        "face2": "Екатеринбург"
    },
    {
        "image": "project_photo/project3.jpg",
        "face1": "Россия 2024 год",
        "face2": "Екатеринбург"
    },
    {
        "image": "project_photo/project3.jpg",
        "face1": "Первоуральск",
        "face2": "Резеденция Ильи"
    },
    {
        "image": "project_photo/project2.jpg",
        "face1": "Россия 2024 год",
        "face2": "Екатеринбург"
    }
];

function createCards() {
    for (let obj of cardsList) {
        createCardOfObject(obj);
    }

}

function createCardOfObject(obj) {
    let cardContainer = document.getElementsByClassName("card-container")[0];

    let cardElement = document.createElement("div");
    cardElement.setAttribute("class", "card");

    let face1Element = document.createElement("div");
    face1Element.setAttribute("class", "face face-1");

    let imgElement = document.createElement("img");
    imgElement.setAttribute("src", `${obj["image"]}`);

    let h3 = document.createElement("h3");
    h3.textContent = obj["face1"];

    face1Element.appendChild(imgElement);
    face1Element.appendChild(h3);

    cardElement.appendChild(face1Element);

    let face2Element = document.createElement("div");
    face2Element.setAttribute("class", "face face-2");

    let p = document.createElement("p");
    p.textContent = obj["face2"];

    face2Element.appendChild(p);

    cardElement.appendChild(face2Element);

    cardContainer.appendChild(cardElement);
}

createCards();