<script setup>
import { useNavigationStore } from "../stores/navigationStore.js";
import { ref, watch } from "vue";

const navigationStore = useNavigationStore();

const navigation = ref(navigationStore.navigation);

const updatePosition = (navigation) => {
  // Обновляем позиции элементов в хранилище
  navigation.forEach((item, index) => {
    navigationStore.updateNavigationPosition(item.id, index);
  });
};

watch(
  navigation,
  (newNavigation) => {
    navigationStore.navigation = newNavigation;
  },
  { deep: true }
);
</script>
<template class="d-flex flex-column">
  <v-list-item
    class=""
    height="64px"
    :elevation="2"
    title="Ваши заметки"
  ></v-list-item>
  <v-list :height="$vuetify.display.height-128" bordered class="rounded-borders pa-0 overflow-y-auto">
    <nested-draggable
      :folders="navigation"
      
      :empty="false"
      :onUpdate="updatePosition(navigation)"
    />
  </v-list>
  <v-list-item
    class="mt-auto position-sticky bottom-0 left-0 bg-white"
    height="64px"
    width="100%"
    :elevation="3"
    title="Ваши заметки"
  >
    <template #append>
      <v-btn flat size="38px" icon>
        <v-icon>mdi-note-plus</v-icon>
      </v-btn>
    </template>
  </v-list-item>
  
</template>

<script>
import nestedDraggable from "./NestedDragable.vue";
export default {
  name: "nested-example",
  display: "Nested",
  order: 15,
  components: {
    nestedDraggable,
  },
  setup() {},
  data() {
    return {};
  },
  methods: {
    updatePosition() {
      console.log(this.navigation);
      // Обновляем позиции элементов в хранилище
      this.navigation.forEach((item, index) => {
        this.navigationStore.updateNavigationPosition(item.id, index);
      });
    },
  },
};
</script>
<style scoped>
.NavigationTree {
  min-height: 50px;
  outline: 1px dashed;
}
</style>
