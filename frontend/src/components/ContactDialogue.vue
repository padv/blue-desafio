<template>
  <div v-if="visible" class="dialog-overlay" @click="closeDialog">
    <div class="dialog-content" @click.stop>
      <div class="dialog-header">
        <h2>{{ contact ? 'Editar Contato' : 'Novo Contato' }}</h2>
        <button class="btn-close" @click="closeDialog">✕</button>
      </div>

      <form @submit.prevent="handleSubmit" class="dialog-form">
        <!-- Nome -->
        <div class="form-group">
          <label>Nome</label>
          <input v-model="formData.nome" type="text" placeholder="Nome" required />
        </div>

        <!-- Email -->
        <div class="form-group">
          <label>Email</label>
          <input v-model="formData.email" type="email" placeholder="Email" required />
        </div>

        <!-- Telefone -->
        <div class="form-group">
          <label>Telefone</label>
          <input
            v-model="formData.telefone"
            type="text"
            placeholder="(XX) XXXXX-XXXX"
            maxlength="16"
            @input="formatPhoneNumber"
            required
          />
        </div>

        <!-- Botões -->
        <div class="dialog-actions">
          <button type="button" class="btn-cancel" @click="closeDialog">Cancelar</button>
          <button type="submit" class="btn-save">{{ contact ? 'Atualizar' : 'Criar' }}</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, watch } from 'vue'
import type { Contact, CreateContactDto } from '@/types/Contact'

interface Props {
  visible: boolean
  contact?: Contact | null
}

const props = withDefaults(defineProps<Props>(), {
  contact: null
})

const emit = defineEmits<{
  'update:visible': [value: boolean]
  save: [data: CreateContactDto]
  close: []
}>()

const formData = reactive({
  nome: '',
  email: '',
  telefone: ''
})

// Atualizar form quando contact muda
watch(
  () => props.contact,
  (newContact) => {
    if (newContact) {
      formData.nome = newContact.nome
      formData.email = newContact.email
      formData.telefone = newContact.telefone
    } else {
      formData.nome = ''
      formData.email = ''
      formData.telefone = ''
    }
  },
  { immediate: true }
)

// ✅ Formatar número de telefone automaticamente
const formatPhoneNumber = () => {
  // Remove tudo que não é número
  let phone = formData.telefone.replace(/\D/g, '')

  // Limitar a 11 dígitos
  if (phone.length > 11) {
    phone = phone.slice(0, 11)
  }

  // Aplicar máscara
  if (phone.length === 0) {
    formData.telefone = ''
  } else if (phone.length <= 2) {
    // (XX
    formData.telefone = `(${phone}`
  } else if (phone.length <= 7) {
    // (XX) XXXXX
    formData.telefone = `(${phone.slice(0, 2)}) ${phone.slice(2)}`
  } else {
    // (XX) XXXXX-XXXX
    formData.telefone = `(${phone.slice(0, 2)}) ${phone.slice(2, 7)}-${phone.slice(7, 11)}`
  }
}

const handleSubmit = () => {
  emit('save', { ...formData })
  closeDialog()
}

const closeDialog = () => {
  emit('update:visible', false)
  emit('close')
}
</script>

<style scoped>
.dialog-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.dialog-content {
  background: white;
  border-radius: 12px;
  box-shadow: 0 10px 40px rgba(0, 0, 0, 0.2);
  width: 100%;
  max-width: 500px;
  max-height: 90vh;
  overflow-y: auto;
}

.dialog-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1.5rem;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border-bottom: none;
}

.dialog-header h2 {
  margin: 0;
  font-size: 1.25rem;
}

.btn-close {
  background: rgba(255, 255, 255, 0.2);
  border: none;
  color: white;
  font-size: 1.5rem;
  cursor: pointer;
  padding: 0;
  width: 32px;
  height: 32px;
  border-radius: 4px;
  transition: all 0.3s ease;
}

.btn-close:hover {
  background: rgba(255, 255, 255, 0.3);
}

.dialog-form {
  padding: 1.5rem;
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

.form-group input {
  width: 100%;
  padding: 0.75rem 1rem;
  border: 2px solid #e2e8f0;
  border-radius: 6px;
  font-size: 1rem;
  font-family: inherit;
  transition: all 0.3s ease;
}

.form-group input:focus {
  outline: none;
  border-color: #667eea;
  box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
}

.dialog-actions {
  display: flex;
  gap: 1rem;
  justify-content: flex-end;
  padding-top: 1rem;
  border-top: 1px solid #e2e8f0;
}

.btn-cancel,
.btn-save {
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 6px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;
  font-size: 0.95rem;
}

.btn-cancel {
  background-color: #f1f5f9;
  color: #475569;
}

.btn-cancel:hover {
  background-color: #e2e8f0;
}

.btn-save {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
}

.btn-save:hover {
  background: linear-gradient(135deg, #5568d3 0%, #6a398f 100%);
  transform: translateY(-2px);
  box-shadow: 0 5px 20px rgba(102, 126, 234, 0.3);
}
</style>
