<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import BarChart from "@/components/BarChart.vue";
import { useToast } from "vue-toastification";

const toast = useToast();
const chartData = ref(null);
const chartOptions = {
  responsive: true,
  plugins: {
    legend: {
      position: "top",
    },
    title: {
      display: true,
      text: "Product Detail Reports",
    },
  },
};

const isValidPositiveNumber = (value) => {
  return /^\d+$/.test(value);
};

const fetchProductDetailReport = async () => {
  try {
    
    let userInput = prompt(
      "Please enter the Report ID (positive number only):"
    );
    if (userInput === null) {
      return;
    }
    if (!isValidPositiveNumber(userInput)) {
      toast.warning("Invalid input!", {
        timeout: 2000,
      });
      return;
    }

    const { data } = await axios.get(
      `https://localhost:7130/api/ProductReport/${userInput}`
    );
    const productResult = data.result;

    if (productResult === null) {
      toast.info("Report ID cannot be found!", {
        timeout: 2000,
      });
    } else {
      chartData.value = {
        labels: [
          `ID: ${productResult.id} - Order ID: ${productResult.orderReportId} - Product ID: ${productResult.productId}`,
        ],
        datasets: [
          {
            label: "Total Sold",
            data: [productResult.totalSold],
            backgroundColor: "rgba(34, 197, 94, 0.7)",
          },
          {
            label: "Revenue",
            data: [productResult.revenue],
            backgroundColor: "rgba(37, 99, 235, 0.7)",
          },
          {
            label: "Cost",
            data: [productResult.cost],
            backgroundColor: "rgba(220, 38, 38, 0.7)",
          },
          {
            label: "Profit",
            data: [productResult.profit],
            backgroundColor: "rgba(249, 115, 22, 0.7)",
          },
        ],
      };
    }
  } catch (error) {
    console.error("Error:", error);
    toast.error("Service error!", {
      timeout: 2000,
    });
  }
};

onMounted(() => {
  fetchProductDetailReport();
});
</script>

<template>
  <BarChart
    v-if="chartData"
    :chart-data="chartData"
    :chart-options="chartOptions"
  />
</template>
