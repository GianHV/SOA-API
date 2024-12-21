<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import BarChart from "@/components/BarChart.vue";
import { useToast } from "vue-toastification";

const toast = useToast()
const chartData = ref(null);
const chartOptions = {
  responsive: true,
  plugins: {
    legend: {
      position: "top",
    },
    title: {
      display: true,
      text: "Order Reports",
    },
  },
};

const fetchOrderReport = async () => {
  try {
    const {data} = await axios.get("https://localhost:7130/api/OrderReport");
    const orderResult = data.result;

    chartData.value = {
      labels: orderResult.map((item) => `Order ID: ${item.orderId}`),
      datasets: [
        {
          label: "Total Revenue",
          data: orderResult.map((item) => item.totalRevenue),
          backgroundColor: "rgba(37, 99, 235, 0.7)",
        },
        {
          label: "Total Cost",
          data: orderResult.map((item) => item.totalCost),
          backgroundColor: "rgba(220, 38, 38, 0.7)",
        },
        {
          label: "Total Profit",
          data: orderResult.map((item) => item.totalProfit),
          backgroundColor: "rgba(249, 115, 22, 0.7)",
        },
      ],
    };
  } catch (error) {
    console.error("Error:", error);
    toast.error("Service error!", {
      timeout: 2000,
    });
  }
};

onMounted(() => {
  fetchOrderReport();
});
</script>
<template>
  <BarChart
    v-if="chartData"
    :chart-data="chartData"
    :chart-options="chartOptions"
  />

</template>
