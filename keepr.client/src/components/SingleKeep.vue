<template>
  <div
    class="keeps component card mt-1 img-fluid elevation 10"
    :style="{ 'background-image': `url(${keep.img})` }"
  >
    <!-- <img
      data-bs-toggle="modal"
      data-bs-target="#keep-modal"
      :src="keep.img"
      class="rounded-top img-fluid action"
      alt="..."
      @click="setActive(keep.id)"
    /> -->
    <div class="d-flex h-100 w-100 align-items-end">
      <!-- <div></div> -->
      <!-- <div> -->
      <!-- <div class="d-flex"> -->
      <div class="card-body">
        <p
          class="card-text text-light action"
          @click="setActive(keep.id)"
          data-bs-toggle="modal"
          data-bs-target="#keep-modal"
          title="open keep modal"
        >
          {{ keep.name }}
        </p>
      </div>
      <div>
        <router-link :to="{ name: 'Profile', params: { id: keep.creator.id } }">
          <img
            :src="keep.creator.picture"
            class="pic action m-1"
            title="go to profile page"
          />
        </router-link>
      </div>
    </div>
  </div>
  <!-- </div> -->
  <!-- </div> -->
  <Modal id="keep-modal">
    <template #modal-title>Keep Info</template>
    <template #modal-body> <ActiveKeepModal :keep="keep" /> </template>
  </Modal>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRouter } from "vue-router"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { vaultKeepsService } from "../services/VaultKeepsService"
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
.keeps {
  background-size: cover;
  object-fit: cover;
  height: 200px;
  width: 200px;
}
</style>