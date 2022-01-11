<template>
  <div class="Vault form">
    <form @submit.prevent="createVault()">
      <input
        type="text"
        id="name"
        name="name"
        placeholder="Name..."
        class="form-control my-2"
        required
        v-model="vault.name"
      />
      <span class="me-3">is this Private?</span>
      <input
        type="checkbox"
        name="isPrivte"
        id="isPrivate"
        class="my-2 action"
        v-model="vault.isPrivate"
      />
      <textarea
        name="description"
        id="description"
        cols="30"
        rows="10"
        class="form-control my-2"
        placeholder="Description..."
        required
        v-model="vault.description"
      ></textarea>
      <button class="rounded" type="submit" title="Upload">Upload</button>
    </form>
  </div>
</template>


<script>
import { ref } from "@vue/reactivity"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { vaultsService } from "../services/VaultsService"
import { Modal } from "bootstrap"
export default {
  setup() {
    const vault = ref({})
    return {
      vault,
      async createVault() {
        try {
          await vaultsService.createVault(vault.value)
          Pop.toast("You have created a Vault!", 'success')
          Modal.getOrCreateInstance(document.getElementById("vault-form-modal")).hide();
        } catch (error) {
          logger.error(error)
          Pop.toast(error``)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>