import { AppState } from "../AppState";
import { api } from "./AxiosService";

class RecipesService {
  async getRecipes() {
    const res = await api.get("api/recipes");
    console.log("Recipes", res.data);
    AppState.allRecipes = res.data;
  }

  async getById(recipeId) {
    const res = await api.get(`api/recipes/${recipeId}`);
    console.log(res.data);
    AppState.activeRecipe = res.data;
  }
}

export const recipesService = new RecipesService();
