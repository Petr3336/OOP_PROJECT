<template>
  <v-navigation-drawer permanent rail location="right">
    <v-list density="compact" nav>
      <v-list-item
        prepend-icon="mdi-view-dashboard"
        @click="console.log('e')"
      ></v-list-item>

      <v-list-item prepend-icon="mdi-forum"></v-list-item>
    </v-list>
  </v-navigation-drawer>
</template>
<script>
export default {
  name: "NotesToolbar",
  props: {
    note: {
      required: true,
      type: Array,
    },
  },
  setup() {},
  mounted() {},
  components: {},
  methods: {},
  computed: {},
};
</script>
<style scoped></style>
