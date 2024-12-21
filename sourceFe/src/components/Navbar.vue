<script setup>
import { useRouter } from "vue-router";
import { useCartStore } from "@/stores/cartStore";

const router = useRouter();
const cartStore = useCartStore();

const logout = () => {
  localStorage.removeItem("token");
  router.push({ name: "Login" });
  cartStore.clearCart()
};
</script>

<template>
  <nav class="navbar">
    <div class="nav-items">
      <router-link :to="{name: 'ProductSelector'}" class="nav-link">Dashboard</router-link>
      <router-link :to="{name: 'Report'}" class="nav-link">Generate Report</router-link>
    </div>
    <button @click="logout" class="logout-button">Logout</button>
  </nav>
</template>

<style scoped>
.navbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background-color: #4caf50;
  padding: 10px 20px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.nav-items {
  display: flex;
  gap: 15px;
}

.nav-link {
  text-decoration: none;
  color: white;
  font-size: 16px;
  padding: 8px 12px;
  border-radius: 4px;
  transition: background-color 0.3s;
}

.nav-link:hover {
  background-color: #45a049;
}

.logout-button {
  background-color: #f44336;
  color: white;
  font-size: 16px;
  padding: 8px 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.logout-button:hover {
  background-color: #d32f2f;
}
</style>
