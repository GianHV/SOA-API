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
      text: "Product Reports",
    },
  },
};

const fetchProductReport = async () => {
  try {
    const {data} = await axios.get(
      "https://localhost:7130/api/ProductReport"
    );
    const productResult = data.result;

    chartData.value = {
      labels: productResult.map(
        (item) =>
          `Order ID: ${item.orderReportId} - Product ID: ${item.productId}`
      ),
      datasets: [
        {
          label: "Total Sold",
          data: productResult.map((item) => item.totalSold),
          backgroundColor: "rgba(34, 197, 94, 0.7)",
        },
        {
          label: "Revenue",
          data: productResult.map((item) => item.revenue),
          backgroundColor: "rgba(37, 99, 235, 0.7)",
        },
        {
          label: "Cost",
          data: productResult.map((item) => item.cost),
          backgroundColor: "rgba(220, 38, 38, 0.7)",
        },
        {
          label: "Profit",
          data: productResult.map((item) => item.profit),
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
  fetchProductReport();
});
</script>

<template>
  <BarChart
    v-if="chartData"
    :chart-data="chartData"
    :chart-options="chartOptions"
  />


</template>
