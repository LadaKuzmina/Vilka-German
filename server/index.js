const express = require('express');
const app = express();
const path = require('path');
app.use(express.static('public'));
app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, "public", "header_page",'index.html'))
})
app.listen(3000, ()=>{
    console.log("Server is running")
})