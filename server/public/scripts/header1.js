const header = document.createElement('header');
header.classList.add('header');

const logoDiv = document.createElement('div');
logoDiv.classList.add('logo');

const dropNav = document.createElement('nav');
dropNav.classList.add('drop_nav');

const dropdownMobile = document.createElement('div');
dropdownMobile.classList.add('dropdown_mobile');

const dropButton = document.createElement('button');
dropButton.classList.add('drop_button');

const mobileRoofImg = document.createElement('img');
mobileRoofImg.setAttribute('src', '../header_icons/roof.png');
mobileRoofImg.setAttribute('alt', 'roof');
mobileRoofImg.classList.add('roof');
mobileRoofImg.setAttribute('height', '25');
mobileRoofImg.setAttribute('width', '50');

dropButton.appendChild(mobileRoofImg);

const mobileContent = document.createElement('div');
mobileContent.classList.add('mobile_content');

const logoLink = document.createElement('a');
logoLink.setAttribute('href', '../header_page/index.html');

const logoImg = document.createElement('img');
logoImg.setAttribute('src', '../header_icons/logo.png');
logoImg.setAttribute('alt', 'logo');
logoImg.classList.add('logo_photo');
logoImg.setAttribute('height', '50');
logoImg.setAttribute('width', '230');

logoLink.appendChild(logoImg);
logoDiv.appendChild(logoLink);

const textAboutCompanyDiv = document.createElement('div');
textAboutCompanyDiv.classList.add('text_about_company');
textAboutCompanyDiv.innerHTML = 'Продажа и монтаж материалов для кровли и фасада<br> в Екатеринбурге';
logoDiv.appendChild(textAboutCompanyDiv);

const infoDiv = document.createElement('div');
infoDiv.classList.add('info');

const contactsUl = document.createElement('ul');
contactsUl.classList.add('contacts');

const emailLi = document.createElement('li');
const emailLink = document.createElement('a');
emailLink.setAttribute('href', 'mailto:3834520@mail.ru');

const emailImg = document.createElement('img');
emailImg.setAttribute('src', '../header_icons/email_logo.png');
emailImg.setAttribute('alt', 'whatsapp');
emailImg.setAttribute('height', '18');
emailImg.setAttribute('width', '27');

const emailSpan = document.createElement('span');
emailSpan.textContent = ' 3834520@mail.ru ';
emailLink.appendChild(emailImg);
emailLink.appendChild(emailSpan);
emailLi.appendChild(emailLink);
contactsUl.appendChild(emailLi);

const phoneLi = document.createElement('li');

const phoneLink = document.createElement('a');
phoneLink.setAttribute('href', 'tel:+73433834520');

const phoneImg = document.createElement('img');
phoneImg.setAttribute('src', '../header_icons/phone_logo.png');
phoneImg.setAttribute('alt', 'whatsapp');
phoneImg.setAttribute('height', '18');
phoneImg.setAttribute('width', '18');

const phoneSpan = document.createElement('span');
phoneSpan.textContent = ' +7 (343) 383-45-20 ';
phoneLink.appendChild(phoneImg);
phoneLink.appendChild(phoneSpan);
phoneLi.appendChild(phoneLink);
contactsUl.appendChild(phoneLi);

infoDiv.appendChild(contactsUl);

const whatsappLink = document.createElement('a');
whatsappLink.setAttribute('href', 'https://wa.me/73433834520');

const whatsappImg = document.createElement('img');
whatsappImg.setAttribute('src', '../header_icons/whatsapp.png');
whatsappImg.setAttribute('alt', 'whatsapp');
whatsappImg.classList.add('whatsapp');
whatsappImg.setAttribute('height', '40');
whatsappImg.setAttribute('width', '40');

whatsappLink.appendChild(whatsappImg);
infoDiv.appendChild(whatsappLink);

logoDiv.appendChild(infoDiv);

header.appendChild(logoDiv);
