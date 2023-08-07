let phoneInput = document.querySelector(".phone");

const mask = new IMask(phoneInput, {
    mask: "+{7}(000) 000-00-00",
});