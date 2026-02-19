/* Inject a simple token input into Swagger UI and add it to requests */
(function () {
  function addTokenInput() {
    const container = document.createElement('div');
    container.style.padding = '8px';
    container.style.display = 'flex';
    container.style.gap = '8px';
    container.style.alignItems = 'center';

    const input = document.createElement('input');
    input.placeholder = 'Bearer <token>';
    input.style.minWidth = '300px';
    input.id = 'swagger-auth-input';

    // prefill from localStorage if present
    const saved = localStorage.getItem('swagger_auth_token');
    if (saved) input.value = saved;

    const btn = document.createElement('button');
    btn.textContent = 'Set Header';
    btn.onclick = function () {
      let val = document.getElementById('swagger-auth-input').value;
      if (val) {
        // normalize: if user entered raw token, prefix with 'Bearer '
        if (!/^\s*Bearer\s+/i.test(val)) {
          val = 'Bearer ' + val.trim();
          input.value = val;
        }
        localStorage.setItem('swagger_auth_token', val);
        // try to preauthorize Swagger UI built-in authorize dialog (if available)
        try {
          if (window.ui && typeof window.ui.preauthorizeApiKey === 'function') {
            // try the security scheme name used in Program.cs
            window.ui.preauthorizeApiKey('Bearer', val);
          }
        } catch (e) {
          console.warn('preauthorizeApiKey failed', e);
        }
        alert('Authorization header saved');
      } else {
        localStorage.removeItem('swagger_auth_token');
        alert('Authorization header cleared');
      }
    };

    container.appendChild(input);
    container.appendChild(btn);

    const target = document.querySelector('.swagger-ui .topbar') || document.body;
    if (target) {
      // append inside the topbar so it is visible with the UI
      try {
        target.appendChild(container);
      } catch (e) {
        target.parentNode.insertBefore(container, target.nextSibling);
      }
    }
  }

  function waitForUiAndAttach(fn) {
    if (window.ui && typeof window.ui.getSystem === 'function') {
      return fn();
    }
    setTimeout(() => waitForUiAndAttach(fn), 200);
  }

  function addRequestInterceptor() {
    const system = window.ui.getSystem();
    const configs = system.getConfigs ? system.getConfigs() : (window.ui.getConfigs ? window.ui.getConfigs() : null);
    const previous = configs && configs.requestInterceptor ? configs.requestInterceptor : null;
    const interceptor = (req) => {
      const token = localStorage.getItem('swagger_auth_token');
      if (token) {
        req.headers = req.headers || {};
        req.headers['Authorization'] = token;
        req.headers['authorization'] = token;
      }
      return previous ? previous(req) : req;
    };

    if (configs) {
      configs.requestInterceptor = interceptor;
    } else if (system && typeof system.setConfig === 'function') {
      system.setConfig({ requestInterceptor: interceptor });
    }

    // also try to set built-in auth state so the Authorize dialog reflects the token
    try {
      const token = localStorage.getItem('swagger_auth_token');
      if (token && window.ui) {
        if (typeof window.ui.preauthorizeApiKey === 'function') {
          window.ui.preauthorizeApiKey('Bearer', token);
        }
      }
    } catch (e) {
      console.warn('preauthorize set failed', e);
    }
  }

  window.addEventListener('load', function () {
    addTokenInput();
    // wait for UI to be ready then attach interceptor
    waitForUiAndAttach(addRequestInterceptor);
  });
})();
