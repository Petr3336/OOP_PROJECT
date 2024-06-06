<template>
    <v-card>
    {{ navigationStore.getFolderById($route.params.id).name }}    
    </v-card>
    </template>
    <script>
    import { useNavigationStore } from "../stores/navigationStore.js";
    export default {
      setup() {
        const navigationStore = useNavigationStore();
        return { navigationStore };
      },
      mounted() {
        console.log("e")
         console.log(this.navigationStore.getFolderById(this.$route.params.id).name);
      },
      components: {
      },
      name: "FolderView",
      methods: {
      },
      computed: {},
    };
    </script>
    <style scoped></style>
    