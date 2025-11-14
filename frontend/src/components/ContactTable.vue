<template>
  <div class="table-container">
    <table class="contacts-table">
      <thead>
        <tr>
          <th>Nome</th>
          <th>Email</th>
          <th>Telefone</th>
          <th>Ações</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="contact in contacts" :key="contact.id">
          <td>{{ contact.nome }}</td>
          <td>
            <a :href="`mailto:${contact.email}`">{{ contact.email }}</a>
          </td>
          <td>{{ contact.telefone }}</td>
          <td class="actions">
            <button @click="$emit('edit', contact)" class="btn-edit">Editar</button>
            <button @click="$emit('delete', contact)" class="btn-delete">Deletar</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup lang="ts">
import type { Contact } from '@/types/Contact'

defineProps<{
  contacts: Contact[]
}>()

defineEmits<{
  edit: [contact: Contact]
  delete: [contact: Contact]
}>()
</script>

<style scoped>
.table-container {
  background: white;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
}

.contacts-table {
  width: 100%;
  border-collapse: collapse;
}

.contacts-table thead {
  background-color: #f1f5f9;
  border-bottom: 2px solid #e2e8f0;
}

.contacts-table thead th {
  padding: 1rem;
  text-align: left;
  font-weight: 600;
  color: #1e293b;
  font-size: 0.95rem;
}

.contacts-table tbody tr {
  border-bottom: 1px solid #e2e8f0;
  transition: background-color 0.2s ease;
}

.contacts-table tbody tr:hover {
  background-color: #f8fafc;
}

.contacts-table td {
  padding: 1rem;
  color: #475569;
}

.contacts-table a {
  color: #667eea;
  text-decoration: none;
}

.contacts-table a:hover {
  text-decoration: underline;
}

.actions {
  display: flex;
  gap: 0.5rem;
}

.btn-edit,
.btn-delete {
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 6px;
  font-size: 0.85rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;
}

.btn-edit {
  background-color: #dbeafe;
  color: #0369a1;
}

.btn-edit:hover {
  background-color: #bae6fd;
}

.btn-delete {
  background-color: #fee2e2;
  color: #991b1b;
}

.btn-delete:hover {
  background-color: #fecaca;
}
</style>
