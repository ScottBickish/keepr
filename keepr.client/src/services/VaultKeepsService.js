import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { vaultsService } from "./VaultsService"


class VaultKeepsService{
  async createVK(VK){
    const res = await api.post('api/vaultkeeps', VK)
    AppState.vaultKeeps = [...AppState.vaultKeeps, res.data]
  }
  async removeVK(id){
    // debugger
    await api.delete(`api/VaultKeeps/${id}`)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(v => v.vaultKeepId !== id)
  }
}

export const vaultKeepsService = new VaultKeepsService()