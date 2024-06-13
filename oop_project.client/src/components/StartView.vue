<template>
  <v-container
    fluid
    full-width
    class="d-flex flex-column-reverse justify-center h-100 align-center"
  >
    <v-card class="my-6 mx-16" variant="elevated" max-width="1200px">
      <template #title>
        <div class="text-h4">
          Что-бы начать работу, выберите один из списков заметок слева
        </div>
      </template>
      <template #text>
        <span class="text-h6"
          >Откройте боковое меню навигации нажав на кнопку <v-icon class="mb-1" size="24px">mdi-menu</v-icon> слева сверху, если
          оно закрыто, а после откройте существующую заметку, или создайте новую
          нажав на кнопку <v-icon class="mb-1" size="24px">mdi-note-plus</v-icon> , для создания списка заметок в правом нижнем углу навигационной
          панели</span
        >
      </template>
    </v-card>
  </v-container>
</template>
<script>
export default {
  name: "StartView",
  components: {},
  data() {
    return {};
  },
  methods: {},
  setup() {},
  mounted: async function () {},
  watch: {},
  computed: {},
};
</script>
<style scoped></style>
