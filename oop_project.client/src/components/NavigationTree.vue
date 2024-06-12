<script setup>
import { useNavigationStore } from "../stores/NavigationStore.js";
import { storeToRefs } from "pinia";
import { ref, watch } from "vue";

const useFolderStore = defineStore('folder', {
  state: () => ({
    folders: [{
        id: 0,
        name: "1",
        position: "0",
        parent: 0,
        childrenFolders: [],
        childrenNotesLists: [],
      }]
  }),
});

const folderStore = useFolderStore();

const folders = ref(folderStore.folders);

watch(folders, (newFolders) => {
  folderStore.folders = newFolders;
}, { deep: true });

const navigationStoreRef = storeToRefs(navigationStore);

const navigation = navigationStoreRef.navigation;

watch(
  navigation,
  (newNavigation) => {
    console.log("update");
    function updatePosition(navigationElement) {
      navigationElement.forEach((item, index) => {
        item.position = index;
        updatePosition(item.childrenNavigation);
      });
    }
    updatePosition(newNavigation);
    navigationStore.navigation = newNavigation;
  },
  { deep: true }
);
</script>
<template class="d-flex flex-column">
  <v-list-item class="" height="64px" :elevation="2" title="Ваши заметки"
    ><template v-if="$vuetify.display.mobile" v-slot:prepend
      ><v-btn class="ml-n2 mr-2" flat icon @click="$emit('closeNavBar')"
        ><v-icon>mdi-menu</v-icon></v-btn
      ></template
    ></v-list-item
  >
  <v-list
    color="primary"
    :height="$vuetify.display.height - 128"
    bordered
    class="rounded-borders pa-0 overflow-y-auto"
  >
    <nested-draggable
      :folders="navigation"
      :empty="false"
      :disabled="disableDragging"
    />
  </v-list>
  <v-list-item
    class="mt-auto position-sticky bottom-0 left-0"
    height="64px"
    width="100%"
    :elevation="3"
    title=""
  >
    <template #prepend>
      <v-btn
        flat
        size="38px"
        icon
        v-tooltip="'Сменить тему'"
        @click="toggleTheme()"
      >
        <v-icon>mdi-brightness-6</v-icon>
      </v-btn>
      <v-snackbar
        v-if="$vuetify.display.mobile"
        v-model="snackbarIsDragabble"
        :timeout="1500"
        color="surface"
        class="mb-16"
      >
        <template v-slot:activator>
          <v-btn
            flat
            size="38px"
            icon
            v-tooltip="'Включить/выключить перетаскивание элементов дерева'"
            @click="toggleDraggable()"
          >
            <v-icon>mdi-file-tree</v-icon>
          </v-btn>
        </template>

        Перетаскивание {{ disableDragging ? "выключено" : "включено" }}
      </v-snackbar>
    </template>
    <template #append>
      <NewFolderDialog />
      <NewNoteListDialog />
    </template>
  </v-list-item>
</template>

<script>
import nestedDraggable from "./NestedDragable.vue";
import NewFolderDialog from "./NewFolderDialog.vue";
import NewNoteListDialog from "./NewNoteListDialog.vue";
export default {
  name: "navigationTree",
  order: 15,
  components: {
    nestedDraggable,
    NewFolderDialog,
    NewNoteListDialog,
  },
  emits: ["closeNavBar"],
  setup() {},
  data() {
    return {
      disableDragging: false,
      snackbarIsDragabble: false,
    };
  },
  methods: {
    toggleTheme() {
      console.log(this.$vuetify.theme.global.name);
      this.$vuetify.theme.global.name = this.$vuetify.theme.global.current.dark
        ? "light"
        : "dark";
    },
    toggleDraggable() {
      this.disableDragging = !this.disableDragging;
      this.snackbarIsDragabble = true;
    },
  },
  mounted() {
    this.disableDragging = this.$vuetify.display.mobile;
  },
};
</script>
<style scoped>
.NavigationTree {
  min-height: 50px;
  outline: 1px dashed;
}
</style>
