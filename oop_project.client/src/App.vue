<template>
  <v-app>
    <router-view />
  </v-app>
</template>

<script>
export default {
  data: () => ({}),
  methods: {
    toggleTheme() {
      console.log(this.$vuetify.theme.global.name);
      this.$vuetify.theme.global.name = this.$vuetify.theme.global.current.dark
        ? "light"
        : "dark";
    },
  },
  computed: {
  },
  mounted() {
    //this.toggleTheme()
  }
};
</script>
