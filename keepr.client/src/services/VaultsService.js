import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class VaultsService{
  async createVault(vault){
    const res = await api.post('api/vaults', vault)
    AppState.profileVaults = [...AppState.profileVaults, res.data]
  }
  async getKeepsByVaultId(id){
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.vaultKeeps = res.data
  }
  async getById(id){
    const res = await api.get(`api/vaults/${id}`)
    AppState.vault = res.data
  }

}

export const vaultsService = new VaultsService()