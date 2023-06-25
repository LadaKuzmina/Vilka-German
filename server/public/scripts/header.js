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

const catalogLink = document.createElement('a');
catalogLink.setAttribute('href', '../header_page/index.html');
catalogLink.textContent = 'Каталог';
mobileContent.appendChild(catalogLink);

const mobileAboutUsLink = document.createElement('a');
mobileAboutUsLink.setAttribute('href', '../header_page/page_about_us.html');
mobileAboutUsLink.textContent = 'О нас';
mobileContent.appendChild(mobileAboutUsLink);

const mobileDeliveryLink = document.createElement('a');
mobileDeliveryLink.setAttribute('href', '../header_page/delivery_page.html');
mobileDeliveryLink.textContent = 'Доставка';
mobileContent.appendChild(mobileDeliveryLink);

const mobileContactLink = document.createElement('a');
mobileContactLink.setAttribute('href', '../header_page/contact_page.html');
mobileContactLink.textContent = 'Контакты';
mobileContent.appendChild(mobileContactLink);

const mobileD3Div = document.createElement('div');
mobileD3Div.classList.add('d3');

const mobileSearchForm = document.createElement('form');
mobileSearchForm.classList.add('search');

const mobileSearchInput = document.createElement('input');
mobileSearchInput.setAttribute('type', 'text');
mobileSearchInput.setAttribute('placeholder', 'Искать здесь...');

const mobileSearchButton = document.createElement('button');
mobileSearchButton.setAttribute('type', 'submit');

mobileSearchForm.appendChild(mobileSearchInput);
mobileSearchForm.appendChild(mobileSearchButton);
mobileD3Div.appendChild(mobileSearchForm);
mobileContent.appendChild(mobileD3Div);

dropdownMobile.appendChild(dropButton);
dropdownMobile.appendChild(mobileContent);
dropNav.appendChild(dropdownMobile);
logoDiv.appendChild(dropNav);

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

const containerDiv = document.createElement('div');
containerDiv.classList.add('container');

const headerInnerDiv = document.createElement('div');
headerInnerDiv.classList.add('header_inner');

const nav = document.createElement('nav');
nav.classList.add('nav');

const dropdownDiv = document.createElement('div');
dropdownDiv.classList.add('dropdown');

const dropdownButton = document.createElement('button');
dropdownButton.classList.add('dropdown_button');
dropdownButton.textContent = 'Каталог';
dropdownButton.setAttribute('onclick', 'getDropdown()');

const roofImg = document.createElement('img');
roofImg.setAttribute('src', '../header_icons/roof.png');
roofImg.setAttribute('alt', 'roof');
roofImg.classList.add('roof');
roofImg.setAttribute('height', '25');
roofImg.setAttribute('width', '50');

dropdownButton.prepend(roofImg);

const dropdownContentDiv = document.createElement('div');
dropdownContentDiv.classList.add('dropdown_content');
dropdownContentDiv.setAttribute('id', 'dropdown');

const roofLink = document.createElement('a');
roofLink.classList.add('nav_link');
roofLink.setAttribute('href', '../catalog/roof.html');
roofLink.textContent = 'Кровля';
dropdownContentDiv.appendChild(roofLink);

const facadeLink = document.createElement('a');
facadeLink.classList.add('nav_link');
facadeLink.setAttribute('href', '../catalog/facade.html');
facadeLink.textContent = 'Фасад';
dropdownContentDiv.appendChild(facadeLink);

const waterSystemLink = document.createElement('a');
waterSystemLink.classList.add('nav_link');
waterSystemLink.setAttribute('href', '#');
waterSystemLink.textContent = 'Водосточные системы';
dropdownContentDiv.appendChild(waterSystemLink);

const dormerWindowLink = document.createElement('a');
dormerWindowLink.classList.add('nav_link');
dormerWindowLink.setAttribute('href', '../catalog/dormer_windows.html');
dormerWindowLink.textContent = 'Мансардные окна';
dropdownContentDiv.appendChild(dormerWindowLink);

const ventilationLink = document.createElement('a');
ventilationLink.classList.add('nav_link');
ventilationLink.setAttribute('href', '../catalog/ventilation.html');
ventilationLink.textContent = 'Вентиляция';
dropdownContentDiv.appendChild(ventilationLink);

const atticStairsLink = document.createElement('a');
atticStairsLink.classList.add('nav_link');
atticStairsLink.setAttribute('href', '#');
atticStairsLink.textContent = 'Чердачные лестницы';
dropdownContentDiv.appendChild(atticStairsLink);

const insulationMaterialsLink = document.createElement('a');
insulationMaterialsLink.classList.add('nav_link');
insulationMaterialsLink.setAttribute('href', '../catalog/insulation_materials.html');
insulationMaterialsLink.textContent = 'Изоляционные материалы';
dropdownContentDiv.appendChild(insulationMaterialsLink);

const fencesLink = document.createElement('a');
fencesLink.classList.add('nav_link');
fencesLink.setAttribute('href', '../catalog/fences.html');
fencesLink.textContent = 'Ограждения';
dropdownContentDiv.appendChild(fencesLink);

