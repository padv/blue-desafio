<template>
  <div class="contacts-view">
    <!-- Header -->
    <AppHeader @logout="handleLogout" />

    <!-- Main Content -->
    <div class="contacts-container">
      <div class="contacts-header">
        <div class="header-title">
          <h1>📋 Meus Contatos</h1>
          <p>Gerencie seus contatos de forma simples e rápida</p>
        </div>

        <button @click="openDialog()" class="btn-new">
          ➕ Novo Contato
        </button>
      </div>

      <!-- Loading State -->
      <div v-if="isLoading" class="loading">Carregando...</div>

      <!-- Error State -->
      <div v-if="hasError" class="error-box">
        ⚠️ {{ errorMessage }}
      </div>

      <!-- Contacts Table -->
      <ContactTable
        v-if="!isLoading && contacts.length > 0"
        :contacts="contacts"
        @edit="openDialog"
        @delete="deleteContact"
      />

      <!-- Empty State -->
      <div v-if="!isLoading && contacts.length === 0 && !hasError" class="empty-state">
        <p>📭 Nenhum contato encontrado</p>
        <p style="color: #64748b; font-size: 0.9rem;">Clique em "Novo Contato" para começar</p>
      </div>
    </div>

    <!-- Contact Dialog (Create/Edit) -->
    <ContactDialog
      :visible="showDialog"
      :contact="selectedContact"
      @save="saveContact"
      @close="closeDialog"
      @update:visible="showDialog = $event"
    />
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useAuth } from '@/composables/useAuth'
import { useContacts } from '@/composables/useContacts'
import AppHeader from '@/components/common/AppHeader.vue'
import ContactTable from '@/components/ContactTable.vue'
import ContactDialog from '@/components/ContactDialogue.vue'
import type { Contact, CreateContactDto } from '@/types/Contact'


const { logout } = useAuth()
const {
  contacts,
  loading: isLoading,
  error: hasError,
  errorMessage,
  selectedContact,
  fetchContacts,
  createContact,
  updateContact,
  deleteContact: deleteContactService,
  setSelectedContact,
  clearError
} = useContacts()

const showDialog = ref(false)

onMounted(() => {
  fetchContacts()
})

const openDialog = (contact?: Contact) => {
  clearError()
  if (contact) {
    setSelectedContact(contact)
  } else {
    setSelectedContact(null)
  }
  showDialog.value = true
}

const closeDialog = () => {
  showDialog.value = false
  setSelectedContact(null)
}

const saveContact = async (data: CreateContactDto) => {
  try {
    if (selectedContact.value) {
      await updateContact(selectedContact.value.id, data)
    } else {
      await createContact(data)
    }
    closeDialog()
  } catch (error) {
    console.error('Error saving contact:', error)
  }
}

const deleteContact = (contact: Contact) => {
  // Usar confirm nativo do navegador
  if (confirm(`Tem certeza que deseja deletar ${contact.nome}?`)) {
    deleteContactService(contact.id).catch(error => {
      console.error('Error deleting contact:', error)
    })
  }
}

const handleLogout = () => {
  logout()
}
</script>

<style scoped>
.contacts-view {
  width: 100%;
  min-height: 100vh;
  background-color: #f8fafc;
}

.contacts-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem 1rem;
}

.contacts-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  gap: 2rem;
  margin-bottom: 2rem;
}

.header-title h1 {
  font-size: 2rem;
  font-weight: 700;
  color: #1e293b;
  margin: 0;
  margin-bottom: 0.5rem;
}

.header-title p {
  color: #64748b;
  font-size: 0.95rem;
  margin: 0;
}

.btn-new {
  padding: 0.75rem 1.5rem;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  border: none;
  border-radius: 6px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;
  white-space: nowrap;
}

.btn-new:hover {
  background: linear-gradient(135deg, #5568d3 0%, #6a398f 100%);
  transform: translateY(-2px);
  box-shadow: 0 5px 20px rgba(102, 126, 234, 0.3);
}

.loading {
  text-align: center;
  padding: 2rem;
  color: #64748b;
}

.error-box {
  background-color: #fee2e2;
  border: 1px solid #fecaca;
  color: #991b1b;
  padding: 1rem;
  border-radius: 6px;
  margin-bottom: 1.5rem;
}

.empty-state {
  text-align: center;
  padding: 3rem 2rem;
  background: white;
  border-radius: 8px;
  border: 2px dashed #e2e8f0;
}

.empty-state p {
  margin: 0.5rem 0;
  color: #64748b;
  font-size: 1rem;
}

@media (max-width: 768px) {
  .contacts-header {
    flex-direction: column;
    align-items: stretch;
  }

  .btn-new {
    width: 100%;
  }

  .header-title h1 {
    font-size: 1.5rem;
  }
}
</style>
