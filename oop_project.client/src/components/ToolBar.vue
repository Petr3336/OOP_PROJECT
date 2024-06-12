<template>
  <v-app-bar-nav-icon
    @click="changeDrawerState"
    class="ml-2"
  ></v-app-bar-nav-icon>

  <v-toolbar-title> {{ title }} </v-toolbar-title>
</template>

<script>
import { useNotesListStore } from "../stores/NotesListsStore";
export default {
  name: "ToolBar",
  props: {
    changeDrawerState: Function,
  },

  computed: {
    title() {
      let currentNoteList = this.noteListStore.getNoteList(
        this.$route.params.id
      );
      if (currentNoteList) return currentNoteList.name;
      else return "Откройте список заметок";
    },
  },
  setup() {
    const noteListStore = useNotesListStore();
    return { noteListStore };
  },
};
</script>
