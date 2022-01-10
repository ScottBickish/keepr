<template>
  <div class="keeps component card mt-1">
    <img
      data-bs-toggle="modal"
      data-bs-target="#keep-modal"
      :src="keep.img"
      class="rounded-top img-fluid action"
      alt="..."
      @click="setActive(keep.id)"
    />
    <div class="d-flex">
      <div class="card-body">
        <p class="card-text">{{ keep.name }}</p>
      </div>
      <div>
        <router-link :to="{ name: 'Profile', params: { id: keep.creator.id } }">
          <img :src="keep.creator.picture" class="pic action m-1" />
        </router-link>
      </div>
    </div>
  </div>
  <Modal id="keep-modal">
    <template #modal-title>Keep Info</template>
    <template #modal-body> <ActiveKeepModal /> </template>
  </Modal>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRouter } from "vue-router"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
export default {
  props: {
    keep: Object
  },
  setup(props) {

    return {
      props,
      async setActive(id) {
        try {
          await keepsService.getKeepById(id)
        } catch (error) {
          logger.log(error)
          Pop.toast(error)
        }
      }

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
</style>