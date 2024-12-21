<script setup>
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";
import axios from "axios";
import { useToast } from "vue-toastification";

const route = useRoute();
const orderId = route.params.id;
const toast = useToast();

const order = ref({});
const statusMapping = {
  0: "Pending",
  1: "Completed",
  2: "Cancel",
};

const fetchOrder = async () => {
  try {
    const { data } = await axios.get(
      `https://localhost:7280/api/orders/${orderId}`
    );
    order.value = data.result;
  } catch (error) {
    console.error("Error:", error);
    toast.error("Service error!", {
      timeout: 2000,
    });
  }
};

const updateStatus = () => {
  if (order.value.status == 0) {
    setTimeout(async () => {
      try {
        await axios.put(`https://localhost:7280/api/orders`, {
          id: orderId,
          status: "1",
        });
        order.value.status = 1;
        toast.success("Update status completed successfully!", {
          timeout: 2000,
        });
      } catch (error) {
        console.error("Error:", error);
        toast.error("Cannot change order status!", {
          timeout: 2000,
        });
      }
    }, 3000);
  }
};

onMounted(async () => {
  await fetchOrder();
  updateStatus();
});
</script>

<template>
  <v-container class="d-flex flex-column align-center justify-center">
    <v-card v-if="order" class="mx-auto elevation-5" rounded>
      <v-list>
        <v-list-item class="text-center">
          <v-list-item-title class="text-h5 font-weight-bold">
            Order Details
          </v-list-item-title>
        </v-list-item>
        <v-divider></v-divider>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>
              <strong>Order ID:</strong> {{ order.id }}
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>
              <strong>Customer:</strong> {{ order.customerName }}
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>
              <strong>Email:</strong> {{ order.customerEmail }}
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>
              <strong>Status: </strong>
              <v-chip :color="order.status == 1 ? 'green' : 'blue'" dark small>
                {{ statusMapping[order.status] }}
              </v-chip>
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title>
              <strong>Order Items:</strong>
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
        <v-list>
          <v-list-item v-for="item in order.items" :key="item.productId">
            <v-list-item-content>
              <v-list-item-title>
                {{ item.productName }}
              </v-list-item-title>
              <v-list-item-subtitle>
                Quantity: {{ item.quantity }} x {{ item.unitPrice }}$
              </v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-list>
        <v-list-item>
          <v-list-item-content>
            <v-list-item-title class="font-weight-bold">
              Total Payment: {{ order.totalAmount }}$
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-card>
  </v-container>
</template>
