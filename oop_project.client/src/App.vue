<script setup>
import { defineAsyncComponent } from 'vue'

import ToolBar from './components/ToolBar.vue';
const NavigationTree = defineAsyncComponent(() => import('./components/NavigationTree.vue'));
</script>


<template>
  <v-app>
    
    <v-navigation-drawer v-model="drawer" width="350" app>
      <NavigationTree />
    </v-navigation-drawer>

    <v-app-bar :elevation="2">
      <ToolBar :changeDrawerState="changeDrawerState"/>
    </v-app-bar>

    

    <v-main>
      <router-view />
    </v-main>

    </v-app>
</template>

<script>
  export default {
    data: () => ({ drawer: true }),
    methods: {
      changeDrawerState(){
        this.drawer= !this.drawer
      }
    }
  }
</script>
<style>

#app,.body {
  margin: 0;
  padding: 0;
  width: 100vw;
  max-width: 100vw;
  height: 100vh;
  max-height: 100vh;
  display: block;
}
</style>