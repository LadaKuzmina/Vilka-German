let currentUrl = window.location.href;
let menuLinks = document.querySelectorAll('.nav_link');

for (let i = 0; i < menuLinks.length; i++) {
    if (menuLinks[i].href === currentUrl) {
        menuLinks[i].classList.add('active');
        break;
    }
}
