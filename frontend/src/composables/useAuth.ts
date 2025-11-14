import { ref } from 'vue'
import { useRouter } from 'vue-router'

export const useAuth = () => {
  const router = useRouter()
  const loading = ref(false)
  const error = ref<string | null>(null)

  const login = async (email: string, password: string): Promise<void> => {
    loading.value = true
    error.value = null

    try {
      // Validar
      if (!email || !password) {
        error.value = 'Email e senha são obrigatórios'
        return
      }

      if (password.length < 6) {
        error.value = 'Senha deve ter no mínimo 6 caracteres'
        return
      }

      // Simular delay de API
      await new Promise(resolve => setTimeout(resolve, 800))

      // Gerar token
      const token = `token-${Date.now()}`
      localStorage.setItem('token', token)
      localStorage.setItem('user', email)

      console.log('✅ Login bem-sucedido!')

      // Redirecionar
      await router.push('/contacts')
    } catch (err) {
      error.value = 'Erro ao fazer login'
      console.error(err)
    } finally {
      loading.value = false
    }
  }

  const logout = () => {
    localStorage.removeItem('token')
    localStorage.removeItem('user')
    error.value = null
    router.push('/login')
  }

  return {
    login,
    logout,
    loading,
    error
  }
}
