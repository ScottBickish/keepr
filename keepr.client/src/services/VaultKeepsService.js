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
    await api.delete(`api/vaultkeeps/${id}`)
    AppState.vaultKeeps = AppState.vaultKeeps.filter(v => v.id !== id)
    // await vaultsService.getKeepsByVaultId()
  }
}

export const vaultKeepsService = new VaultKeepsService()