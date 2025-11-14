
<template>
  <div class="login-container">
    <div class="login-card">
      <div class="login-header">
        <h1>📋 Agenda de Contatos</h1>
        <p>Faça login para continuar</p>
      </div>

      <form @submit.prevent="handleSubmit" class="login-form">
        <!-- Email -->
        <div class="form-group">
          <label for="email">Email</label>
          <input
            id="email"
            v-model="form.email"
            type="email"
            placeholder="seu@email.com"
            class="form-input"
            @blur="validateField('email')"
          />
          <span v-if="errors.email" class="error">{{ errors.email }}</span>
        </div>

        <!-- Senha -->
        <div class="form-group">
          <label for="password">Senha</label>
          <input
            id="password"
            v-model="form.password"
            type="password"
            placeholder="Digite sua senha"
            class="form-input"
            @blur="validateField('password')"
          />
          <span v-if="errors.password" class="error">{{ errors.password }}</span>
        </div>

        <!-- Erro de Login -->
        <div v-if="loginError" class="error-message">
          {{ loginError }}
        </div>

        <!-- Botão -->
        <button type="submit" class="btn-submit" :disabled="isLoading">
          {{ isLoading ? 'Entrando...' : 'Entrar' }}
        </button>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, computed } from 'vue'
import { useAuth } from '@/composables/useAuth'

interface LoginForm {
  email: string
  password: string
}

interface FormErrors {
  email?: string
  password?: string
}

const form = reactive<LoginForm>({
  email: '',
  password: ''
})

const errors = reactive<FormErrors>({})
const { login, loading, error } = useAuth()

// ✅ Computed para reatividade
const isLoading = computed(() => loading.value)
const loginError = computed(() => error.value)

const validateField = (field: keyof LoginForm) => {
  errors[field] = undefined

  if (field === 'email') {
    if (!form.email) {
      errors.email = 'Email é obrigatório'
    } else if (!isValidEmail(form.email)) {
      errors.email = 'Email inválido'
    }
  }

  if (field === 'password') {
    if (!form.password) {
      errors.password = 'Senha é obrigatória'
    } else if (form.password.length < 6) {
      errors.password = 'Senha deve ter no mínimo 6 caracteres'
    }
  }
}


const isValidEmail = (email: string): boolean => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  return emailRegex.test(email)
}

const handleSubmit = async () => {
    await login(form.email, form.password)
}
</script>

<style scoped>
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  padding: 1rem;
}

.login-card {
  width: 100%;
  max-width: 400px;
  background: white;
  border-radius: 12px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.3);
  overflow: hidden;
}

.login-header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 2rem;
  text-align: center;
}

.login-header h1 {
  margin: 0;
  font-size: 1.75rem;
  font-weight: 700;
}

.login-header p {
  margin: 0.5rem 0 0;
  font-size: 0.95rem;
  opacity: 0.9;
}

.login-form {
  padding: 2rem;
}

.form-group {
  margin-bottom: 1.5rem;
}

.form-group label {
  display: block;
  font-weight: 600;
  margin-bottom: 0.5rem;
  color: #1e293b;
  font-size: 0.95rem;
}

.form-input {
  width: 100%;
  padding: 0.75rem 1rem;
  border: 2px solid #e2e8f0;
  border-radius: 6px;
  font-size: 1rem;
  transition: all 0.3s ease;
  font-family: inherit;
}

.form-input:focus {
  outline: none;
  border-color: #667eea;
  box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
}

.form-input::placeholder {
  color: #94a3b8;
}

.error {
  display: block;
  color: #ef4444;
  font-size: 0.85rem;
  margin-top: 0.25rem;
  font-weight: 500;
}

.error-message {
  background-color: #fee2e2;
  border: 1px solid #fecaca;
  color: #991b1b;
  padding: 0.75rem 1rem;
  border-radius: 6px;
  margin-bottom: 1.5rem;
  font-size: 0.95rem;
}

.btn-submit {
  width: 100%;
  padding: 0.75rem 1rem;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border: none;
  border-radius: 6px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;
}

.btn-submit:hover:not(:disabled) {
  background: linear-gradient(135deg, #5568d3 0%, #6a398f 100%);
  transform: translateY(-2px);
  box-shadow: 0 5px 20px rgba(102, 126, 234, 0.3);
}

.btn-submit:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}
</style>
