import { ref, computed } from 'vue'
import { contactService, ContactServiceError } from '@/services/contactService'
import type { Contact, CreateContactDto, UpdateContactDto } from '@/types/Contact'
export const useContacts = () => {
  const contacts = ref<Contact[]>([])
  const loading = ref(false)
  const error = ref<string | null>(null)
  const selectedContact = ref<Contact | null>(null)

  const isLoading = computed(() => loading.value)
  const hasError = computed(() => error.value !== null)

  const clearError = () => {
    error.value = null
  }

  const fetchContacts = async () => {
    loading.value = true
    error.value = null
    try {
      contacts.value = await contactService.getAll()
    } catch (err) {
      if (err instanceof ContactServiceError) {
        error.value = err.message
      } else {
        error.value = 'Erro ao buscar contatos'
      }
    } finally {
      loading.value = false
    }
  }

  const createContact = async (data: CreateContactDto) => {
    loading.value = true
    error.value = null
    try {
      const newContact = await contactService.create(data)
      contacts.value.push(newContact)
      return newContact
    } catch (err) {
      if (err instanceof ContactServiceError) {
        error.value = err.message
      } else {
        error.value = 'Erro ao criar contato'
      }
      throw err
    } finally {
      loading.value = false
    }
  }

  const updateContact = async (id: string, data: UpdateContactDto) => {
    loading.value = true
    error.value = null
    try {
      const updated = await contactService.update(id, data)
      const index = contacts.value.findIndex(c => c.id === id)
      if (index !== -1) {
        contacts.value[index] = updated
      }
      return updated
    } catch (err) {
      if (err instanceof ContactServiceError) {
        error.value = err.message
      } else {
        error.value = 'Erro ao atualizar contato'
      }
      throw err
    } finally {
      loading.value = false
    }
  }

  const deleteContact = async (id: string) => {
    loading.value = true
    error.value = null
    try {
      await contactService.delete(id)
      contacts.value = contacts.value.filter(c => c.id !== id)
    } catch (err) {
      if (err instanceof ContactServiceError) {
        error.value = err.message
      } else {
        error.value = 'Erro ao deletar contato'
      }
      throw err
    } finally {
      loading.value = false
    }
  }

  const setSelectedContact = (contact: Contact | null) => {
    selectedContact.value = contact
  }

  return {
    contacts,
    loading: isLoading,
    error: hasError,
    errorMessage: computed(() => error.value),
    selectedContact,
    clearError,
    fetchContacts,
    createContact,
    updateContact,
    deleteContact,
    setSelectedContact
  }
}
