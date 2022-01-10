<template>
  <div class="keep form">
    <form @submit.prevent="createKeep()">
      <input
        type="text"
        id="name"
        name="name"
        placeholder="Name..."
        class="form-contol my-2"
        required
        v-model="keep.name"
      />
      <input
        type="text"
        id="img"
        name="img"
        placeholder="Img Url..."
        class="form-contol my-2"
        required
        v-model="keep.img"
      />
      <textarea
        name="description"
        id="description"
        cols="30"
        rows="10"
        class="form-control my-2"
        required
        v-model="keep.description"
      ></textarea>
      <button class="btn btn-rounded" type="submit">Upload</button>
    </form>
  </div>
</template>


<script>
import { ref } from "@vue/reactivity"
import { keepsService } from "../services/KeepsService"
import Pop from "../utils/Pop"
import { logger } from "../utils/Logger"
import { Modal } from "bootstrap"
export default {
  setup() {
    const keep = ref({})
    return {
      keep,
      async createKeep() {
        try {
          await keepsService.createKeep(keep.value)
          Pop.toast("You have created a keep!", 'success')
          Modal.getOrCreateInstance(document.getElementById("keep-form-modal")).hide();
        } catch (error) {
          logger.error(error)
          Pop.toast(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>