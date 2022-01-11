<template>
  <div class="d-flex">
    <div class="keeps component card mt-1">
      <img :src="activeKeep.img" class="rounded-top img-fluid mpic" alt="..." />
    </div>
    <div class="text-center ms-5">
      <span class="">
        <b>
          Views: {{ activeKeep.views }} Keeps:
          {{ activeKeep.keeps }}
        </b></span
      >
      <h3 class="">{{ activeKeep.name }}</h3>
      <p class="">
        <b>{{ activeKeep.description }}</b>
      </p>
      <hr />
      <div class="">
        <div class="fdiv">
          <form @submit.prevent="createVK" v-if="account.id">
            <div class="dropdown input-group">
              <button
                class="btn btn-secondary dropdown-toggle form-control"
                type="button"
                id="dropdownMenuButton1"
                data-bs-toggle="dropdown"
                aria-expanded="false"
                title="choose a vault"
              >
                {{ newVault }}
              </button>
              <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
                <li v-for="vault in vaults" :key="vault.id">
                  <div
                    class="dropdown-item selectable"
                    @click="newVault = vault.name"
                  >
                    {{ vault.name }}
                  </div>
                </li>
              </ul>
              <button
                class="btn bg-white border border-secondary text-secondary px-2"
                type="submit"
                title="submit"
              >
                <i class="mdi mdi-plus-thick"></i>
              </button>
            </div>
          </form>
        </div>
        <div class="d-flex mt-3">
          <div>
            <span
              v-if="account.id === activeKeep.creatorId"
              class="mdi mdi-trash-can text-danger ms-5 fs-3 action"
              title="delete keep"
              @click="removeKeep(activeKeep)"
            ></span>
          </div>
          <div class="d-flex">
            <div>
              <img
                :src="activeKeep.creator?.picture"
                alt=""
                class="pic ms-5"
                :title="activeKeep.creator?.name"
              />
            </div>
            <div class="ms-2 mb-0">
              <span
                ><p>
                  <b>{{ activeKeep.creator?.name }}</b>
                </p></span
              >
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "@vue/reactivity"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { vaultKeepsService } from "../services/VaultKeepsService"
import { Modal } from "bootstrap"
export default {
  props: {
    keep: Object
  },
  setup(props) {
    const newVault = ref("choose a vault!")
    return {
      activeKeep: computed(() => AppState.activeKeep),
      async removeKeep(activeKeep) {
        try {
          if (window.location.href.indexOf("Vault") > -1) {
            if (await Pop.confirm("Are you sure you want to delete this?")) {
              Modal.getOrCreateInstance(document.getElementById("keep-modal")).hide();
              await vaultKeepsService.removeVK(props.keep.vaultKeepId)
            }
          } else
            if (await Pop.confirm("Are you sure you want to delete this?")) {
              Modal.getOrCreateInstance(document.getElementById("keep-modal")).hide();
              await keepsService.removeKeep(activeKeep.id)
            }
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      async createVK() {
        try {
          const vault = newVault.value
          const found = AppState.myVaults.find(v => v.name === vault)
          let object = { vaultId: found.id, keepId: AppState.activeKeep.id }

          await vaultKeepsService.createVK(object)
          Modal.getOrCreateInstance(document.getElementById("keep-modal")).hide();
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      },
      newVault,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.myVaults)
    }
  }
}
</script>


<style lang="scss" scoped>
.pic {
  height: 50px;
  width: 50px;
  object-fit: cover;
  border-radius: 50%;
}
.mpic {
  height: 490px;
  width: 400px;
  object-fit: cover;
}
</style>