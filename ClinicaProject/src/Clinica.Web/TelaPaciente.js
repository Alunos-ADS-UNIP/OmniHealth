/* ============================================================
   1. LÓGICA GLOBAL DE CANCELAMENTO
   ============================================================ */

/**
 * Função global chamada estritamente pelo clique no botão do card.
 * Armazena os dados no modal e o exibe de forma controlada.
 */
window.prepararCancelamento = function(data, hora) {
    const modal = document.getElementById("modalAvisoCancel");
    
    if (modal) {
        // Garante que o modal esteja fechado antes de configurar novos dados
        if (modal.open) modal.close();

        // Vincula os dados da consulta específica ao modal
        modal.dataset.dataParaRemover = data;
        modal.dataset.horaParaRemover = hora;
        
        // Abre o modal apenas agora
        modal.showModal();
        
        // Foco no botão de cancelar para acessibilidade
        const btnManter = modal.querySelector(".btn-outline-full");
        if (btnManter) btnManter.focus();
    }
};

/* ============================================================
   2. INICIALIZAÇÃO E RENDERIZAÇÃO
   ============================================================ */

document.addEventListener("DOMContentLoaded", () => {
    const containerLista = document.getElementById("listaConsultas");
    const avisoVazio = document.getElementById("statusConsultaVazio");
    const areaAtalho = document.getElementById("areaAtalho");
    const btnConfirmarCancel = document.getElementById("confirmarCancelamento");

    /**
     * Renderiza os cards baseando-se no banco de dados local.
     */
    const carregarAgendaDoPaciente = () => {
        const agendaGlobal = JSON.parse(localStorage.getItem('agendaFisioData')) || {};
        const nomePacienteLogado = "Davi Gusmão"; 

        // Limpa apenas os cards gerados anteriormente
        const cardsExistentes = containerLista.querySelectorAll(".card-consulta-item");
        cardsExistentes.forEach(card => card.remove());

        let encontrouConsulta = false;

        // Ordenação por data para melhor visualização
        const datasOrdenadas = Object.keys(agendaGlobal).sort();

        datasOrdenadas.forEach(data => {
            Object.keys(agendaGlobal[data]).forEach(hora => {
                const info = agendaGlobal[data][hora];

                // Só renderiza se o paciente for o logado e o status for bloqueado
                if (info.paciente === nomePacienteLogado) {
                    encontrouConsulta = true;

                    const card = document.createElement("div");
                    card.className = "card-moderno card-consulta-item";
                    
                    card.innerHTML = `
                        <div class="card-body-info">
                            <span class="badge">Sessão Confirmada</span>
                            <h3 class="card-titulo-sessao">Atendimento Fisioterapêutico</h3>
                            <p class="card-detalhe"><strong>📅 Data:</strong> ${data.split('-').reverse().join('/')}</p>
                            <p class="card-detalhe"><strong>🕒 Horário:</strong> ${hora}</p>
                        </div>
                        <button type="button" class="btn-cancelar-sessao" 
                                onclick="window.prepararCancelamento('${data}', '${hora}')">
                            Desmarcar Sessão
                        </button>
                    `;
                    containerLista.appendChild(card);
                }
            });
        });

        // Gerenciamento de visibilidade de placeholders
        if (avisoVazio) avisoVazio.style.display = encontrouConsulta ? "none" : "block";
        if (areaAtalho) areaAtalho.style.display = encontrouConsulta ? "block" : "none";
    };

    /**
     * Ação de confirmação de exclusão (Botão "Sim" do Modal)
     */
    if (btnConfirmarCancel) {
        btnConfirmarCancel.onclick = () => {
            const modal = document.getElementById("modalAvisoCancel");
            const data = modal.dataset.dataParaRemover;
            const hora = modal.dataset.horaParaRemover;

            if (!data || !hora) return;

            const agendaGlobal = JSON.parse(localStorage.getItem('agendaFisioData')) || {};
            
            if (agendaGlobal[data] && agendaGlobal[data][hora]) {
                // Deleta a entrada e limpa chaves de datas vazias
                delete agendaGlobal[data][hora];
                if (Object.keys(agendaGlobal[data]).length === 0) {
                    delete agendaGlobal[data];
                }
            }

            // Persiste no storage e fecha a interface
            localStorage.setItem('agendaFisioData', JSON.stringify(agendaGlobal));
            modal.close();
            
            // Recarrega a lista dinamicamente
            carregarAgendaDoPaciente();
        };
    }

    // Executa a carga inicial
    carregarAgendaDoPaciente();
});
