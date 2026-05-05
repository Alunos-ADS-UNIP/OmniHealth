// --- 1. CONFIGURAÇÕES INICIAIS E MAPEAMENTO ---
const elements = {
    btnAgendarPrincipal: document.getElementById('btnAgendar'),
    selectFisio: document.getElementById('selectFisio'),
    toastErro: document.getElementById('toastErro'),
    
    modalAgenda: document.getElementById('modalAgenda'),
    modalSucesso: document.getElementById('modalSucesso'),
    modalNavegacao: document.getElementById('modalNavegacao'),
    modalSintomas: document.getElementById('modalSintomas'),
    modalEspecialista: document.getElementById('modalEspecialista'),
    modalData: document.getElementById('modalData'),
    gridHorarios: document.getElementById('gridHorarios'),
    btnConfirmarAgendamento: document.getElementById('btnConfirmarAgendamento'),
    erroModal: document.getElementById('erroModal'),
    textoErroModal: document.getElementById('textoErroModal'),
    
    resumoMedico: document.getElementById('resumoMedico'),
    resumoData: document.getElementById('resumoData'),
    resumoHora: document.getElementById('resumoHora'),
    btnProximoPasso: document.getElementById('btnProximoPasso')
};

let horaSelecionada = null;

const gradeBase = ["10:00","10:30", "11:00", 
    "11:30", "12:00", "12:30", "13:00", "13:30", 
    "14:00","14:30", "15:00", "15:30", "16:00", 
    "16:30", "17:00", "17:30", "18:00"];

const getHojeBR = () => {
    const data = new Date();
    data.setMinutes(data.getMinutes() - data.getTimezoneOffset());
    return data.toISOString().split('T')[0];
};

// --- 2. LÓGICA DE ABERTURA ---

elements.btnAgendarPrincipal.onclick = () => {
    if (!elements.selectFisio.value) {
        elements.toastErro.classList.add("show");
        setTimeout(() => elements.toastErro.classList.remove("show"), 3000);
        return;
    }

    elements.modalEspecialista.value = elements.selectFisio.value;
    elements.modalData.setAttribute("min", getHojeBR());
    elements.modalData.value = "";
    elements.gridHorarios.innerHTML = "<p style='grid-column:1/-1; color:gray; text-align:center;'>Escolha uma data primeiro...</p>";
    elements.erroModal.style.display = "none";
    horaSelecionada = null;

    document.body.style.overflow = 'hidden';
    elements.modalAgenda.showModal();
    elements.modalSintomas.showModal();
};

// --- 3. RENDERIZAÇÃO DE HORÁRIOS (LOGICA DE TEMPO REAL E BLOQUEIOS) ---

elements.modalData.onchange = () => {
    const dataSel = elements.modalData.value;
    if (!dataSel) return;

    const agora = new Date();
    const hojeStr = getHojeBR();

    const agendaGlobal = JSON.parse(localStorage.getItem('agendaFisioData')) || {};
    const dadosDaData = agendaGlobal[dataSel] || {};

    elements.gridHorarios.innerHTML = "";
    horaSelecionada = null;
    
    gradeBase.forEach(hora => {
        const info = dadosDaData[hora] || { status: "disponivel" };
        const isBloqueadoPeloFisio = info.status === "bloqueado";

        // Lógica de Horário Passado
        let isPassado = false;
        if (dataSel === hojeStr) {
            const [h, m] = hora.split(':');
            const dataHoraBotao = new Date();
            dataHoraBotao.setHours(parseInt(h), parseInt(m), 0, 0);

            if (dataHoraBotao <= agora) {
                isPassado = true;
            }
        }

        const btn = document.createElement("button");
        btn.classList.add("btn-hora");
        btn.type = "button";
        
        // ORDEM DE IMPORTÂNCIA: 1º Se passou do horário, 2º Se está bloqueado/agendado
        if (isPassado) {
            btn.classList.add("encerrado"); 
            btn.disabled = true;
            btn.innerHTML = `<small style="font-size: 10px; display: block; opacity: 0.7;">Encerrado</small>${hora}`;
            btn.style.opacity = "0.5";
            btn.style.cursor = "not-allowed";
        } 
        else if (isBloqueadoPeloFisio) {
            btn.classList.add("ocupado"); 
            btn.disabled = true;
            btn.innerHTML = `🚫 ${hora}`;
            btn.style.cursor = "not-allowed";
        } 
        else {
            btn.classList.add("disponivel");
            btn.innerText = hora;
            btn.onclick = () => {
                document.querySelectorAll('.btn-hora').forEach(b => b.classList.remove('active'));
                btn.classList.add('active');
                horaSelecionada = hora;
                elements.erroModal.style.display = "none";
            };
        }
        
        elements.gridHorarios.appendChild(btn);
    });
};

// --- 4. CONFIRMAÇÃO (SALVAMENTO INTEGRADO) ---

elements.btnConfirmarAgendamento.onclick = () => {
    const dataRaw = elements.modalData.value;

    if (!dataRaw || !horaSelecionada) {
        elements.textoErroModal.innerText = "⚠️ Selecione a data e o horário.";
        elements.erroModal.style.display = "block";
        return;
    }

    const dataFormatada = dataRaw.split("-").reverse().join("/");

    // 1. Salva no histórico do paciente
    const novaConsulta = {
        especialista: elements.modalEspecialista.value,
        data: dataFormatada,
        hora: horaSelecionada,
        id: Date.now()
    };
    const consultas = JSON.parse(localStorage.getItem("consultas_fisio")) || [];
    consultas.push(novaConsulta);
    localStorage.setItem("consultas_fisio", JSON.stringify(consultas));

    // 2. Salva na agenda global para o Terapeuta ver e bloquear o horário
    const agendaGlobal = JSON.parse(localStorage.getItem('agendaFisioData')) || {};
    if (!agendaGlobal[dataRaw]) agendaGlobal[dataRaw] = {};
    
    agendaGlobal[dataRaw][horaSelecionada] = { 
        status: "bloqueado", 
        paciente: "Davi Gusmão" 
    };
    localStorage.setItem('agendaFisioData', JSON.stringify(agendaGlobal));

    // Feedback Visual e Transição
    elements.modalAgenda.close();
    setTimeout(() => {
        elements.resumoMedico.innerText = novaConsulta.especialista;
        elements.resumoData.innerText = novaConsulta.data;
        elements.resumoHora.innerText = novaConsulta.hora;
        elements.modalSucesso.showModal();
    }, 150);
};

// --- 5. NAVEGAÇÃO E FECHAMENTO ---

elements.btnProximoPasso.onclick = () => {
    elements.modalSucesso.close();
    setTimeout(() => {
        if(elements.modalNavegacao) elements.modalNavegacao.showModal();
    }, 150);
};
    elements.btnConfirmarAgendamento.onclick = () =>{
        setTimeout(() => {
            if(elements.btnConfirmarAgendamento) elements.modalSintomas.showModal();
        })
    }

const monitorarFechamento = (modal) => {
    if(!modal) return;
    modal.addEventListener('close', () => {
        // Só libera o scroll se nenhum outro modal estiver aberto
        const algumAberto = elements.modalAgenda.open || elements.modalSucesso.open || (elements.modalNavegacao && elements.modalNavegacao.open);
        if (!algumAberto) {
            document.body.style.overflow = 'auto';
        }
    });
};

[elements.modalAgenda, elements.modalSucesso, elements.modalNavegacao].forEach(monitorarFechamento);