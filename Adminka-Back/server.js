const express = require('express');
const path = require('path');
const app = express();

app.use(express.static('/html'));

app.get('/', (req, res) => {
    res.sendFile('demo.html', {root: "./html"});
});

app.listen(3333, () => {
    console.log('http://localhost:3333');
});