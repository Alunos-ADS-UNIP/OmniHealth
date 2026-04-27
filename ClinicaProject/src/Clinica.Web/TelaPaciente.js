document.addEventListener("DOMContentLoaded", () => {
    // 1. MAPEAMENTO DE ELEMENTOS
    const containerLista = document.getElementById("listaConsultas");
    const avisoVazio = document.getElementById("statusConsultaVazio");
    const areaAtalho = document.getElementById("areaAtalho");
    const modalAviso = document.getElementById("modalAvisoCancel");
    const btnConfirmarCancel = document.getElementById("confirmarCancelamento");

    let infoParaRemover = null; // Armazena data e hora para cancelamento

    // 2. FUNÇÃO PARA CARREGAR AS CONSULTAS DO BANCO SINCRONIZADO
    const carregarAgendaDoPaciente = () => {
        // Lê a agenda global (mesma chave usada pelo Terapeuta e no Agendamento)
        const agendaGlobal = JSON.parse(localStorage.getItem('agendaFisioData')) || {};
        const nomePacienteLogado = "Davi Gusmão"; // Nome que deve constar no agendamento

        // Limpa cards antigos para evitar duplicatas
        const cardsExistentes = containerLista.querySelectorAll(".card-consulta-item");
        cardsExistentes.forEach(card => card.remove());

        let encontrouConsulta = false;

        // Varre todas as datas e horários do banco
        Object.keys(agendaGlobal).sort().forEach(data => {
            Object.keys(agendaGlobal[data]).forEach(hora => {
                const info = agendaGlobal[data][hora];

                // Se o horário estiver ocupado por este paciente específico
                if (info.paciente === nomePacienteLogado) {
                    encontrouConsulta = true;

                    const card = document.createElement("div");
                    card.className = "card-moderno card-consulta-item";
                    card.style.display = "flex";
                    card.style.flexDirection = "column";
                    card.style.justifyContent = "space-between";
                    card.style.marginTop = "20px";

                    card.innerHTML = `
                        <div>
                            <span class="badge">Sessão Confirmada</span>
                            <h3 style="margin: 15px 0 10px 0;">Atendimento Fisioterapêutico</h3>
                            <p style="margin: 5px 0; color: var(--texto-muted);"><strong>📅 Data:</strong> ${data.split('-').reverse().join('/')}</p>
                            <p style="margin: 5px 0; color: var(--texto-muted);"><strong>🕒 Horário:</strong> ${hora}</p>
                        </div>
                        <button class="btn-outline-full" 
                                style="color: var(--error) !important; border-color: #ffcccc; margin-top: 20px;" 
                                onclick="prepararCancelamento('${data}', '${hora}')">
                            Desmarcar Sessão
                        </button>
                    `;
                    containerLista.appendChild(card);
                }
            });
        });

        // Controle de visibilidade dos avisos e botões de atalho
        if (!encontrouConsulta) {
            avisoVazio.style.display = "block";
            if (areaAtalho) areaAtalho.style.display = "none";
        } else {
            avisoVazio.style.display = "none";
            if (areaAtalho) areaAtalho.style.display = "block";
        }
    };

    // 3. LÓGICA DE CANCELAMENTO
    window.prepararCancelamento = (data, hora) => {
        infoParaRemover = { data, hora };
        if (modalAviso) modalAviso.showModal();
    };

    if (btnConfirmarCancel) {
        btnConfirmarCancel.onclick = () => {
            if (!infoParaRemover) return;

            const agendaGlobal = JSON.parse(localStorage.getItem('agendaFisioData')) || {};
            
            // Libera o horário no banco (volta para disponível e remove o paciente)
            if (agendaGlobal[infoParaRemover.data] && agendaGlobal[infoParaRemover.data][infoParaRemover.hora]) {
                agendaGlobal[infoParaRemover.data][infoParaRemover.hora] = { 
                    status: "disponivel", 
                    paciente: null 
                };
            }

            localStorage.setItem('agendaFisioData', JSON.stringify(agendaGlobal));
            
            if (modalAviso) modalAviso.close();
            carregarAgendaDoPaciente(); // Atualiza a tela sem precisar de reload total
        };
    }

    // Inicializa
    carregarAgendaDoPaciente();
});
