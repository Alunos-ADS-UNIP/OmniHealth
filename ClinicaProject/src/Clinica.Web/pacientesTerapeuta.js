// ============================================================
// 1. ESTADO GLOBAL (Sincronizado com LocalStorage)
// ============================================================
const state = {
    get consultas() {
        // Busca dados e limpa entradas corrompidas
        const dados = JSON.parse(localStorage.getItem("consultas_fisio")) || [];
        return dados.filter(c => c && c.nome && c.data && c.id);
    },
    get hoje() {
        // Força o formato DD/MM/YYYY para garantir compatibilidade com o salvamento
        const d = new Date();
        const dia = String(d.getDate()).padStart(2, '0');
        const mes = String(d.getMonth() + 1).padStart(2, '0');
        const ano = d.getFullYear();
        return `${dia}/${mes}/${ano}`;
    },
    get mesAtual() {
        return (new Date().getMonth() + 1).toString().padStart(2, '0');
    }
};

const el = {
    grid: document.getElementById("gridConsultas"),
    dateText: document.getElementById("currentDate"),
    statsCards: document.querySelectorAll(".stat-value"),
    modalProntuario: document.getElementById("modalProntuario"),
    nomePacienteProntuario: document.getElementById("nomePacienteProntuario"),
    textoProntuario: document.getElementById("textoProntuario"), 
    btnSalvarProntuario: document.getElementById("btnSalvarProntuario"),
    btnBloquear: document.getElementById("btnBloquearAgenda"),
    modalAgenda: document.getElementById("modalAgenda")
};

// ============================================================
// 2. RENDERIZAÇÃO DO DASHBOARD
// ============================================================
function renderizarDashboard() {
    if (!el.grid) return;
    el.grid.innerHTML = ""; 
    
    const listaGeral = state.consultas;
    const hojeStr = state.hoje;
    
    // Pegar hora e minutos atuais para comparação
    const agora = new Date();
    const horaAtualMinutos = (agora.getHours() * 60) + agora.getMinutes();

    let totalHoje = 0;
    let totalMes = 0;

    const listaOrdenada = listaGeral.sort((a, b) => a.hora.localeCompare(b.hora));

    listaOrdenada.forEach(c => {
        const dataConsulta = c.data; 
        const mesConsulta = dataConsulta.split('/')[1];

        if (dataConsulta === hojeStr) totalHoje++;
        if (mesConsulta === state.mesAtual) totalMes++;

        if (dataConsulta === hojeStr) {
            // --- LÓGICA DE STATUS DINÂMICO ---
            const [horaC, minC] = c.hora.split(':').map(Number);
            const consultaMinutos = (horaC * 60) + minC;
            const tempoEstimadoSessao = 50; // Duração de 50 min

            let statusHTML = "";
            
            if (horaAtualMinutos < consultaMinutos) {
                // Ainda não chegou o horário
                statusHTML = `<div class="card-status status-azul">● Agendado</div>`;
            } else if (horaAtualMinutos >= consultaMinutos && horaAtualMinutos <= (consultaMinutos + tempoEstimadoSessao)) {
                // Está dentro do intervalo da consulta
                statusHTML = `<div class="card-status status-amarelo">● Em Atendimento</div>`;
            } else {
                // Já passou do horário + tempo de sessão
                statusHTML = `<div class="card-status status-verde">● Concluído</div>`;
            }
            // --------------------------------

            const card = document.createElement("div");
            card.className = "card-consulta";
            card.innerHTML = `
                <div class="card-horario">🕒 ${c.hora}</div>
                <div class="card-info">
                    <h3 class="card-paciente-nome">${c.nome}</h3>
                    <p class="card-especialista">${c.especialista || "Fisioterapia"}</p>
                </div>
                ${statusHTML}
                <div class="card-acoes">
                    <button class="btn-card-inline" data-id="${c.id}">
                        Ver Prontuário
                    </button>
                    <button class="btn-cancelar-discreto" onclick="cancelarConsulta('${c.id}')" title="Desmarcar">
                        ✕
                    </button>
                </div>
            `;
            el.grid.appendChild(card);
        }
    });

    // Atualiza contadores
    if (el.statsCards.length >= 3) {
        el.statsCards[0].innerText = totalHoje;
        el.statsCards[1].innerText = totalMes;
        el.statsCards[2].innerText = listaGeral.length;
    }

    if (totalHoje === 0) {
        el.grid.innerHTML = `<div class="card-vazio-msg">☕ Sem consultas para hoje.</div>`;
    }
}

// ============================================================
// 3. AÇÕES E EVENTOS
// ============================================================

// Função para o Terapeuta desmarcar uma consulta
window.cancelarConsulta = (id) => {
    if (confirm("Deseja desmarcar este paciente? O horário será liberado.")) {
        const consultas = JSON.parse(localStorage.getItem("consultas_fisio")) || [];
        const consultaParaRemover = consultas.find(c => String(c.id) === String(id));

        if (consultaParaRemover) {
            // 1. Remove da lista de consultas
            const novaLista = consultas.filter(c => String(c.id) !== String(id));
            localStorage.setItem("consultas_fisio", JSON.stringify(novaLista));

            // 2. Libera o horário na Agenda Global para que outros possam marcar
            const agendaGlobal = JSON.parse(localStorage.getItem('agendaFisioData')) || {};
            if (agendaGlobal[consultaParaRemover.dataISO]) {
                delete agendaGlobal[consultaParaRemover.dataISO][consultaParaRemover.hora];
                localStorage.setItem('agendaFisioData', JSON.stringify(agendaGlobal));
            }

            renderizarDashboard();
        }
    }
};

function bindEvents() {
    // Clique no botão "Ver Prontuário"
    document.addEventListener("click", (e) => {
        const btn = e.target.closest(".btn-card-inline");
        if (btn) {
            const idPac = btn.dataset.id;
            const consulta = state.consultas.find(c => String(c.id) === String(idPac));

            if (consulta) {
                el.nomePacienteProntuario.innerText = consulta.nome;
                el.textoProntuario.value = `--- QUEIXA DO PACIENTE ---\n${consulta.sintomas || "Nenhum relato."}\n\n--- EVOLUÇÃO MÉDICA ---\n`;
                el.modalProntuario.showModal();
            }
        }
    });

    el.btnSalvarProntuario?.addEventListener("click", () => {
        const btn = el.btnSalvarProntuario;
        btn.innerText = "⌛ Salvando...";
        btn.disabled = true;

        setTimeout(() => {
            alert(`✅ Evolução salva com sucesso!`);
            btn.innerText = "Salvar Evolução";
            btn.disabled = false;
            el.modalProntuario.close();
        }, 800);
    });

    el.btnBloquear?.addEventListener("click", () => el.modalAgenda.showModal());
}

// Sincronização entre abas
window.addEventListener('storage', (e) => {
    if (e.key === 'consultas_fisio' || e.key === 'agendaFisioData') {
        renderizarDashboard(); 
    }
});

// ============================================================
// 4. INICIALIZAÇÃO
// ============================================================
document.addEventListener("DOMContentLoaded", () => {
    if (el.dateText) {
        el.dateText.innerText = new Date().toLocaleDateString('pt-br', {
            weekday: 'long', day: 'numeric', month: 'long'
        });
    }
    renderizarDashboard();
    bindEvents();
});