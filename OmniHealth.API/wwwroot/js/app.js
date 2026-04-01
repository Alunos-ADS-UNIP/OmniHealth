/**
 * OmniHealth — SPA Entry Point
 * Roteamento client-side simples baseado em hash.
 */

const API_BASE = '/api';

// ── Roteamento ────────────────────────────────────────────────────────────────
const routes = {
  '#/login':         () => import('./pages/auth.js').then(m => m.renderLogin()),
  '#/cadastro':      () => import('./pages/auth.js').then(m => m.renderCadastro()),
  '#/paciente':      () => import('./pages/paciente.js').then(m => m.render()),
  '#/medico':        () => import('./pages/medico.js').then(m => m.render()),
  '#/funcionario':   () => import('./pages/funcionario.js').then(m => m.render()),
  '#/farmaceutico':  () => import('./pages/farmaceutico.js').then(m => m.render()),
};

function navigate() {
  const hash = window.location.hash || '#/login';
  const render = routes[hash];
  if (render) {
    render();
  } else {
    window.location.hash = '#/login';
  }
}

window.addEventListener('hashchange', navigate);
window.addEventListener('load', navigate);
