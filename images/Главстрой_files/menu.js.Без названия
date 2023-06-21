let currentUrl = window.location.href;
let menuLinks = document.querySelectorAll('.nav_link');

//всякие каталоги лучше в будущем в отдельный список

for (let i = 0; i < menuLinks.length; i++) {
    if (menuLinks[i].href === currentUrl || currentUrl.includes('catalog')) {
        menuLinks[i].classList.add('active');
        break;
    }
}
