const header = document.createElement('header');
header.classList.add('header');
// headerStyle();

const logoDiv = document.createElement('div');
logoDiv.classList.add('logo');

const logoA = document.createElement('a');
logoA.href = 'index.html';

const logoImg = document.createElement('img');
logoImg.src = '../header_icons/logo.png';
logoImg.alt = 'logo';
logoImg.height = '50';
logoImg.width = '230';
logoImg.classList.add('logo_photo');
// logoImgStyle();

logoA.appendChild(logoImg);
logoDiv.appendChild(logoA);

const textAboutCompanyDiv = document.createElement('div');
textAboutCompanyDiv.classList.add('text_about_company');
textAboutCompanyDiv.innerHTML = 'Продажа и монтаж материалов для кровли и фасада<br>в Екатеринбурге';
// textAboutCompanyStyle();

logoDiv.appendChild(textAboutCompanyDiv);

const infoDiv = document.createElement('div');
infoDiv.classList.add('info');
// infoStyle();

const contactsUl = document.createElement('ul');
contactsUl.classList.add('contacts');
// contactsStyle();

const emailLi = document.createElement('li');
const emailA = document.createElement('a');
emailA.href = 'mailto:3834520@mail.ru';

const emailImg = document.createElement('img');
emailImg.src = '../header_icons/email_logo.png';
emailImg.alt = 'whatsapp';
emailImg.height = '18';
emailImg.width = '27';

emailA.appendChild(emailImg);
emailA.innerHTML += ' 3834520@mail.ru ';
emailLi.appendChild(emailA);
contactsUl.appendChild(emailLi);

const phoneLi = document.createElement('li');
const phoneA = document.createElement('a');
phoneA.href = 'tel:+73433834520';

const phoneImg = document.createElement('img');
phoneImg.src = '../header_icons/phone_logo.png';
phoneImg.alt = 'whatsapp';
phoneImg.height = '18';
phoneImg.width = '18';

phoneA.appendChild(phoneImg);
phoneA.innerHTML += ' +7 (343) 383-45-20 ';
phoneLi.appendChild(phoneA);
contactsUl.appendChild(phoneLi);

infoDiv.appendChild(contactsUl);

const whatsappA = document.createElement('a');
whatsappA.href = 'https://wa.me/73433834520';

const whatsappImg = document.createElement('img');
whatsappImg.src = '../header_icons/whatsapp.png';
whatsappImg.alt = 'whatsapp';
whatsappImg.height = '40';
whatsappImg.width = '40';
whatsappImg.classList.add('whatsapp');

whatsappA.appendChild(whatsappImg);
infoDiv.appendChild(whatsappA);

logoDiv.appendChild(infoDiv);
header.appendChild(logoDiv);

const containerDiv = document.createElement('div');
containerDiv.classList.add('container');

const headerInnerDiv = document.createElement('div');
headerInnerDiv.classList.add('header_inner');

const nav = document.createElement('nav');
nav.classList.add('nav');
// navStyle();

const catalogLink = document.createElement('a');
catalogLink.classList.add('nav_link');
catalogLink.href = 'index.html';

const roofImg = document.createElement('img');
roofImg.src = '../header_icons/roof.png';
roofImg.alt = 'roof';
roofImg.height = '25';
roofImg.width = '50';
roofImg.classList.add('roof');
// roofImgStyle();

catalogLink.appendChild(roofImg);
catalogLink.innerHTML += ' Каталог ';
nav.appendChild(catalogLink);

const aboutUsLink = document.createElement('a');
aboutUsLink.classList.add('nav_link');
aboutUsLink.href = 'page_about_us.html';
aboutUsLink.innerHTML = ' О нас ';
nav.appendChild(aboutUsLink);

// const completedProjectsLink = document.createElement('a');
// completedProjectsLink.classList.add('nav_link');
// completedProjectsLink.href = 'page_complited_project.html';
// completedProjectsLink.innerHTML = ' Выполненные проекты ';
// nav.appendChild(completedProjectsLink);

// const servicesLink = document.createElement('a');
// servicesLink.classList.add('nav_link');
// servicesLink.href = 'services_page.html';
// servicesLink.innerHTML = ' Наши услуги ';
// nav.appendChild(servicesLink);

const deliveryLink = document.createElement('a');
deliveryLink.classList.add('nav_link');
deliveryLink.href = 'delivery_page.html';
deliveryLink.innerHTML = ' Доставка ';
nav.appendChild(deliveryLink);

const contactsLink = document.createElement('a');
contactsLink.classList.add('nav_link');
contactsLink.href = 'contact_page.html';
contactsLink.innerHTML = ' Контакты ';
nav.appendChild(contactsLink);

const searchDiv = document.createElement('div');
searchDiv.classList.add('d3');

const searchForm = document.createElement('form');
searchForm.classList.add('search');
// searchFormStyle();

const searchInput = document.createElement('input');
searchInput.type = 'text';
searchInput.placeholder = 'Искать здесь...';
// searchInputStyle();

const searchButton = document.createElement('button');
searchButton.type = 'submit';
// searchButtonStyle();

searchForm.appendChild(searchInput);
searchForm.appendChild(searchButton);
searchDiv.appendChild(searchForm);
nav.appendChild(searchDiv);

headerInnerDiv.appendChild(nav);
containerDiv.appendChild(headerInnerDiv);
header.appendChild(containerDiv);

document.body.append(header);

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