<template>

  <div class="col-12 banner px-0">
    <div class="row text-end ">
      <div class="col-1 offset-10 px-0">
        <Login class="px-0" />
      </div>
    </div>
    <!-- <div class="row d-flex justify-content-center align-items-center plusHeight">
      <div class="col-4 text-center">
        <h1>All-Spice</h1>
        <h5>Cherish Your family</h5>
        <h5>And Their Cooking</h5>
      </div>
    </div> -->
  </div>


</template>

<script>
import Login from "./Login.vue";


export default {
  setup() {
    return {};
  },
  components: { Login }
}
</script>

<style scoped>
.banner {
  height: 10vh;
  background-image: url(https://images.unsplash.com/photo-1575000977355-8b2a719926c9?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80);
  background-size: cover;
  object-fit: cover;
  margin-top: 15px;
}

.plusHeight {
  color: rgb(236, 232, 232);
  text-shadow: 2px 2px 2px black;
  height: 70%;
}
</style>