const modal = document.getElementById("createModal");
const enviar = document.getElementById("enviar");
const deletar = document.getElementById("deletar");
const tbody = document.querySelector("tbody");
const inputId = document.querySelector("#id");
const inputModelo = document.querySelector("#floatingModel");
const inputMarca = document.querySelector("#floatingMarca");
const inputAno = document.querySelector("#floatingAno");
const inputCor = document.querySelector("#floatingCor");

const div = document.querySelector("#see")
const divDelete = document.querySelector("#delete")

let id = 0;
let idDelete = 0;

const url = "https://localhost:7172/api/Carro";

const writeServer = (action, data) => ({
  method: action,
  body: JSON.stringify(data),
  headers: {
    'Content-Type': 'application/json; charset=UTF-8'
  }
});


// preenche modal ver
async function verItem(id) {
  const data = await getPorId(id)
  
  let dl = document.createElement("dl");
  dl.classList.add('row')
  
  dl.style.marginBottom = '0';
  div.innerHTML = ''

  dl.innerHTML = `
    <dt class="col-sm-4">ID </dt>
    <dd class="col-sm-8"> ${data.id} </dd>
    <hr>
    <dt class="col-sm-4"> Modelo </dt>
    
    <dd class="col-sm-8"> ${data.modelo} </dd>
    <hr>
    <dt class="col-sm-4">Marca</dt>
    <dd class="col-sm-8">${data.marca}</dd>
    <hr>
    <dt class="col-sm-4"> Ano </dt>
    <dd class="col-sm-8"> ${data.ano} </dd>
    <hr>
    <dt class="col-sm-4">Cor</dt>
    <dd class="col-sm-8"> ${data.cor} </dd>
    `;
    div.appendChild(dl);
}

// preenche modal delete
async function deleteItem(id) {
  const data = await getPorId(id)
  idDelete = data.id
  
  let dl = document.createElement("dl");
  dl.classList.add('row')
  
  dl.style.marginBottom = '0';
  divDelete.innerHTML = ''

  dl.innerHTML = `
    <dt class="col-sm-4">ID </dt>
    <dd id="viewid" class="col-sm-8"> ${data.id} </dd>
    <hr>
    <dt class="col-sm-4"> Modelo </dt>
    
    <dd class="col-sm-8"> ${data.modelo} </dd>
    <hr>
    <dt class="col-sm-4">Marca</dt>
    <dd class="col-sm-8">${data.marca}</dd>
    <hr>
    <dt class="col-sm-4"> Ano </dt>
    <dd class="col-sm-8"> ${data.ano} </dd>
    <hr>
    <dt class="col-sm-4">Cor</dt>
    <dd class="col-sm-8"> ${data.cor} </dd>
    `;
    divDelete.appendChild(dl);
}

// preenche modal criar, editar
async function editItem(id) {
  const data = await getPorId(id)
  
  inputId.value = data.id
  inputModelo.value = data.modelo
  inputMarca.value = data.marca
  inputAno.value = data.ano
  inputCor.value = data.cor
}

//GET po ID
async function getPorId(id) {
  const response = await fetch(`${url}/${id}`, writeServer('GET'));
  const data = await response.json();

  return data;
}

// POST e PUT
enviar.onclick = async () => {
  const carro = {
    modelo: inputModelo.value,
    marca: inputMarca.value,
    ano: inputAno.value,
    cor: inputCor.value
  }
  if (inputId.value != 0){
    carro.id = inputId.value
    await fetch(`${url}/${inputId.value}`, writeServer('PUT', carro));
    
    inputId.value = 0
    inputModelo.value = null
    inputMarca.value = null
    inputAno.value = 0
    inputCor.value = null
  }else{
    await fetch(url, writeServer('POST', carro));
  }

  loadCarros()
}

// DELETE 
deletar.onclick = async () => {
  console.log(idDelete)
  await fetch(`${url}/${idDelete}`, writeServer('DELETE'))

  loadCarros()
}

// GET ALL
async function loadCarros() {

    const response = await fetch(url,  writeServer('GET'));
    const data = await response.json();

    tbody.innerHTML = ''
    data.forEach(item => {
      let tr = document.createElement("tr");
      tr.innerHTML = `
        <td>${item.id}</td>
        <td>${item.modelo}</td>
        <td>${item.marca}</td>
        <td>${item.ano}</td>
        <td>${item.cor}</td>
        <td class="ver">
          <button onclick="verItem(${item.id})" class='btn btn-secondary' data-bs-toggle='modal' data-bs-target='#viewModal'>
          <svg xmlns="http://www.w3.org/2000/svg" width="22" height="22" fill="currentColor" class="me-2 bi bi-eye" viewBox="0 0 16 16">
          <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z"/>
          <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z"/>
          </svg>Ver</button>
        </td>

        <td class="edita">
        <button onclick="editItem(${item.id})" class='btn btn-warning' data-bs-toggle='modal' data-bs-target='#createModal'>
          <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="me-1 bi bi-pencil-square" viewBox="0 0 16 16">
          <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
          <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
          </svg>
          Editar</button>
        </td>

        <td class="delete">
        <button onclick="deleteItem(${item.id})" class='btn btn-danger' data-bs-toggle='modal' data-bs-target='#deleteModal'>
          <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="me-1 bi bi-trash" viewBox="0 0 16 16">
          <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
          <path fill-rule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
          </svg>
          Deletar</button>
        </td>
      `;
    tbody.appendChild(tr);
    });
};

loadCarros()
