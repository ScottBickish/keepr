import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class ProfileService{
async getKeepsByProfile(id){
  const res = await api.get(`api/profiles/${id}/keeps`)
  logger.log(res.data)
  AppState.profileKeeps = res.data
}
async getProfile(id){
  const res = await api.get(`api/profiles/${id}`)
  logger.log(res.data)
  AppState.profile = res.data
}
async getVaultsByProfile(id){
  const res = await api.get(`api/profiles/${id}/vaults`)
  logger.log(res.data)
  AppState.profileVaults = res.data
}
}

export const profilesService = new ProfileService()