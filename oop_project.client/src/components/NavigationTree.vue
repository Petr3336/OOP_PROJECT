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
<template>
  <div>
    <div>
      <v-list padding bordered class="rounded-borders">
        <nested-draggable
          :folders="navigation"
          :empty="false"
          :onUpdate="updatePosition(navigation)"
        />
      </v-list>
    </div>

    <rawDisplayer class="col-3" :value="list" title="List" />
  </div>
</template>

<script>
import { useNavigationStore } from "../stores/navigationStore.js";
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
