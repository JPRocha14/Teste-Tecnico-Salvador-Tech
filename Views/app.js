async function fetchVagas() {
    const response = await fetch(apiUrl);
    const vagas = await response.json();
    const vagasList = document.getElementById("vagas-list");
    vagasList.innerHTML = '';

    vagas.forEach(vaga => {
        const div = document.createElement('div');
        div.innerHTML = `
            <h3>${vaga.title} - ${vaga.status}</h3>
            <button onclick="editVaga(${vaga.id})">Editar</button>
            <button onclick="deleteVaga(${vaga.id})">Excluir</button>
        `;
        vagasList.appendChild(div);
    });
}

function showCreateForm() {
    document.getElementById("create-form").style.display = 'block';
}

function hideCreateForm() {
    document.getElementById("create-form").style.display = 'none';
}

async function createVaga() {
    const title = document.getElementById("title").value;
    const status = document.getElementById("status").value;

    const vaga = {
        title: title,
        status: status,
        created_at: new Date().toISOString(),
        updated_at: new Date().toISOString()
    };

    const response = await fetch(apiUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(vaga)
    });

    if (response.ok) {
        alert('Vaga criada com sucesso!');
        fetchVagas();
        hideCreateForm();
    }
}

async function deleteVaga(id) {
    const response = await fetch(`${apiUrl}/${id}`, {
        method: 'DELETE'
    });

    if (response.ok) {
        alert('Vaga excluída com sucesso!');
        fetchVagas();
    }
}

async function editVaga(id) {
    const response = await fetch(`${apiUrl}/${id}`);
    const vaga = await response.json();
    const title = prompt('Novo título:', vaga.title);
    const status = prompt('Novo status:', vaga.status);

    const updatedVaga = { ...vaga, title, status, updated_at: new Date().toISOString() };

    await fetch(`${apiUrl}/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(updatedVaga)
    });

    fetchVagas();
}

fetchVagas();
