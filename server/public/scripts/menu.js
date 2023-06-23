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
