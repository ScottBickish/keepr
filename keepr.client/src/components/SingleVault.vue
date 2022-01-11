<template>
  <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
    <div class="rounded bg-secondary border action">
      <h3>{{ vault.name }}</h3>
      <h5>{{ vault.description }}</h5>
    </div>
  </router-link>
</template>


<script>
export default {
  props: {
    vault: Object
  },
  setup(props) {
    return {}
  }
}
</script>


<style lang="scss" scoped>
</style>