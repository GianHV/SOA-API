<script setup>
import OrderReport from "@/components/OrderReport.vue";
import ProductReport from "@/components/ProductReport.vue";
import OrderDetailReport from "@/components/OrderDetailReport.vue";
import ProductDetailReport from "@/components/ProductDetailReport.vue";
import { ref } from "vue";

const isShowOrderReport = ref(false);
const isShowProductReport = ref(false);
const isShowOrderDetailReport = ref(false);
const isShowProductDetailReport = ref(false);

const toggleShowOrderReport = () =>
  (isShowOrderReport.value = !isShowOrderReport.value);
const toggleShowProductReport = () =>
  (isShowProductReport.value = !isShowProductReport.value);
  const toggleShowOrderDetailReport = () =>
  (isShowOrderDetailReport.value = !isShowOrderDetailReport.value);
  const toggleShowProductDetailReport = () =>
  (isShowProductDetailReport.value = !isShowProductDetailReport.value);
</script>

<template>
  <v-container>
    <div class="d-flex">
      <v-btn class="mr-3" size="small" color="primary" @click="toggleShowOrderReport"
      >Toggle Order Report</v-btn
    >
    <v-btn class="mr-3" size="small" color="primary" @click="toggleShowProductReport"
      >Toggle Product Report</v-btn
    >
    <v-btn class="mr-3" color="primary" size="small" @click="toggleShowOrderDetailReport"
      >Toggle Order Detail Report</v-btn
    >
    <v-btn color="primary" size="small" @click="toggleShowProductDetailReport"
      >Toggle Product Detail Report</v-btn
    >
    </div>
    
    <OrderReport class="mb-6" v-if="isShowOrderReport" />
    <ProductReport class="mb-6" v-if="isShowProductReport" />
    <OrderDetailReport class="mb-6" v-if="isShowOrderDetailReport" />
    <ProductDetailReport class="mb-6" v-if="isShowProductDetailReport" />
  </v-container>
</template>
