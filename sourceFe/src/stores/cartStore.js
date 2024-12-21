import { defineStore } from "pinia";

import { ref } from "vue";

export const useCartStore = defineStore("cart", () => {
  const cart = ref([]);

  const clearCart = () => {
    cart.value = [];
  };

  return { cart, clearCart };
});
