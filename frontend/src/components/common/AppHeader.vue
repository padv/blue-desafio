<template>
  <div class="app-header">
    <div class="header-content">
      <div class="header-left">
        <h2 class="app-name">
          <i class="pi pi-phone"></i>
          Agenda
        </h2>
      </div>

      <div class="header-right">
        <span class="user-info">
          <i class="pi pi-user"></i>
          {{ userEmail }}
        </span>
        <Button
          icon="pi pi-sign-out"
          class="p-button-rounded p-button-text"
          severity="danger"
          @click="handleLogout"
          title="Logout"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import Button from 'primevue/button'

const emit = defineEmits<{
  logout: []
}>()

const userEmail = computed(() => {
  return localStorage.getItem('user') || 'Usuário'
})

const handleLogout = () => {
  emit('logout')
}
</script>

<style scoped>
.app-header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 1rem 2rem;
  box-shadow: 0 2px 12px rgba(102, 126, 234, 0.15);
  position: sticky;
  top: 0;
  z-index: 100;
}

.header-content {
  max-width: 1200px;
  margin: 0 auto;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.header-left {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.app-name {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  font-size: 1.5rem;
  font-weight: 700;
  margin: 0;
}

.app-name i {
  font-size: 1.75rem;
}

.header-right {
  display: flex;
  align-items: center;
  gap: 1.5rem;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  font-weight: 500;
  white-space: nowrap;
  font-size: 0.95rem;
}

.user-info i {
  font-size: 1rem;
}

:deep(.p-button-rounded) {
  background-color: rgba(255, 255, 255, 0.2) !important;
}

:deep(.p-button-rounded:hover) {
  background-color: rgba(255, 255, 255, 0.3) !important;
}

@media (max-width: 768px) {
  .app-header {
    padding: 1rem;
  }

  .header-content {
    gap: 1rem;
  }

  .app-name {
    font-size: 1.25rem;
  }

  .user-info {
    display: none;
  }
}
</style>
