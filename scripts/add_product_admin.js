const tableBody = document.querySelector('tbody');
let newRow;
// console.log(tableBody);
function addRow() {
    console.log(tableBody);
        newRow = document.createElement('tr');
        newRow.innerHTML = "<td><input type=\"text\" placeholder=\"Описание\"></td>\n" +
            "        <td><input type=\"text\" placeholder=\"Значение\"></td>";
    tableBody.appendChild(newRow);
}
