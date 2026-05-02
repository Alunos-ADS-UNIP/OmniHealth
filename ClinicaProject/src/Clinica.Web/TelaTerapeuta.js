document.addEventListener("DOMContentLoaded", () => {
    const el = {
        dataInput: document.getElementById("dataGestao"),
        grid: document.getElementById("gridGestao"),
        listaGeral: document.getElementById("listaGeralPacientes"),
        countHoje: document.getElementById("countHoje"),
        countSemana: document.getElementById("countSemana"),
        countMes: document.getElementById("countMes")
    };

    const gradeBase = ["10:00","10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00","14:30",  "15:00", "15:30",  "16:00", "16:30",  "17:00", "17:30",  "18:00"];

    // Data inicial como hoje
    el.dataInput.value = new Date().toISOString().split('T')[0];

    const atualizarDashboardELista = (agenda) => {
        const hojeStr = new Date().toISOString().split('T')[0];
        let totalHoje = 0, totalSemana = 0, totalMes = 0;
        
        el.listaGeral.innerHTML = "";
        let temPacientes = false;

        // Ordenar datas para a lista ficar organizada
        const datasOrdenadas = Object.keys(agenda).sort();

        datasOrdenadas.forEach(data => {
            const dataObj = new Date(data);
            const diffDias = Math.floor((dataObj - new Date(hojeStr)) / (1000 * 60 * 60 * 24));

            Object.keys(agenda[data]).forEach(hora => {
                const info = agenda[data][hora];
                
                if (info.paciente) {
                    temPacientes = true;
                    // Contagem dashboard
                    if (data === hojeStr) totalHoje++;
                    if (diffDias >= 0 && diffDias <= 7) totalSemana++;
                    if (diffDias >= 0 && diffDias <= 30) totalMes++;

                    // Construção da lista lateral
                    el.listaGeral.innerHTML += `
                        <div class="paciente-item-geral">
                            <div class="info">
                                <strong>${info.paciente}</strong>
                                <span>📅 ${data.split('-').reverse().join('/')} — ${hora}</span>
                            </div>
                            <div class="status-tag">Confirmado</div>
                        </div>`;
                }
            });
        });

        el.countHoje.innerText = totalHoje;
        el.countSemana.innerText = totalSemana;
        el.countMes.innerText = totalMes;
        if (!temPacientes) el.listaGeral.innerHTML = "<p class='vazio'>Aguardando novos agendamentos...</p>";
    };

    const carregarTela = () => {
        const agendaGlobal = JSON.parse(localStorage.getItem('agendaFisioData')) || {};
        const dataSel = el.dataInput.value;

        if (!agendaGlobal[dataSel]) agendaGlobal[dataSel] = {};

        // Renderiza Grid de Cadeados
        el.grid.innerHTML = "";
        gradeBase.forEach(hora => {
            const info = agendaGlobal[dataSel][hora] || { status: "disponivel", paciente: null };
            const isBloqueado = info.status === "bloqueado";

            const btn = document.createElement("button");
            btn.className = `btn-gestao ${isBloqueado ? 'bloqueado' : 'liberado'}`;
            btn.innerHTML = isBloqueado ? `🔒 ${hora}` : `🔓 ${hora}`;

            btn.onclick = () => {
                if (info.paciente && !isBloqueado) {
                    if (!confirm(`Desmarcar o paciente ${info.paciente} e bloquear o horário?`)) return;
                }
                
                const novoStatus = isBloqueado ? "disponivel" : "bloqueado";
                agendaGlobal[dataSel][hora] = { status: novoStatus, paciente: null };
                
                localStorage.setItem('agendaFisioData', JSON.stringify(agendaGlobal));
                carregarTela();
            };
            el.grid.appendChild(btn);
        });

        atualizarDashboardELista(agendaGlobal);
    };

    el.dataInput.onchange = carregarTela;
    carregarTela();
});
