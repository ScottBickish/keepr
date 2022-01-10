import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultKeepsService{
  async createVK(VK){
    const res = await api.post('api/vaultkeeps', VK)
    logger.log(res.data)
    AppState.vaultKeeps = [...AppState.vaultKeeps, res.data]
  }
  async removeVault(id){
    await api.delete(`api/VaultKeeps/${id}`)
    
  }

}

export const vaultKeepsService = new VaultKeepsService()