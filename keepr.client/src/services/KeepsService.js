import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{
async getAllKeeps(){
  const res = await api.get('api/keeps')
  AppState.keeps = res.data
}
async getKeepById(id){
  const res = await api.get(`api/keeps/${id}`)
  AppState.activeKeep = res.data
}
async removeKeep(id){
  await api.delete(`api/keeps/${id}`)
  AppState.keeps = AppState.keeps.filter(k => k.id !== id)
  AppState.profileKeeps = AppState.profileKeeps.filter(p => p.id !== id)
}
async createKeep(keep){
  const res = await api.post('api/keeps', keep)
  AppState.profileKeeps = [...AppState.profileKeeps, res.data]
}

}
export const keepsService = new KeepsService()