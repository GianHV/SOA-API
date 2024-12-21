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
      text: "Order Detail Reports",
    },
  },
};

const isValidPositiveNumber = (value) => {
  return /^\d+$/.test(value);
};

const fetchOrderDetailReport = async () => {
  try {
    let userInput = prompt("Please enter the Report ID (positive number only):");
    if (userInput === null) {
      return;
    }
    if (!isValidPositiveNumber(userInput)) {
      toast.warning("Invalid input!", {
      timeout: 2000,
    });
    return
    }

    const { data } = await axios.get(
      `https://localhost:7130/api/OrderReport/${userInput}`
    );
    const orderResult = data.result;

    if (orderResult === null) {
      toast.info("Report ID cannot be found!", {
      timeout: 2000,
    });
    }
    else {
      chartData.value = {
        labels: [`ID: ${orderResult.id} - Order ID: ${orderResult.orderId}`],
        datasets: [
          {
            label: "Total Revenue",
            data: [orderResult.totalRevenue],
            backgroundColor: "rgba(37, 99, 235, 0.7)",
          },
          {
            label: "Total Cost",
            data: [orderResult.totalCost],
            backgroundColor: "rgba(220, 38, 38, 0.7)",
          },
          {
            label: "Total Profit",
            data: [orderResult.totalProfit],
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
  fetchOrderDetailReport();
});
</script>

<template>
  <BarChart
    v-if="chartData"
    :chart-data="chartData"
    :chart-options="chartOptions"
  />
</template>
