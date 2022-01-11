<template>
  <!-- <router-link :to="{ name: 'Vault', params: { id: vault.id } }"> -->
  <div class="rounded bg-secondary border action p-2" @click="pushHome()">
    <h3>{{ vault.name }}</h3>
    <h5>{{ vault.description }}</h5>
  </div>
  <!-- </router-link> -->
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRouter } from "vue-router"
export default {
  props: {
    vault: Object
  },
  setup(props) {
    const router = useRouter()
    return {
      account: computed(() => AppState.account),
      pushHome() {
        if (AppState.account.id !== props.vault.creatorId && props.vault.isPrivate) {

          router.push({ name: 'Home' })

        }
        else
          router.push({ name: 'Vault', params: { id: props.vault.id } })


      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>