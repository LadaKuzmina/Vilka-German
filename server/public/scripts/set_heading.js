async function setHeading() {
    let heading = getHeadingName();

    document.getElementsByClassName('headText')[0].textContent = heading;

    await createCards();
}

function getHeadingName() {
    const queryString = window.location.search;
    console.log(queryString);
    const urlParams = new URLSearchParams(queryString);

    return urlParams.get('heading');
}

setHeading().then();