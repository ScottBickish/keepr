import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { profilesService } from "./ProfilesService"


class VaultsService{
  async createVault(vault){
    const res = await api.post('api/vaults', vault)
    AppState.profileVaults = [...AppState.profileVaults, res.data]
    // await profilesService. getVaultsByProfile(vault.creatorId)
  }
  async getKeepsByVaultId(id){
    const res = await api.get(`api/vaults/${id}/keeps`)
    AppState.vaultKeeps = res.data
  }
  async getById(id){
    const res = await api.get(`api/vaults/${id}`)
    AppState.vault = res.data
  }
  async removeVault(id){
    await api.delete(`api/vaults/${id}`)
  }
  async getMyVaults(){
    const res = await api.get(`account/vaults`)
    AppState.myVaults = res.data
  }

}

export const vaultsService = new VaultsService()