<template>
  <div class="keeps component card">
    <img :src="keep.img" class="rounded-top img-fluid" alt="..." />
    <div class="d-flex">
      <div class="card-body">
        <p class="card-text">{{ keep.name }}</p>
      </div>
      <div><img :src="keep.creator.picture" class="pic" /></div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
export default {
  props: {
    keep: Object
  },
  setup(props) {
    return {
      props,

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