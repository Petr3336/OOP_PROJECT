<!-- <script setup>
import { useNavigationStore } from "../stores/NavigationStore.js";
import { storeToRefs } from "pinia";
import { watch } from "vue";

const navigationStore = useNavigationStore();
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

import { useNotesListStore } from "../stores/NotesListsStore";
import { storeToRefs } from "pinia";

const noteListStore = useNotesListStore();

const noteListStoreRef = storeToRefs(noteListStore);

const notesLists = noteListStoreRef.noteList;

</script> -->
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
    lines="two"
    class="rounded-borders pa-0 overflow-y-auto"
  >
    <!-- <nested-draggable
      :folders="navigation"
      :empty="false"
      :disabled="disableDragging"
    /> -->

    <v-list-item
      v-for="noteList in notesLists"
      :key="noteList.id"
      :title="noteList.name"
      :to="'/note/' + noteList.id"
      @contextmenu="onContextMenu($event, noteList)"
    >
      <template #subtitle v-if="noteList.description">
        <div class="mb-1">
          {{ noteList.description }}
        </div>
      </template>
    </v-list-item>
  </v-list>
  <context-menu v-model:show="contextMenuShow" :options="optionsComponent">
    <context-menu-item>
      <template #icon>
        <v-icon size="20px">mdi-pencil</v-icon>
      </template>
      <template #label>
        <span class="label ml-1">Изменить список заметок</span>
      </template>
    </context-menu-item>
    <context-menu-item
      @click="noteListStore.removeNotesList(selectedNoteList.id)"
    >
      <template #icon>
        <v-icon size="20px">mdi-delete</v-icon>
      </template>
      <template #label>
        <span class="label ml-1">Удалить список заметок</span>
      </template>
    </context-menu-item>
  </context-menu>
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
      <!-- <NewFolderDialog /> -->
      <NewNoteListDialog />
    </template>
  </v-list-item>
</template>

<script>
//import nestedDraggable from "./NestedDragable.vue";
//import NewFolderDialog from "./NewFolderDialog.vue";
import NewNoteListDialog from "./NewNoteListDialog.vue";
import { useNotesListStore } from "../stores/NotesListsStore";
import { storeToRefs } from "pinia";
import { ContextMenu, ContextMenuItem } from "@imengyu/vue3-context-menu";
export default {
  name: "navigationTree",
  order: 15,
  components: {
    //nestedDraggable,
    //NewFolderDialog,
    NewNoteListDialog,
    ContextMenu,

    ContextMenuItem,
  },
  emits: ["closeNavBar"],
  setup() {
    const noteListStore = useNotesListStore();

    const noteListStoreRef = storeToRefs(noteListStore);

    const notesLists = noteListStoreRef.noteList;
    return { noteListStore, noteListStoreRef, notesLists };
  },
  data() {
    return {
      contextMenuShow: false,
      disableDragging: false,
      snackbarIsDragabble: false,
      selectedNoteList: null,
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
    onContextMenu(e, noteList) {
      e.preventDefault();
      this.selectedNoteList = noteList;
      //Set the mouse position
      this.optionsComponent.x = e.x;
      this.optionsComponent.y = e.y;
      //Show menu
      this.contextMenuShow = true;
      console.log(noteList);
    },
  },
  mounted() {
    this.disableDragging = this.$vuetify.display.mobile;
    this.noteListStore.refreshNoteListsFromServer();
  },
  computed: {
    optionsComponent() {
      return {
        theme: this.$vuetify.theme.current.dark ? "dark" : "default",
        iconFontClass: "mdi",
        customClass: "class-a",
        zIndex: 1010,
        minWidth: 230,
        x: 500,
        y: 200,
      };
    },
  },
};
</script>
<style>
.NavigationTree {
  min-height: 2px;
  outline: 1px dashed;
}
</style>
