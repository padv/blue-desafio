import api from './api'
import type { Contact, CreateContactDto, UpdateContactDto } from '@/types/Contact'
import { AxiosError } from 'axios'

class ContactServiceError extends Error {
  constructor(
    public message: string,
    public statusCode?: number,
    public originalError?: AxiosError
  ) {
    super(message)
    this.name = 'ContactServiceError'
  }
}

const handleError = (error: unknown): never => {
  if (error instanceof ContactServiceError) {
    throw error
  }

  if (error instanceof AxiosError) {
    const statusCode = error.response?.status
    const message = error.response?.data?.message || error.message

    console.error(`[ContactService Error] Status: ${statusCode}, Message: ${message}`)
    
    throw new ContactServiceError(message, statusCode, error)
  }

  throw new ContactServiceError('Erro desconhecido ao comunicar com a API')
}

export const contactService = {
  async getAll(): Promise<Contact[]> {
    try {
      console.log('[ContactService] Buscando todos os contatos')
      const response = await api.get<Contact[]>('/contacts')
      console.log('[ContactService] Contatos obtidos com sucesso', response.data)
      return response.data
    } catch (error) {
      return handleError(error)
    }
  },

  async getById(id: string): Promise<Contact> {
    try {
      if (!id) {
        throw new ContactServiceError('ID do contato é obrigatório')
      }
      
      console.log(`[ContactService] Buscando contato com ID: ${id}`)
      const response = await api.get<Contact>(`/contacts/${id}`)
      console.log('[ContactService] Contato obtido com sucesso', response.data)
      return response.data
    } catch (error) {
      return handleError(error)
    }
  },

  async create(data: CreateContactDto): Promise<Contact> {
    try {
      validateCreateContactData(data)
      
      console.log('[ContactService] Criando novo contato', data)
      const response = await api.post<Contact>('/contacts', data)
      console.log('[ContactService] Contato criado com sucesso', response.data)
      return response.data
    } catch (error) {
      return handleError(error)
    }
  },

  async update(id: string, data: UpdateContactDto): Promise<Contact> {
    try {
      if (!id) {
        throw new ContactServiceError('ID do contato é obrigatório')
      }

      validateUpdateContactData(data)
      
      console.log(`[ContactService] Atualizando contato ID: ${id}`, data)
      const response = await api.put<Contact>(`/contacts/${id}`, data)
      console.log('[ContactService] Contato atualizado com sucesso', response.data)
      return response.data
    } catch (error) {
      return handleError(error)
    }
  },

  async delete(id: string): Promise<void> {
    try {
      if (!id) {
        throw new ContactServiceError('ID do contato é obrigatório')
      }
      
      console.log(`[ContactService] Deletando contato ID: ${id}`)
      await api.delete(`/contacts/${id}`)
      console.log('[ContactService] Contato deletado com sucesso')
    } catch (error) {
      return handleError(error)
    }
  }
}

// Função auxiliar para validações na criação
const validateCreateContactData = (data: CreateContactDto): void => {
  if (!data.nome || data.nome.trim() === '') {
    throw new ContactServiceError('Nome é obrigatório')
  }
  
  if (!data.email || data.email.trim() === '') {
    throw new ContactServiceError('Email é obrigatório')
  }
  
  if (!isValidEmail(data.email)) {
    throw new ContactServiceError('Email inválido')
  }
  
  if (!data.telefone || data.telefone.trim() === '') {
    throw new ContactServiceError('Telefone é obrigatório')
  }

  if (!isValidPhoneNumber(data.telefone)) {
    throw new ContactServiceError('Telefone deve estar no formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX')
  }
}

// Função auxiliar para validações na atualização (parcial)
const validateUpdateContactData = (data: UpdateContactDto): void => {
  if (data.nome !== undefined && data.nome !== null) {
    if (data.nome.trim() === '') {
      throw new ContactServiceError('Nome não pode ser vazio')
    }
  }

  if (data.email !== undefined && data.email !== null) {
    if (data.email.trim() === '') {
      throw new ContactServiceError('Email não pode ser vazio')
    }
    if (!isValidEmail(data.email)) {
      throw new ContactServiceError('Email inválido')
    }
  }

  if (data.telefone !== undefined && data.telefone !== null) {
    if (data.telefone.trim() === '') {
      throw new ContactServiceError('Telefone não pode ser vazio')
    }
    if (!isValidPhoneNumber(data.telefone)) {
      throw new ContactServiceError('Telefone deve estar no formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX')
    }
  }
}

const isValidEmail = (email: string): boolean => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  return emailRegex.test(email)
}

const isValidPhoneNumber = (phone: string): boolean => {
  const phoneRegex = /^\(\d{2}\)\s\d{4,5}-\d{4}$/
  return phoneRegex.test(phone)
}

export { ContactServiceError }
