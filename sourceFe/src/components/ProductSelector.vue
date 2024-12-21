<script setup>
import { ref, computed } from "vue";
import { useRouter } from "vue-router";
import { mdiMinus, mdiPlus } from "@mdi/js";
import { useCartStore } from "@/stores/cartStore";
import axios from "axios";
import { useToast } from "vue-toastification";

const router = useRouter();
const products = ref([]);
const cartStore = useCartStore();
const toast = useToast();
const isShowCart = ref(false);

const getProducts = async () => {
  try {
    const { data } = await axios.get("https://localhost:7065/api/products");
    products.value = data.result;
  } catch (error) {
    console.error("Error:", error);
    toast.error("Service error!", {
      timeout: 2000,
    });
  }
};

const addToCart = (product) => {
  const existItem = cartStore.cart.find(
    (item) => item.product.id === product.id
  );
  try {
    if (existItem) {
      return;
    } else {
      cartStore.cart.push({ product, quantity: 1 });
    }
  } finally {
    isShowCart.value = true;
  }
};

const incrementQuantity = (item) => {
  if (item.quantity < item.product.quantity) {
    item.quantity++;
  } else {
    toast.warning(`${item.product.name} is out of stock!`, {
      timeout: 2000,
    });
  }
};

const reduceQuantity = (item) => {
  if (item.quantity === 1) {
    cartStore.cart = cartStore.cart.filter((i) => i !== item);
  } else {
    item.quantity--;
  }
};

const filterProducts = computed(() =>
  products.value.filter((product) => product.quantity > 0)
);

const checkout = () => {
  router.push({ name: "OrderForm" });
};

getProducts();
</script>

<template>
  <v-app>
    <v-container>
      <h2>Product List</h2>
      <v-row>
        <v-col
          v-for="product in filterProducts"
          :key="product.id"
          cols="12"
          md="4"
        >
          <v-card>
            <v-card-title class="mb-2">{{ product.name }}</v-card-title>
            <v-card-text>Price: {{ product.price }}$</v-card-text>
            <v-card-text class="mt-n6"
              >Quantity: {{ product.quantity }}</v-card-text
            >
            <v-card-actions>
              <v-btn @click="addToCart(product)" color="primary"
                >Add to cart</v-btn
              >
            </v-card-actions>
          </v-card>
        </v-col>
      </v-row>
    </v-container>

    <v-navigation-drawer v-model="isShowCart" right temporary width="400">
      <v-container>
        <h2 class="text-center">Your Cart</h2>
        <v-row v-if="cartStore.cart.length > 0">
          <v-col
            v-for="item in cartStore.cart"
            :key="item.product.id"
            cols="12"
          >
            <v-card>
              <v-card-title>{{ item.product.name }}</v-card-title>
              <v-card-text> Quantity: {{ item.quantity }} </v-card-text>
              <v-card-actions>
                <v-btn icon @click="reduceQuantity(item)">
                  <v-icon>{{ mdiMinus }}</v-icon>
                </v-btn>
                <span>{{ item.quantity }}</span>
                <v-btn icon @click="incrementQuantity(item)">
                  <v-icon>{{ mdiPlus }}</v-icon>
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-col>
          <v-col>
            <v-btn color="primary" @click="checkout">Checkout</v-btn>
          </v-col>
        </v-row>

        <p v-else>Your cart is empty</p>
      </v-container>
    </v-navigation-drawer>
  </v-app>
</template>
