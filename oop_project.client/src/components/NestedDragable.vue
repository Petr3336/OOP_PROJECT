<template>
  <draggable
    class="dragArea"
    :list="folders"
    :group="{ name: 'g1' }"
    item-key="name"
    @end="onUpdate"
    :clickable="false"
    :disabled="disabled"
  >
    <template #item="{ element }">
      <div>
        <div v-if="element.type == 0">
          <v-list-item
            :active="false"
            :prepend-icon="selectIcon(element.type)"
            :title="element.name"
            :key="element.id"
            @click="element.collapsed = !element.collapsed"
            :append-icon="
              element.collapsed ? 'mdi-chevron-up' : 'mdi-chevron-down'
            "
            >
          </v-list-item>
          <v-slide-y-transition :duration="155" mode="default" >
            <nested-draggable
              v-show="!element.collapsed"
              class="ml-8"
              :folders="element.childrenNavigation"
              @end="onUpdate"
              :empty="element.childrenNavigation.length == 0"
              :disabled="disabled"
            />
          </v-slide-y-transition>
        </div>
        <v-list-item
          v-if="element.type != 0"
          color="primary"
          :prepend-icon="selectIcon(element.type)"
          :title="element.name"
          :key="element.id"
          :to="computeRoute(element.id, element.type)"
        ></v-list-item>
      </div>
    </template>
    <template #header v-if="empty">
      <v-list-item
        prepend-icon="mdi-information-outline"
        title="Пока что это пустая папка"
        :key="0"
      >
      </v-list-item
    ></template>
    <template #footer></template>
  </draggable>
</template>
<script>
import { useNavigationStore } from "../stores/NavigationStore.js";
import draggable from "vuedraggable";
export default {
  setup() {
    const navigationStore = useNavigationStore();
    return { navigationStore };
  },
  props: {
    folders: {
      required: true,
      type: Array,
    },
    empty: {
      required: false,
      type: Boolean,
    },
    onUpdate: {
      required: false,
      type: Function,
    },
    disabled: {
      required: false,
      type: Boolean,
    },
  },
  components: {
    draggable,
  },
  name: "nested-draggable",
  data() {
    return {
      childrenVisible: true,
      isMounted: false,
    };
  },
  methods: {
    selectIcon(type) {
      switch (type) {
        case 0:
          return "mdi-folder";
        case 1:
          return "mdi-note";
        default:
          return "mdi-information-outline";
      }
    },
    computeRoute(id, type) {
      return "/" + (type == 0 ? "folder" : "note") + "/" + id;
    },
  },
  mounted() {
    this.isMounted = true;
  },
  computed: {
    isOpen() {
      return false;
    },
  },
};
</script>
<style scoped>
.dragArea {
  min-height: 50px;
}
.folder-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.icon,
.toggle-icon {
  width: 24px;
  height: 24px;
}
.folder-name {
  flex-grow: 1;
  margin: 0 10px;
}
.nested-files {
  margin-left: 34px; /* Отступ для вложенных файлов */
}
</style>
<!-- <v-expansion-panel>
          <v-expansion-panel :title="element.name" />
          <v-expansion-panel-text>
            <nested-draggable :tasks="element.tasks" :onUpdate="onUpdate" />
          </v-expansion-panel-text>
        </v-expansion-panel> -->
