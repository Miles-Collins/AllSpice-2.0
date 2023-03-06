<template>
  <div @click="setActive(recipe.id)" class="card">
    <img class="card-img-top imgSize" :src="recipe.img" alt="" srcset="">
    <div class="card-body backgroundLight">
      <p class="card-text p-0 mb-1"><span class="text-danger">RECIPE</span>
        <span v-if="recipe.creator.name < 10">&nbsp;|&nbsp; >FROM {{ recipe.creator.name }}</span>
      </p>
      <div class="row upperRow">
        <p class="textSize m-0">
          {{ recipe.title }}
        </p>
      </div>
      <div class="row d-flex align-items-center lowerRow">
        <div class="col-6">
          <h2 class="align-items-center">
            <span class="minuteText mdi mdi-av-timer">{{ randomTime }} mins</span>
          </h2>
        </div>
        <div class="col-6 text-end">
          <span v-if="randomRating === 0">
            <span class="mdi mdi-star-outline"></span>
            <span class="mdi mdi-star-outline"></span>
            <span class="mdi mdi-star-outline"></span>
            <span class="mdi mdi-star-outline"></span>
            <span class="mdi mdi-star-outline"></span>
          </span>
          <span v-if="randomRating === 1">
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star-outline"></span>
            <span class="mdi mdi-star-outline"></span>
            <span class="mdi mdi-star-outline"></span>
            <span class="mdi mdi-star-outline"></span>
          </span>
          <span v-if="randomRating === 2">
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star-outline"></span>
            <span class="mdi mdi-star-outline"></span>
            <span class="mdi mdi-star-outline"></span>
          </span>
          <span v-if="randomRating === 3">
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star-outline"></span>
            <span class="mdi mdi-star-outline"></span>
          </span>
          <span v-if="randomRating === 4">
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star-outline"></span>
          </span>
          <span v-if="randomRating === 5">
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star"></span>
            <span class="mdi mdi-star"></span>
          </span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted } from "vue"
import { recipesService } from "../services/RecipesService"
import Pop from "../utils/Pop"


export default {
  props: {
    recipe: {
      type: Object
    }
  },


  setup(prop) {
    onMounted(() => {
      console.log(randomRating, prop.recipe.title)
    })
    const randomTime = Math.floor(Math.random() * 100)
    const randomRating = Math.floor(Math.random() * 5) + 1

    return {
      randomTime,
      randomRating,

      async setActive(recipeId) {
        try {
          recipesService.getById(recipeId)
        } catch (error) {
          console.error(error)
          // @ts-ignore
          Pop.error(('[ERROR]'), error.message)
        }
      }

    }
  }
}
</script>

<style>
.imgSize {
  min-height: 10vh;
  max-height: 18vh;
  width: 100%;
  object-fit: cover;
  border-radius: 25;
  /* border-radius: 5%; */
  /* box-shadow: 1px 3px 5px black; */
}

.backgroundLight {
  background-color: white;
  height: 20vh;
}

.class {
  cursor: pointer !important;
}

.upperRow {
  height: 60%
}

.lowerRow {
  height: 25%
}

.textSize {
  font-size: 1rem;
  padding-bottom: 0px;
}

.minuteText {
  font-size: 1rem;
}
</style>
