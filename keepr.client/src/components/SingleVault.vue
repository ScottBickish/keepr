<template>
  <router-link :to="{ name: 'Vault', params: { id: vault.id } }">
    <div class="rounded bg-secondary border action">
      <h3>{{ vault.name }}</h3>
      <h5>{{ vault.description }}</h5>
    </div>
  </router-link>
  <!-- <div class="vault component card mt-1">
    <img :src="keep.img" class="rounded-top img-fluid" alt="..." />
    <div class="d-flex">
      <div class="card-body">
        <p class="card-text">{{ keep.name }}</p>
      </div>
      <div>
        <router-link :to="{ name: 'Profile', params: { id: keep.creator.id } }">
          <img :src="keep.creator.picture" class="pic action" />
        </router-link>
      </div>
    </div>
  </div> -->
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