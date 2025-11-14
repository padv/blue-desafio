
import { describe, it, expect, vi, beforeEach } from 'vitest'
import { contactService, ContactServiceError } from '../contactService'
import api from '../api'

vi.mock('../api')

describe('ContactService', () => {
  beforeEach(() => {
    vi.clearAllMocks()
  })

  describe('getAll', () => {
    it('deve buscar todos os contatos com sucesso', async () => {
      const mockContacts = [
        { id: '1', nome: 'João', email: 'joao@email.com', telefone: '(11) 99999-9999' },
        { id: '2', nome: 'Maria', email: 'maria@email.com', telefone: '(11) 9888-8888' }
      ]
      
      vi.mocked(api.get).mockResolvedValueOnce({ data: mockContacts })
      
      const result = await contactService.getAll()
      
      expect(result).toEqual(mockContacts)
      expect(api.get).toHaveBeenCalledWith('/contacts')
    })
  })

  describe('getById', () => {
    it('deve buscar um contato por ID', async () => {
      const mockContact = { 
        id: '1', 
        nome: 'João', 
        email: 'joao@email.com', 
        telefone: '(11) 99999-9999' 
      }
      
      vi.mocked(api.get).mockResolvedValueOnce({ data: mockContact })
      
      const result = await contactService.getById('1')
      
      expect(result).toEqual(mockContact)
      expect(api.get).toHaveBeenCalledWith('/contacts/1')
    })

    it('deve lançar erro se ID não for fornecido', async () => {
      await expect(contactService.getById('')).rejects.toThrow(ContactServiceError)
    })
  })

  describe('create', () => {
    it('deve criar um contato com sucesso com telefone de 9 dígitos', async () => {
      const newContact = { 
        nome: 'João', 
        email: 'joao@email.com', 
        telefone: '(11) 99999-9999' 
      }
      const createdContact = { id: '1', ...newContact }
      
      vi.mocked(api.post).mockResolvedValueOnce({ data: createdContact })
      
      const result = await contactService.create(newContact)
      
      expect(result).toEqual(createdContact)
      expect(api.post).toHaveBeenCalledWith('/contacts', newContact)
    })

    it('deve criar um contato com sucesso com telefone de 8 dígitos', async () => {
      const newContact = { 
        nome: 'Maria', 
        email: 'maria@email.com', 
        telefone: '(11) 9888-8888' 
      }
      const createdContact = { id: '2', ...newContact }
      
      vi.mocked(api.post).mockResolvedValueOnce({ data: createdContact })
      
      const result = await contactService.create(newContact)
      
      expect(result).toEqual(createdContact)
      expect(api.post).toHaveBeenCalledWith('/contacts', newContact)
    })

    it('deve lançar erro se nome estiver vazio', async () => {
      const invalidContact = { 
        nome: '', 
        email: 'joao@email.com', 
        telefone: '(11) 99999-9999' 
      }
      
      await expect(contactService.create(invalidContact)).rejects.toThrow(ContactServiceError)
      await expect(contactService.create(invalidContact)).rejects.toThrow('Nome é obrigatório')
    })

    it('deve lançar erro se email estiver vazio', async () => {
      const invalidContact = { 
        nome: 'João', 
        email: '', 
        telefone: '(11) 99999-9999' 
      }
      
      await expect(contactService.create(invalidContact)).rejects.toThrow(ContactServiceError)
      await expect(contactService.create(invalidContact)).rejects.toThrow('Email é obrigatório')
    })

    it('deve lançar erro se email for inválido', async () => {
      const invalidContact = { 
        nome: 'João', 
        email: 'email-invalido', 
        telefone: '(11) 99999-9999' 
      }
      
      await expect(contactService.create(invalidContact)).rejects.toThrow(ContactServiceError)
      await expect(contactService.create(invalidContact)).rejects.toThrow('Email inválido')
    })

    it('deve lançar erro se telefone estiver vazio', async () => {
      const invalidContact = { 
        nome: 'João', 
        email: 'joao@email.com', 
        telefone: '' 
      }
      
      await expect(contactService.create(invalidContact)).rejects.toThrow(ContactServiceError)
      await expect(contactService.create(invalidContact)).rejects.toThrow('Telefone é obrigatório')
    })

    it('deve lançar erro se telefone estiver em formato inválido', async () => {
      const invalidPhoneFormats = [
        '11999999999',
        '(11) 999999999',
        '11 9999-9999',
        '(11)99999-9999',
        '(11) 9999-999',
        '(1) 99999-9999',
      ]

      for (const phone of invalidPhoneFormats) {
        const invalidContact = { 
          nome: 'João', 
          email: 'joao@email.com', 
          telefone: phone 
        }
        
        await expect(contactService.create(invalidContact)).rejects.toThrow(ContactServiceError)
        await expect(contactService.create(invalidContact)).rejects.toThrow('Telefone deve estar no formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX')
      }
    })
  })

  describe('update', () => {
    it('deve atualizar todos os campos com sucesso', async () => {
      const updatedData = { 
        nome: 'João Silva', 
        email: 'joao.silva@email.com', 
        telefone: '(11) 99999-9999' 
      }
      const result = { id: '1', ...updatedData }
      
      vi.mocked(api.put).mockResolvedValueOnce({ data: result })
      
      const response = await contactService.update('1', updatedData)
      
      expect(response).toEqual(result)
      expect(api.put).toHaveBeenCalledWith('/contacts/1', updatedData)
    })

    it('deve atualizar apenas o nome', async () => {
      const updateData = { nome: 'João Silva' }
      const result = { 
        id: '1', 
        nome: 'João Silva', 
        email: 'joao@email.com', 
        telefone: '(11) 99999-9999' 
      }
      
      vi.mocked(api.put).mockResolvedValueOnce({ data: result })
      
      const response = await contactService.update('1', updateData)
      
      expect(response).toEqual(result)
      expect(api.put).toHaveBeenCalledWith('/contacts/1', updateData)
    })

    it('deve atualizar apenas o telefone com 9 dígitos', async () => {
      const updateData = { telefone: '(21) 98765-4321' }
      const result = { 
        id: '1', 
        nome: 'João', 
        email: 'joao@email.com', 
        telefone: '(21) 98765-4321' 
      }
      
      vi.mocked(api.put).mockResolvedValueOnce({ data: result })
      
      const response = await contactService.update('1', updateData)
      
      expect(response).toEqual(result)
    })

    it('deve atualizar apenas o telefone com 8 dígitos', async () => {
      const updateData = { telefone: '(21) 3333-4444' }
      const result = { 
        id: '1', 
        nome: 'João', 
        email: 'joao@email.com', 
        telefone: '(21) 3333-4444' 
      }
      
      vi.mocked(api.put).mockResolvedValueOnce({ data: result })
      
      const response = await contactService.update('1', updateData)
      
      expect(response).toEqual(result)
    })

    it('deve lançar erro se ID não for fornecido', async () => {
      const updatedData = { nome: 'João' }
      
      await expect(contactService.update('', updatedData)).rejects.toThrow(ContactServiceError)
      await expect(contactService.update('', updatedData)).rejects.toThrow('ID do contato é obrigatório')
    })

    it('deve lançar erro se nome fornecido estiver vazio', async () => {
      const updateData = { nome: '' }
      
      await expect(contactService.update('1', updateData)).rejects.toThrow(ContactServiceError)
      await expect(contactService.update('1', updateData)).rejects.toThrow('Nome não pode ser vazio')
    })

    it('deve lançar erro se email fornecido for inválido', async () => {
      const updateData = { email: 'invalido' }
      
      await expect(contactService.update('1', updateData)).rejects.toThrow(ContactServiceError)
      await expect(contactService.update('1', updateData)).rejects.toThrow('Email inválido')
    })

    it('deve lançar erro se telefone fornecido estiver vazio', async () => {
      const updateData = { telefone: '' }
      
      await expect(contactService.update('1', updateData)).rejects.toThrow(ContactServiceError)
      await expect(contactService.update('1', updateData)).rejects.toThrow('Telefone não pode ser vazio')
    })

    it('deve lançar erro se telefone fornecido estiver em formato inválido', async () => {
      const invalidPhoneFormats = [
        '11999999999',
        '(11) 999999999',
        '11 9999-9999',
        '(11)99999-9999',
      ]

      for (const phone of invalidPhoneFormats) {
        const updateData = { telefone: phone }
        
        await expect(contactService.update('1', updateData)).rejects.toThrow(ContactServiceError)
        await expect(contactService.update('1', updateData)).rejects.toThrow('Telefone deve estar no formato (XX) XXXXX-XXXX ou (XX) XXXX-XXXX')
      }
    })

    it('deve aceitar update vazio (sem campos)', async () => {
      const result = { 
        id: '1', 
        nome: 'João', 
        email: 'joao@email.com', 
        telefone: '(11) 99999-9999' 
      }
      
      vi.mocked(api.put).mockResolvedValueOnce({ data: result })
      
      const response = await contactService.update('1', {})
      
      expect(response).toBeDefined()
      expect(api.put).toHaveBeenCalledWith('/contacts/1', {})
    })
  })

  describe('delete', () => {
    it('deve deletar um contato com sucesso', async () => {
      vi.mocked(api.delete).mockResolvedValueOnce({ status: 204 })
      
      await contactService.delete('1')
      
      expect(api.delete).toHaveBeenCalledWith('/contacts/1')
    })

    it('deve lançar erro se ID não for fornecido', async () => {
      await expect(contactService.delete('')).rejects.toThrow(ContactServiceError)
      await expect(contactService.delete('')).rejects.toThrow('ID do contato é obrigatório')
    })
  })
})
