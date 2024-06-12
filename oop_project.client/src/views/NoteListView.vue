<template>
  <v-container fluid full-width class="d-flex flex-column-reverse justify-end h-100">
    <noteComponent v-if="notes == undefined || notes.length == 0" class="ma-6"></noteComponent>
    <noteComponent
      v-else
      v-for="note in notes"
      v-bind:key="note.id"
      :note="note"
      :editing="editing"
      :deleting="deleting"
      class="ma-6"
    ></noteComponent>
  </v-container>
  <notesToolbar
    @edit-mode="
      {
        (editing = !editing), (deleting = false);
      }
    "
    @delete-mode="
      {
        (deleting = !deleting), (editing = false);
      }
    "
  />
</template>
<script>
import { useNotesListStore } from "../stores/NotesListsStore";
import { useNoteStore } from "../stores/NoteStore";
import noteComponent from "../components/NoteComponent.vue";
import notesToolbar from "../components/NotesToolbar.vue";
export default {
  name: "NoteListView",
  components: {
    noteComponent,
    notesToolbar,
  },
  data() {
    return {
      //noteList: "",
      editing: false,
      deleting: false,
    };
  },
  methods: {
    // updateNotesList() {
    //   this.noteList = this.noteListStore.getNoteList(this.$route.params.id);
    // },
  },
  setup() {
    const noteListStore = useNotesListStore();
    const notesStore = useNoteStore();
    return { noteListStore, notesStore };
  },
  mounted() {
    // this.updateNotesList();
  },
  watch: {
    // "$route.params.id": {
    //   handler(newVal, oldVal) {
    //     if (newVal !== oldVal) {
    //       this.updateNotesList();
    //     }
    //   },
    //   immediate: true,
    // },
  },
  computed: {
    noteList() {
      return this.noteListStore.getNoteList(this.$route.params.id);
    },
    notes() {
      if (this.noteList && this.noteList.notes) {
        let noteList = this.noteList.notes.map((noteId) =>
          this.notesStore.getNoteList(noteId)
        );
        return noteList;
      }
      return [];
    },
  },
};
</script>
<style scoped></style>
