<template>

  <section class="container-fluid">
    <div class="row">
      <div v-if="!activeRecipe" class="col-12 col-md-7 leftBar"
        :style="{ background: `url(${recipes[randomRecipe]?.img})` }"
        style="background-repeat: no-repeat; background-position: center; background-size: cover;">
        <div class="row">

        </div>
        <div class="row d-flex justify-content-center">
          <div class="col-12 leftBarSmush"></div>
          <div class="col-11 col-md-7 leftBarBox d-flex justify-content-center">
            <div class="row ">
              <h1 class="text-center mt-2 px-3">{{ recipes[randomRecipe]?.title }}</h1>
              <p class="text-center">{{ recipes[randomRecipe]?.instructions }}</p>
              <p @click="pushToRecipe(recipes[randomRecipe]?.id)" class="text-center getFont"><strong
                  class="getTheRecipe">GET THE RECIPE
                  ></strong>
              </p>
            </div>
          </div>
        </div>
      </div>
      <div v-else class="col-12 col-md-7 leftBar" :style="{ background: `url(${activeRecipe?.img})` }"
        style="background-repeat: no-repeat; background-position: center; background-size: cover;">
        <div class="row">

        </div>
        <div class="row d-flex justify-content-center">
          <div class="col-12 leftBarSmush"></div>
          <div class="col-11 col-md-7 leftBarBox d-flex justify-content-center">
            <div class="row ">
              <h1 class="text-center mt-2 px-3">{{ activeRecipe?.title }}</h1>
              <p class="text-center">{{ activeRecipe?.instructions }}</p>
              <p @click="pushToRecipe(activeRecipe?.id)" class="text-center getFont "><strong class="getTheRecipe">GET
                  THE RECIPE
                  ></strong>
              </p>
            </div>
          </div>
        </div>
      </div>
      <div class="col-12 col-md-5 rightBar">
        <div class="mt-3 row d-flex justify-content-center ">
          <div class="col-12 rightBarTitle"></div>
          <div v-for="r in recipes" key="r.id" class="col-12 col-md-6 p-2
    ">
            <RecipeCard :recipe="r" />
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import { onMounted } from "vue";
import AllSpiceBanner from "../components/AllSpiceBanner.vue";
import { recipesService } from "../services/RecipesService.js";
import Pop from "../utils/Pop";
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { useRouter } from "vue-router";

export default {

  setup() {
    const router = useRouter()
    onMounted(() => {
      getRecipes();

      // console.log(randomRecipe);
    })



    async function getRecipes() {
      try {
        await recipesService.getRecipes()
      } catch (error) {
        console.error(error)
        // @ts-ignore 
        Pop.error(('[ERROR]'), error.message)
      }
    }

    return {
      recipes: computed(() => AppState.allRecipes),
      randomRecipe: computed(() => Math.floor(Math.random() * AppState.allRecipes.length)),
      activeRecipe: computed(() => AppState.activeRecipe),

      pushToRecipe(recipeId) {
        try {
          router.push({ name: 'Recipe', params: { id: recipeId } })
        } catch (error) {
          console.error(error)
          // @ts-ignore 
          Pop.error(('[ERROR]'), error.message)
        }
      }
    };
  },
}
</script>

<style scoped lang="scss">
.cardSize {
  height: 25vh;
  width: 15vw;
}

.card {
  box-shadow: 1px 2px 5px black !important;
}

.leftBar {
  height: 90vh;
  background-repeat: no-repeat;
}

.leftBarSmush {
  height: 55vh;
}

.leftBarBox {
  border: 5px solid rgb(244, 199, 106);
  outline: 15px solid rgba(255, 255, 255, 0.857);
  height: 30vh;
  background-color: rgba(255, 255, 255, 0.857);
  display: flex;
  justify-content: center;
}

.getFont {
  padding: 0;
}

.rightBar {
  overflow-x: hidden;
  overflow-y: auto;
  height: 90vh;
}

.rightBarTitle {
  height: 30VH;
  background-image: url(src/assets/img/AllSpiceLogoBannerV2.png);
  background-repeat: no-repeat;
  background-size: contain;
  background-position: center;
}

.getTheRecipe {
  cursor: pointer;
}

.getTheRecipe:hover {
  text-decoration: underline;
}
</style>
