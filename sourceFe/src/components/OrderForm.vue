<script setup>
import { ref, computed, onUnmounted } from "vue";
import axios from "axios";
import { useToast } from "vue-toastification";
import { useCartStore } from "@/stores/cartStore";
import { useRouter } from "vue-router";

const customerName = ref("");
const customerEmail = ref("");
const toast = useToast();
const cartStore = useCartStore();
const router = useRouter()

const totalPayment = computed(() =>
  cartStore.cart.reduce((total, item) => {
    return total + item.quantity * item.product.price;
  }, 0)
);

const submitOrder = async () => {
  const orderData = {
    customerName: customerName.value,
    customerEmail: customerEmail.value,
    items: cartStore.cart.map((item) => ({
      productId: item.product.id,
      productName: item.product.name,
      quantity: item.quantity,
      unitPrice: item.product.price,
    })),
  };

  try {
    const {data} = await axios.post("https://localhost:7280/api/orders", orderData);
    const orderId = data.result

    router.push({name: "OrderResult", params: {id: orderId}})

  } catch (error) {
    console.error("Error:", error);
    toast.error("Order failed!", {
      timeout: 2000,
    });
  }
};

onUnmounted(() => {
  cartStore.clearCart()
})
</script>

<template>
  <v-container class="d-flex flex-column">
    <v-container>
      <v-card>
        <v-card-title class="text-center">Order Form</v-card-title>

        <v-form @submit.prevent="submitOrder">
          <v-card-text>
            <v-text-field
              v-model="customerName"
              label="Name"
              required
              outlined
            />

            <v-text-field
              v-model="customerEmail"
              label="Email"
              required
              outlined
            />
          </v-card-text>
          <div class="d-flex justify-center">
            <v-btn class="mb-4" type="submit" color="success">Order</v-btn>
            </div>
          
        </v-form>
      </v-card>
    </v-container>

    <v-divider class="my-3"></v-divider>

    <v-card style="width: 635px;">
      <h3 class="text-h6 mb-3 text-center">Cart</h3>
    <v-list>
      <v-list-item v-for="item in cartStore.cart" :key="item.product.id">
        <v-list-item-title>{{ item.product.name }}</v-list-item-title>
        <v-list-item-subtitle
          >Quantity: {{ item.quantity }} x
          {{ item.product.price }}$</v-list-item-subtitle
        >
        <v-list-item-subtitle
          >Total price:
          {{ item.quantity * item.product.price }}$</v-list-item-subtitle
        >
      </v-list-item>
      <v-list-item>
        <v-list-item-title 
          >Total payment: {{ totalPayment }}$</v-list-item-title
        >
      </v-list-item>
    </v-list>
    </v-card>
    
  </v-container>
</template>

<style scoped>
.v-text-field {
  width: 600px;
}
.v-container {
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
