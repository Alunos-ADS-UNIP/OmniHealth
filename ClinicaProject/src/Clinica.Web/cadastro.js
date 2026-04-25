window.onload = function() {
    const btnCadastrar = document.getElementById("btnCadastrar");
    const inputNome = document.getElementById("nomeCadastro");
    const inputCPF = document.getElementById("cpfCadastro"); // Novo campo de CPF
    const inputEmail = document.getElementById("emailCadastro");
    const inputSenha = document.getElementById("senhaCadastro");
    const URL_API = "http://localhost:8000/register";

    // --- MÁSCARA DE CPF EM TEMPO REAL ---
    if (inputCPF) {
        inputCPF.addEventListener("input", function(e) {
            let v = e.target.value.replace(/\D/g, ""); // Remove tudo que não é número

            if (v.length <= 11) {
                v = v.replace(/(\d{3})(\d)/, "$1.$2");
                v = v.replace(/(\d{3})(\d)/, "$1.$2");
                v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
            }
            e.target.value = v;
            inputCPF.classList.remove("input-erro");
        });
    }

    // --- LÓGICA DE ENVIO DO CADASTRO ---
    if (btnCadastrar) {
        btnCadastrar.onclick = async function() {
            const nome = inputNome.value.trim();
            const cpfLimpo = inputCPF.value.replace(/\D/g, ""); // Tira pontos e traço para a API
            const email = inputEmail.value.trim();
            const senha = inputSenha.value.trim();

            // Validação simples
            if (!nome || cpfLimpo.length < 11 || !email.includes("@") || senha.length < 6) {
                alert("⚠️ Verifique os campos:\n- Nome é obrigatório\n- CPF deve estar completo\n- E-mail deve ser válido\n- Senha: mín. 6 caracteres");
                return;
            }

            try {
                const response = await fetch(URL_API, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ 
                        nome: nome,
                        cpf: cpfLimpo, // Enviando apenas os 11 números
                        email: email, 
                        senha: senha 
                    })
                });

                if (response.ok) {
                    alert("✅ Cadastro realizado com sucesso! Faça seu login.");
                    window.location.href = "./login.html";
                } else {
                    const errorData = await response.json();
                    alert(errorData.detail || "Erro ao cadastrar. Verifique se o CPF ou E-mail já existem.");
                }
            } catch (err) {
                console.error("Erro de conexão:", err);
                alert("Erro ao conectar com o servidor.");
            }
        };
    }
};