const accessoriesLink = document.createElement('a');
accessoriesLink.classList.add('nav_link');
accessoriesLink.setAttribute('href', '../catalog/accessories.html');
accessoriesLink.textContent = 'Комплектующие';
dropdownContentDiv.appendChild(accessoriesLink);

const landscapingSiteLink = document.createElement('a');
landscapingSiteLink.classList.add('nav_link');
landscapingSiteLink.setAttribute('href', '../catalog/landscaping_site.html');
landscapingSiteLink.textContent = 'Благоустройство';
dropdownContentDiv.appendChild(landscapingSiteLink);

dropdownDiv.appendChild(dropdownButton);
dropdownDiv.appendChild(dropdownContentDiv);
nav.appendChild(dropdownDiv);

const aboutUsLink = document.createElement('a');
aboutUsLink.classList.add('nav_link');
aboutUsLink.setAttribute('href', '../header_page/page_about_us.html');
aboutUsLink.textContent = 'О нас';
nav.appendChild(aboutUsLink);

const deliveryLink = document.createElement('a');
deliveryLink.classList.add('nav_link');
deliveryLink.setAttribute('href', '../header_page/delivery_page.html');
deliveryLink.textContent = 'Доставка';
nav.appendChild(deliveryLink);

const contactLink = document.createElement('a');
contactLink.classList.add('nav_link');
contactLink.setAttribute('href', '../header_page/contact_page.html');
contactLink.textContent = 'Контакты';
nav.appendChild(contactLink);

const d3Div = document.createElement('div');
d3Div.classList.add('d3');

const searchForm = document.createElement('form');
searchForm.classList.add('search');

const searchInput = document.createElement('input');
searchInput.setAttribute('type', 'text');
searchInput.setAttribute('placeholder', 'Искать здесь...');

const searchButton = document.createElement('button');
searchButton.setAttribute('type', 'submit');

searchForm.appendChild(searchInput);
searchForm.appendChild(searchButton);
d3Div.appendChild(searchForm);
nav.appendChild(d3Div);

headerInnerDiv.appendChild(nav);
containerDiv.appendChild(headerInnerDiv);
header.appendChild(containerDiv);

document.body.appendChild(header);

function headerStyle() {
    header.style.position = 'absolute';
    header.style.top = '15px';
    header.style.left = '30px';
    header.style.right = '30px';
    header.style.zIndex = '1000';
}

function logoImgStyle() {
    logoImg.style.marginTop = '8px';
}

function textAboutCompanyStyle() {
    textAboutCompanyDiv.style.display = 'inline-block';
    textAboutCompanyDiv.style.position = 'absolute';
    textAboutCompanyDiv.style.fontSize = '16px';
    textAboutCompanyDiv.style.margin = '15px 0 0 30px';
    textAboutCompanyDiv.style.fontFamily = 'Arial, Inter, serif';
}

function infoStyle() {
    infoDiv.style.display = 'flex';
    infoDiv.style.alignItems = 'center';
    infoDiv.style.position = 'absolute';
    infoDiv.style.flexDirection = 'row-reverse';
    infoDiv.style.top = '0';
    infoDiv.style.right = '0';
    infoDiv.style.fontSize = '40px';
    infoDiv.style.textAlign = 'center';
}

function contactsStyle() {
    contactsUl.style.listStyleType = 'none';
    contactsUl.style.fontStyle = 'normal';
    contactsUl.style.fontSize = '18px';
    contactsUl.style.lineHeight = '20px';
}

function navStyle() {
    nav.style.display = 'flex';
    nav.style.position = 'absolute';
    nav.style.justifyContent = 'space-around';
    nav.style.alignItems = 'center';
    nav.style.fontStyle = 'normal';
    nav.style.fontSize = '20px';
    nav.style.lineHeight = '36px';
    nav.style.height = '70px';
    nav.style.left = '0';
    nav.style.top = '80px';
    nav.style.right = '0';
    nav.style.color = '#492D2D';
    nav.style.background = '#FFFFFF';
    nav.style.borderRadius = '12px';
}

function roofImgStyle() {
    roofImg.style.position = 'relative';
    roofImg.style.top = '5px';
    roofImg.style.right = '5px';
}

function searchFormStyle() {
    searchForm.style.position = 'relative';
    searchForm.style.right = '0';
    searchForm.style.maxWidth = '35rem';
    searchForm.style.backgroundColor = '#fff';
    searchForm.style.boxShadow = '0 3px 60px 0 rgba(0, 0, 0, .15)';
    searchForm.style.borderRadius = '12px';
}

function searchInputStyle() {
    searchInput.style.border = 'none';
    searchInput.style.outline = 'none';
    searchInput.style.background = 'transparent';
    searchInput.style.width = '65%';
    searchInput.style.height = '53px';
    searchInput.style.paddingLeft = '16px';
    searchInput.style.fontSize = '17px';
    searchInput.style.color = 'black';
}

function searchButtonStyle() {
    searchButton.style.position = 'absolute';
    searchButton.style.top = '0';
    searchButton.style.right = '5px';
    searchButton.style.border = 'none';
    searchButton.style.background = 'url("../header_icons/magnifier.png") scroll no-repeat center';
    searchButton.style.width = '3.5rem';
    searchButton.style.height = '100%';
    searchButton.style.cursor = 'pointer';
}