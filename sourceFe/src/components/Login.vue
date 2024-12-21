<script setup>
import axios from "axios";
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useToast } from "vue-toastification";

const router = useRouter();
const email = ref("");
const password = ref("");
const toast = useToast();

const handleSubmit = async () => {
  const dataLogin = {
    email: email.value,
    password: password.value,
  };
  try {
    const {data} = await axios.post("https://localhost:7272/api/Auth", dataLogin);
    if (data.isSuccess === true && data.statusCode === 200) {
      if (data.result !== null) {
        localStorage.setItem("token", data.result);
        router.push({ name: "ProductSelector" });
      }
    }
  } catch {
    toast.error("Incorrect email or password!", {
      timeout: 2000,
    });
  }
};
</script>

<template>
  <v-container fluid>
    <v-card>
      <v-card-title class="text-h5 text-center">Login Form</v-card-title>
      <v-card-text>
        <v-form @submit.prevent="handleSubmit">
          <v-text-field
            label="Email"
            v-model="email"
            type="email"
            required
            outlined
          ></v-text-field>
          <v-text-field
            label="Password"
            v-model="password"
            type="password"
            required
            outlined
          ></v-text-field>
          <v-container class="d-flex mt-n2 mb-n3">
            <v-btn type="submit" color="primary"> Login </v-btn>
          </v-container>
        </v-form>
      </v-card-text>
    </v-card>
  </v-container>
</template>

<style scoped>
.v-card {
  width: 600px;
}
.v-container {
  display: flex;
  align-items: center;
  justify-content: center;
  margin-top: 10%;
}
</style>
