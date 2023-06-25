let currentUrl = window.location.href;
let menuLinks = document.querySelectorAll('.nav_link');
let catalogLinks = document.querySelectorAll('.catalog_link');

for (let i = 0; i < catalogLinks.length; i++) {
    if (catalogLinks[i].href === currentUrl) {
        menuLinks[0].classList.add('active');
        break;
    }
}

//всякие каталоги лучше в будущем в отдельный список

for (let i = 0; i < menuLinks.length; i++) {
    if (menuLinks[i].href === currentUrl || currentUrl.includes('catalog')) {
        menuLinks[i].classList.add('active');
        break;
    }
}

function getDropdown() {
    document.getElementById("dropdown").classList.toggle("show");
}

window.onclick = function(e) {
    if (!e.target.matches('.dropdown_button')) {
        let dropdown = document.getElementById("dropdown");
        if (dropdown.classList.contains('show')) {
            dropdown.classList.remove('show');
        }
    }
}

function filterFunction() {
    let input = document.getElementById("search_bar");
    let filter = input.value.toUpperCase();
    let content = document.getElementById("search");
    let a = content.getElementsByTagName("a");
    let searchBlock = document.getElementById("block");
    searchBlock.style.display = "flex";

    for (let i = 0; i < a.length; i++) {
        let txt = a[i].text || a[i].innerText;
        if (filter === "") {
            searchBlock.style.display = "none";
        }
        if (txt.toUpperCase().indexOf(filter) > -1) {
            a[i].style.display = "";
        } else {
            a[i].style.display = "none";
        }
    }
}
