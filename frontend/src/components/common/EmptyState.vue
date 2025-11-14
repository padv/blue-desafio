<template>
  <div class="empty-state">
    <div class="empty-state-icon">
      <i :class="icon"></i>
    </div>
    <h3 class="empty-state-title">{{ title }}</h3>
    <p class="empty-state-description">{{ description }}</p>
  </div>
</template>

<script setup lang="ts">
interface Props {
  title: string
  description: string
  icon?: string
}

withDefaults(defineProps<Props>(), {
  icon: 'pi pi-inbox'
})
</script>

<style scoped>
.empty-state {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 4rem 2rem;
  text-align: center;
  background: white;
  border-radius: 12px;
  border: 2px dashed #e2e8f0;
}

.empty-state-icon {
  font-size: 4rem;
  color: #cbd5e1;
  margin-bottom: 1rem;
}

.empty-state-title {
  font-size: 1.5rem;
  font-weight: 600;
  color: #475569;
  margin-bottom: 0.5rem;
}

.empty-state-description {
  color: #94a3b8;
  font-size: 0.95rem;
  margin: 0;
}
</style>
