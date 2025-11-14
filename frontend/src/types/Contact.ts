
export interface Contact {
  id: string
  nome: string
  email: string
  telefone: string
}


export type CreateContactDto = Omit<Contact, 'id'>
export type UpdateContactDto = Omit<Partial<Contact>, 'id'>