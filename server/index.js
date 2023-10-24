const express = require('express');
const app = express();
const path = require('path');
app.use(express.static('public'));

app.get('/', (req, res) => {
    res.sendFile(__dirname + "/public/header_page/index.html");
})

app.get('/about', function (req, res) {
    res.sendFile(__dirname + "/public/header_page/page_about_us.html");
});

app.get('/contacts', function (req, res) {
    res.sendFile(__dirname + "/public/header_page/contact_page.html");
});

app.get('/delivery', function (req, res) {
    res.sendFile(__dirname + "/public/header_page/delivery_page.html");
});

app.get('/catalog', function (req, res) {
    res.sendFile(__dirname + "/public/catalog/catalog.html");
});

app.get('/subheadings', function (req, res) {
    res.sendFile(__dirname + "/public/catalog/subheadings.html");
});

app.get('/product', function (req, res) {
    res.sendFile(__dirname + "/public/product/product.html");
});

app.listen(3000, ()=>{
    console.log("Server is running")
})