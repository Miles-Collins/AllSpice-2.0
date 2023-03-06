<template>
  <div v-if="recipe" class="container-fluid">
    <div class="row">
      <div class="offset-1 col-1">
        <div class="row mainBody">
          <button class="btn btn-outline">SOCIALS</button>
          <button class="btn btn-outline">PRINT</button>
        </div>
      </div>
      <!-- SECTION SORTED BY SECTION -->
      <div class="col-8">
        <div class="row mainBody">
          <div class="col-2 d-flex align-items-center my-1">
            <h5 class="d-flex m-0">SORTED BY:</h5>
          </div>
          <div class="col-2 mx-1 sortedBy d-flex justify-content-center align-items-center">{{ recipe?.category }}</div>
          <div v-if="recipe?.ingredients"
            class="col-2 mx-1 btn-pill sortedBy d-flex justify-content-center align-items-center">
            {{
              recipe.ingredients.name
            }}</div>
        </div>
        <!-- SECTION NAME OF RECIPE/QUICK DESCRIPTION OF RECIPE -->
        <div class="row mt-5">
          <div class="col-12">
            <h1>{{ recipe?.title }}</h1>
          </div>
          <div class="col-12 mt-1">
            <p>{{ recipe?.title }} are a favorite quick and easy weeknight meal. Use ingredients such as {{
              recipe?.ingredients?.name
            }} in a skillet, and serve with a simply cabbage slaw. 20 minutes start to finish!
            </p>
          </div>
        </div>
        <!-- SECTION DETAILS ABOUT THE CREATOR OF THE RECIPE -->
        <div class="row d-flex align-items-center mt-2">
          <div class="col-1">
            <img class="img-fluid profilePic" :src="recipe?.creator.picture" alt="">
          </div>
          <div class="col-4">
            <span>By {{ recipe?.creator.name }}</span> <br>
            <span>Updated at {{ new Date(recipe?.updatedAt).toDateString('us-en') }}</span>
          </div>
        </div>
        <!-- SECTION RATING SYSTEM/CLICK DOWN TO COMMENTS/QUICK LINK DOWN TO RECIPE  -->
        <div class="row mt-3 borderBottom">
          <button class="col-2 mx-1 btn btn-outline">RATING</button>
          <button class="col-2 mx-1 btn btn-outline">COMMENTS</button>
          <button class="col-2 mx-1 btn btn-outline">TO RECIPE</button>
        </div>
        <div class="row my-4">
          <div class="col-8">
            <img class="img-fluid recipeImg" :src="recipe?.img" alt="">
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted } from "vue";
import { computed } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState";
import { recipesService } from "../services/RecipesService";
import Pop from "../utils/Pop";


export default {
  setup() {
    onMounted(() => {
      getRecipeById()
    })
    const route = useRoute();
    async function getRecipeById() {
      try {
        recipesService.getById(route.params.id)
      } catch (error) {
        console.error(error)
        // @ts-ignore
        Pop.error(('[ERROR]'), error.message)
      }
    }


    return {
      recipe: computed(() => AppState.activeRecipe)

    }
  }
}
</script>

<style scoped lang="scss">
.mainBody {
  margin-top: 10rem;
}

.sortedBy {
  background-color: rgb(247, 237, 237);
  color: rgb(50, 50, 50);
  border-radius: 16px;
  border: 1px rgba(97, 96, 96, 0.801) solid;
}

.sortedBy:hover {
  background-color: rgb(198, 194, 194);
  color: rgb(50, 50, 50);
  cursor: pointer;
}

p {
  font-size: larger;
  color: rgb(81, 81, 81);
}

.profilePic {
  border-radius: 50%;
}

.borderBottom {
  border-bottom: 2px dotted rgb(244, 199, 106);
}

.recipeImg {
  height: 100vh;
}
</style>
