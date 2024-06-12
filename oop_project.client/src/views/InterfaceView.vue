<script setup>
import { defineAsyncComponent } from "vue";

import ToolBar from "../components/ToolBar.vue";
const NavigationTree = defineAsyncComponent(() =>
  import("../components/NavigationTree.vue")
);
</script>

<template>
  <v-navigation-drawer v-model="drawer" width="350" app>
    <NavigationTree @close-nav-bar="changeDrawerState()" />
  </v-navigation-drawer>

  <v-app-bar :elevation="2">
    <ToolBar :changeDrawerState="changeDrawerState" />
  </v-app-bar>

  <v-main>
    <router-view />
  </v-main>
</template>

<script>
export default {
  name: "InterfaceView",
  data: () => ({ drawer: null }),
  methods: {
    changeDrawerState() {
      this.drawer = !this.drawer;
    },
  },
};
</script>
<style></style>
