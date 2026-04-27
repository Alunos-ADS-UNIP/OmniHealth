document.addEventListener("DOMContentLoaded", () => {
    const elements = {
        btnAgendarPrincipal: document.getElementById("btnAgendar"),
        selectFisio: document.getElementById("selectFisio"),
        modalAgenda: document.getElementById("modalAgenda"),
        modalEspecialista: document.getElementById("modalEspecialista"),
        modalData: document.getElementById("modalData"),
        gridHorarios: document.getElementById("gridHorarios"),
        btnConfirmarAgendamento: document.getElementById("btnConfirmarAgendamento"),
        erroModal: document.getElementById("erroModal"),
        textoErroModal: document.getElementById("textoErroModal"),
        modalSucesso: document.getElementById("modalSucesso"),
        resumoMedico: document.getElementById("resumoMedico"),
        resumoData: document.getElementById("resumoData"),
        resumoHora: document.getElementById("resumoHora"),
        btnProximoPasso: document.getElementById("btnProximoPasso"),
        modalNavegacao: document.getElementById("modalNavegacao"),
        toastErro: document.getElementById("toastErro"),
        msgErroPrincipal: document.getElementById("msgErro")
    };

    let horaSelecionada = "";

    const gradeHorarios = [
        "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", 
        "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", 
        "17:00", "17:30", "18:00"
    ];

    // --- NOVA FUNÇÃO PARA OBTER HORA DE BRASÍLIA ---
    const getAgoraBrasilia = () => {
        return new Date(new Date().toLocaleString("en-US", {timeZone: "America/Sao_Paulo"}));
    };

    const renderizarHorariosSincronizados = () => {
        elements.gridHorarios.innerHTML = "";
        const dataSel = elements.modalData.value; // Formato YYYY-MM-DD
        if (!dataSel) return;

        const agora = getAgoraBrasilia();
        const hojeDataIso = agora.toISOString().split('T')[0];
        
        const agendaGlobal = JSON.parse(localStorage.getItem('agendaFisioData')) || {};
        const bloqueiosDoDia = agendaGlobal[dataSel] || {};

        gradeHorarios.forEach(hora => {
            const statusInfo = bloqueiosDoDia[hora] || { status: "disponivel" };
            const isOcupado = statusInfo.status === "bloqueado";
            
            // --- LÓGICA DE BLOQUEIO DE HORÁRIOS PASSADOS ---
            const [h, m] = hora.split(":").map(Number);
            const dataHoraComparacao = new Date(dataSel);
            dataHoraComparacao.setHours(h, m, 0, 0);

            // Bloqueia se: 1. Já está ocupado OU 2. O horário já passou (no dia de hoje)
            const isPassado = dataSel === hojeDataIso && (agora.getHours() > h || (agora.getHours() === h && agora.getMinutes() >= m));

            const btn = document.createElement("button");
            btn.type = "button";
            btn.innerText = hora;
            
            if (isOcupado || isPassado) {
                btn.className = "btn-hora ocupado"; 
                btn.disabled = true;
                if (isPassado) btn.title = "Horário já passou";
            } else {
                btn.className = "btn-hora";
                btn.onclick = () => {
                    document.querySelectorAll(".btn-hora").forEach(b => b.classList.remove("active"));
                    btn.classList.add("active");
                    horaSelecionada = hora;
                    if (elements.erroModal) elements.erroModal.style.display = "none";
                };
            }
            elements.gridHorarios.appendChild(btn);
        });
    };

    elements.btnAgendarPrincipal.onclick = () => {
        if (!elements.selectFisio.value) {
            elements.msgErroPrincipal.innerText = "⚠️ Selecione um especialista!";
            elements.toastErro.classList.add("show");
            setTimeout(() => elements.toastErro.classList.remove("show"), 3000);
            return;
        }

        // --- BLOQUEIA DIAS PASSADOS NO CALENDÁRIO ---
        const hoje = getAgoraBrasilia().toISOString().split("T")[0];
        elements.modalData.setAttribute("min", hoje);

        elements.modalEspecialista.value = elements.selectFisio.value;
        elements.modalData.value = "";
        elements.gridHorarios.innerHTML = "<p style='grid-column:1/-1; color:gray;'>Escolha uma data...</p>";
        elements.modalAgenda.showModal();
    };

    elements.modalData.onchange = renderizarHorariosSincronizados;

    elements.btnConfirmarAgendamento.onclick = () => {
        const dataRaw = elements.modalData.value;
        const agora = getAgoraBrasilia();

        if (!dataRaw || !horaSelecionada) {
            elements.textoErroModal.innerText = "❌ Selecione data e hora.";
            elements.erroModal.style.display = "block";
            return;
        }

        // --- VALIDAÇÃO FINAL ANTES DE SALVAR ---
        const [h, m] = horaSelecionada.split(":").map(Number);
        const dataHoraAgendamento = new Date(dataRaw);
        dataHoraAgendamento.setHours(h, m, 0, 0);

        if (dataHoraAgendamento < agora) {
            alert("Erro: Você não pode confirmar um agendamento para um horário que já passou.");
            renderizarHorariosSincronizados(); // Atualiza a tela para sumir o horário vencido
            return;
        }

        const agendaGlobal = JSON.parse(localStorage.getItem('agendaFisioData')) || {};
        if (!agendaGlobal[dataRaw]) agendaGlobal[dataRaw] = {};
        
        agendaGlobal[dataRaw][horaSelecionada] = {
            status: "bloqueado",
            paciente: "Davi Gusmão"
        };
        localStorage.setItem('agendaFisioData', JSON.stringify(agendaGlobal));

        const consultasPaciente = JSON.parse(localStorage.getItem("consultas_fisio")) || [];
        consultasPaciente.push({
            especialista: elements.modalEspecialista.value,
            data: dataRaw.split("-").reverse().join("/"),
            hora: horaSelecionada,
            id: Date.now()
        });
        localStorage.setItem("consultas_fisio", JSON.stringify(consultasPaciente));

        elements.resumoMedico.innerText = elements.modalEspecialista.value;
        elements.resumoData.innerText = dataRaw.split("-").reverse().join("/");
        elements.resumoHora.innerText = horaSelecionada;
        
        elements.modalAgenda.close();
        elements.modalSucesso.showModal();
    };

    elements.btnProximoPasso.onclick = () => {
        elements.modalSucesso.close();
        elements.modalNavegacao.showModal();
    };
});
