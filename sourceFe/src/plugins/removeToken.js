import axios from "axios";
import router from "@/router";
import { useCartStore } from "@/stores/cartStore";

axios.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 401) {
      const cartStore = useCartStore();
      localStorage.removeItem("token");
      router.push({ name: "Login" });
      cartStore.clearCart();
    }
    return Promise.reject(error);
  }
);
