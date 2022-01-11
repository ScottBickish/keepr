<template>
  <div class="row container-fluid d-flex m-3">
    <div class="col-md-3">
      <img :src="profile.picture" alt="" class="pic" />
    </div>
    <div class="col-md-6">
      <h1>
        {{ profile.name }}
      </h1>
      <p>
        <b> Vaults: {{ profileVaults.length }}</b>
      </p>
      <p>
        <b> Keeps: {{ profileKeeps.length }} </b>
      </p>
    </div>
  </div>
  <div class="row container-fluid">
    <div class="col-12 d-flex m-3">
      <h2>Vaults</h2>
      <button
        data-bs-toggle="modal"
        data-bs-target="#vault-form-modal"
        class="mdi mdi-plus ms-3 color rounded"
        v-if="account.id === profile.id"
        title="create a vault"
      ></button>
    </div>
  </div>
  <!-- <div class="row m-2 container-fluid" v-if="account.id === myVaults.creatorId">
    <div class="col-md-3" v-for="vault in myVaults" :key="vault.id">
      <SingleVault :vault="vault" />
    </div>
  </div> -->
  <div class="row m-2 container-fluid">
    <div class="col-md-3" v-for="vault in profileVaults" :key="vault.id">
      <SingleVault :vault="vault" />
    </div>
  </div>

  <div class="row container-fluid">
    <div class="col-12 d-flex m-3">
      <h2>Keeps</h2>
      <button
        data-bs-toggle="modal"
        data-bs-target="#keep-form-modal"
        class="mdi mdi-plus ms-3 color rounded"
        v-if="account.id === profile.id"
        title="create a keep"
      ></button>
    </div>
  </div>
  <div class="row m-2 container-fluid">
    <div class="col-md-3" v-for="keep in profileKeeps" :key="keep.id">
      <SingleKeep :keep="keep" />
    </div>
  </div>
  <Modal id="keep-form-modal">
    <template #modal-title>Keep Info</template>
    <template #modal-body> <KeepFormModal /> </template>
  </Modal>
  <Modal id="vault-form-modal">
    <template #modal-title>Keep Info</template>
    <template #modal-body> <VaultForm /> </template>
  </Modal>

  <!-- <div class="profile" v-for="keep in profileKeeps" :key="keep.id"></div> -->
</template>


<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { profilesService } from "../services/ProfilesService"
import { useRoute } from "vue-router"
import { AppState } from "../AppState"
export default {
  name: "Profile",
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        if (route.params.id) {
          await profilesService.getProfile(route.params.id)
          await profilesService.getKeepsByProfile(route.params.id)
          await profilesService.getVaultsByProfile(route.params.id)

        }
      } catch (error) {
        logger.error(error)
        Pop.toast(error)
      }
    })
    return {
      route,
      profileKeeps: computed(() => AppState.profileKeeps),
      profileVaults: computed(() => AppState.profileVaults),
      profile: computed(() => AppState.profile),
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),


    }
  }
}
</script>


<style lang="scss" scoped>
.pic {
  height: 150px;
  width: 150px;
  object-fit: cover;
  border-radius: 10px;
}
.color {
  background-color: lightblue;
}
</style>