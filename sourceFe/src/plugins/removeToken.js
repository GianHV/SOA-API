import axios from "axios";
import router from "@/router";
import { useCartStore } from "@/stores/cartStore";
import { useToast } from "vue-toastification";

axios.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response && error.response.status === 401) {
      const toast = useToast();
      const cartStore = useCartStore();
      localStorage.removeItem("token");
      router.push({ name: "Login" });
      toast.warning("Session expired. Please log in again!", {
        timeout: 2000,
      });

      cartStore.clearCart();
    }
    return Promise.reject(error);
  }
);
