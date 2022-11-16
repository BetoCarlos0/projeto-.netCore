const modal = document.querySelector(".modal-container");
const tbody = document.querySelector("tbody");
const sNome = document.querySelector("#m-nome");
const sFuncao = document.querySelector("#m-funcao");
const sSalario = document.querySelector("#m-salario");
const btnSalvar = document.querySelector("#btnSalvar");

const url = "https://localhost:7172/api/Carro";

let itens;
let id;

// GET ALL
function insertItem(item, index) {
  let tr = document.createElement("tr");

  tr.innerHTML = `
        <td>${item.Modelo}</td>
        <td>${item.funcao}</td>
        <td>R$ ${item.salario}</td>
        <td class="acao">
        <button onclick="editItem(${index})"><i class='bx bx-edit' ></i></button>
        </td>
        <td class="acao">
        <button onclick="deleteItem(${index})"><i class='bx bx-trash'></i></button>
        </td>
    `;
  tbody.appendChild(tr);
}

// GET
tbody.addEventListener("click", () => {
  fetch(`${url}`)
  .then(getResponse)
  .then(processJSON);
});

function loadCarros() {
    let headers = new Headers();

    headers.append('Content-Type', 'application/json');
    headers.append('Accept', 'application/json');

    headers.append('Access-Control-Allow-Origin', 'http://127.0.0.1:5500/index.html');
    headers.append('Access-Control-Allow-Credentials', 'true');

    //headers.append('GET', 'POST', 'OPTIONS');
    fetch(url, { method: 'GET', mode: "no-cors"})
        .then(response => response.json())
        .then(json => console.log(json))
        .catch(function(error) {
            console.log('erro no fetch: ' + error.message);
        });
};

async function loadCarros2() {
    const response = await fetch(url, { method: 'GET' });
    const data = await response.json();
    console.log(data);
};

loadCarros()
