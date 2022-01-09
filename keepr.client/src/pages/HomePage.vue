<template>
  <div class="row container-fluid">
    <div class="col-md-3" v-for="keep in keeps" :key="keep.id">
      <SingleKeep :keep="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from "@vue/runtime-core"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { keepsService } from "../services/KeepsService"
import { AppState } from "../AppState"
export default {
  setup() {
    onMounted(async () => {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        logger.error(error)
        Pop.toast(error)
      }
    })
    return {
      name: 'Home',
      keeps: computed(() => AppState.keeps)

    }
  }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;
  .home-card {
    width: 50vw;
    > img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
