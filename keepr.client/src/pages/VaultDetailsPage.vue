<template>
  <div class="component">
    <div class="d-flex">
      <div class="ms-5">
        <h1>{{ vault.name }}</h1>
        <h3 class="my-2">Keeps: {{ vaultKeeps.length }}</h3>
      </div>
      <div class="my-2">
        <router-link
          :to="{ name: 'Profile', params: { id: vault.creator?.id } }"
        >
          <button @click="removeVault()">Delete Vault</button>
        </router-link>
      </div>
    </div>
    <div class="row m-2 container-fluid">
      <div class="col-md-3" v-for="keep in vaultKeeps" :key="keep.id">
        <SingleKeep :keep="keep" />
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted } from "@vue/runtime-core"
import { useRoute, useRouter } from "vue-router"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { vaultsService } from "../services/VaultsService"
import { AppState } from "../AppState"
import { vaultKeepsService } from "../services/VaultKeepsService"
export default {
  setup() {
    const router = useRouter()
    const route = useRoute()
    onMounted(async () => {
      try {
        if (route.params.id) {
          await vaultsService.getById(route.params.id)
          await vaultsService.getKeepsByVaultId(route.params.id)

        }
      } catch (error) {
        logger.error(error)
        Pop.toast(error)
      }
    })
    return {
      async removeVault() {
        try {
          // router.push({ name: 'Profile', params: { id: vault.creatorId } })
          await vaultKeepsService.removeVault(route.params.id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      vaultKeeps: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.vault),
    }
  }
}
</script>


<style lang="scss" scoped>
</style>